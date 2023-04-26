using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MG02GameController : MonoBehaviour
{      
    [Header("Canva")]
    [SerializeField] private TextMeshProUGUI textMoves;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private GameObject[] listTextLines;

    // Para controle do jogo
    private GameObject blockSelected;
    [HideInInspector] public bool canClick = false;
    [HideInInspector] public List<List<GameObject>> blockInRightPositions;
    int arrivedBlocks = 0;

    // Contadores de Progresso
    int countMoves = 0;
    float timeCount = 0;

    // Outros
    bool isPaused = false;

    private void Update()
    {
        VerifyClick();
        UpdateTime();
    }

    /// <summary>
    /// Contabiliza o tempo passado desde o começo do jogo
    /// </summary>
    private void UpdateTime()
    {
        if (!isPaused)
        {
            timeCount += Time.deltaTime;
            textTime.text = Mathf.FloorToInt(timeCount).ToString();
        }        
    }    

    /// <summary>
    /// Verifica se algum clique ocorreu, trata o clique de acordo com o colisor que foi encontrado.
    /// </summary>
    void VerifyClick()
    {
        if (canClick)
        {
            bool isClicked = false;
            Vector2 touchPosition = Vector2.negativeInfinity;

            if (Input.GetMouseButtonDown(0))
            {
                touchPosition = Input.mousePosition;
                isClicked = true;
            }
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    touchPosition = touch.position;
                    isClicked = true;
                }
            }

            if (isClicked)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Block"))
                        {
                            OnBlockTouched(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Comportamento de clique para o bloco:
    /// Primeiro verifica-se se o bloco está estático, se for o caso, nada acontece.
    /// Depois verifica-se se já existe um bloco selecionado, e não for o caso, esse bloco é selecionado.
    /// Se já existir um bloco selecionado os blocos trocam de posição.
    /// </summary>
    /// <param name="block"></param>
    public void OnBlockTouched(GameObject block)
    {
        MG02BlockController blockClickedController = block.GetComponent<MG02BlockController>();

        if (!blockClickedController.isStatic && block != blockSelected)
        {
            if (blockSelected == null)
            {
                blockSelected = block;
                blockClickedController.ShowIndicatorCircle();
            }
            else
            {
                blockSelected.GetComponent<MG02BlockController>().MoveTo(block.transform.position);
                blockClickedController.MoveTo(blockSelected.transform.position);
                blockSelected = null;
                canClick = false;

                countMoves += 1;
                textMoves.text = countMoves.ToString();
            }
        }
        
    }

    /// <summary>
    /// Verifica se os dois blocos sinalizaram chegada ao destino. 
    /// Quando for o caso, libera o clique depois dos blocos terminarem de se movimentar e
    /// verifica se alguma lista foi completa e/ou se o jogo foi finalizado.
    /// </summary>
    public void FreeClick()
    {
        arrivedBlocks += 1;

        if (arrivedBlocks == 2)
        {
            canClick = true;
            arrivedBlocks = 0;
            VerifyGameProgress();
        }        
    }

    void VerifyGameProgress()
    {
        bool isGameCompleted = true;

        for (int lineIndex = 0; lineIndex < blockInRightPositions.Count; lineIndex++)
        {
            List<GameObject> lineList = blockInRightPositions[lineIndex];

            bool isLineCompleted = true;

            foreach (GameObject block in lineList)
            {
                if (!block.GetComponent<MG02BlockController>().isStatic)
                {
                    isLineCompleted = false;
                    isGameCompleted = false;
                    break;
                }
            }

            if (isLineCompleted)
            {
                Debug.Log("LINHA " + lineIndex + " COMPLETA!");
                LineCompleted(lineIndex);
            }
        }

        if (isGameCompleted)
        {
            EndGame();
        }
    }

    /// <summary>
    /// Mostra na tela a linha de texto referente a linha completada.
    /// </summary>
    /// <param name="index"></param>
    void LineCompleted(int index)
    {
        listTextLines[index].gameObject.SetActive(true);
    }

    //TODO
    /// <summary>
    /// Método para executaro o fim do jogo
    /// </summary>
    void EndGame()
    {

    }
}

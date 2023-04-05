using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MG02GameController : MonoBehaviour
{
    public GameObject blockSelected;
    public GameObject gridGeneratorGameObject;
    GridGenerator gridGenerator;

    int countMoves = 0;

    [Header("Canva")]
    [SerializeField] private TextMeshProUGUI textMoves;
    [SerializeField] private GameObject[] listTextLines;

    [Header("Shuffle")]
    bool isShuffled = false;
    float countToShuffle = 0;

    bool canClick = false;

    private void Start()
    {
        gridGenerator = gridGeneratorGameObject.GetComponent<GridGenerator>();
    }

    private void Update()
    {
        VerifyClick();
        NeedToShuffle();
    }

    void NeedToShuffle()
    {
        if (!isShuffled && gridGenerator.isFinishedInstantiate)
        {
            countToShuffle += Time.deltaTime;
            if (countToShuffle < 1)
            {
                return;
            }

            int centerList = (gridGenerator.toShuffleBlocks.Count / 2);
            List<GameObject> listKings = gridGenerator.toShuffleBlocks.GetRange(0, centerList);
            List<GameObject> listQueens = gridGenerator.toShuffleBlocks.GetRange(centerList, centerList);

            foreach (GameObject king in listKings)
            {
                int queenNumber = Random.Range(0, listQueens.Count-1);
                GameObject chosenQueen = listQueens[queenNumber];

                king.GetComponent<BlockController>().MoveTo(chosenQueen.transform.position);
                chosenQueen.GetComponent<BlockController>().MoveTo(king.transform.position);

                listQueens.RemoveAt(queenNumber);
            }
            isShuffled = true;
            canClick = true;
        }
       
    }

    void VerifyClick()
    {
        if (canClick && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null )
                {
                    if (hit.collider.CompareTag("Block"))
                    {
                        OnBlockTouched(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    public void OnBlockTouched(GameObject block)
    {
        BlockController blockClickedController = block.GetComponent<BlockController>();

        if (!blockClickedController.isStatic)
        {
            if (blockSelected == null)
            {
                blockSelected = block;
                blockClickedController.ShowIndicatorCircle();
            }
            else
            {
                blockSelected.GetComponent<BlockController>().MoveTo(block.transform.position);
                blockClickedController.MoveTo(blockSelected.transform.position);
                blockSelected = null;
                canClick = false;

                countMoves += 1;
                textMoves.text = countMoves.ToString();
            }
        }
        
    }

    public void FreeClick()
    {
        canClick = true;
        VerifyGameProgress();
    }

    void VerifyGameProgress()
    {
        List<List<GameObject>> blockInRightPositions = gridGenerator.blockInRightPositions;
        bool isGameCompleted = true;

        for (int lineIndex = 0; lineIndex < blockInRightPositions.Count; lineIndex++)
        {
            List<GameObject> lineList = blockInRightPositions[lineIndex];

            bool isLineCompleted = true;

            foreach (GameObject block in lineList)
            {
                if (!block.GetComponent<BlockController>().isStatic)
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

    void LineCompleted(int index)
    {
        listTextLines[index].gameObject.SetActive(true);
    }

    void EndGame()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG02BlockController : MonoBehaviour
{
    [HideInInspector] public MG02GameController gameController; // Informa��o recebida pelo GridGenerator

    [Header("Indicators")]
    [SerializeField] private GameObject indicatorCircle;
    [SerializeField] private GameObject indicatorCorrect;

    [Header("Position Puzzle")]
    public Vector3 correctPosition; // Informa��o recebida pelo GridGenerator
    public bool isStatic = false; // Informa��o recebida pelo GridGenerator, alterada ao longo do jogo
    public Color baseColor; // Informa��o recebida pelo GridGenerator

    // Configura��es
    readonly float speed = 4;
    Vector3 destination;
    bool isMoving;

    void Start()
    {
        Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b, 0.666f);
        GetComponent<SpriteRenderer>().color = newColor;

        if (isStatic)
        {
            indicatorCorrect.SetActive(true);
        }
    }
    
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                isMoving = false;
                indicatorCircle.SetActive(false);

                if (transform.position == correctPosition)
                {
                    indicatorCorrect.SetActive(true);
                    isStatic = true;
                }

                //Avisa ao GameController que o bloco chegou na posi��o desejada
                gameController.FreeClick();
            }
        }
    }

    /// <summary>
    /// M�todo p�blico que inicia o movimento do bloco.
    /// </summary>
    /// <param name="position">Posi��o final que o bloco deve alcan�ar</param>
    public void MoveTo(Vector3 position)
    {
        isMoving = true;
        destination = position;
    }

    /// <summary>
    /// Ativar o indicador que mostra que esse bloco foi selecionado.
    /// </summary>
    public void ShowIndicatorCircle()
    {
        indicatorCircle.SetActive(true);
    }
}

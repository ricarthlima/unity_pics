using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG02BlockController : MonoBehaviour
{
    [HideInInspector] public MG02GameController gameController; // Informação recebida pelo GridGenerator

    [Header("Indicators")]
    [SerializeField] private GameObject indicatorCircle;
    [SerializeField] private GameObject indicatorCorrect;

    [Header("Position Puzzle")]
    public Vector3 correctPosition; // Informação recebida pelo GridGenerator
    public bool isStatic = false; // Informação recebida pelo GridGenerator, alterada ao longo do jogo
    public Color baseColor; // Informação recebida pelo GridGenerator

    // Configurações
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

                //Avisa ao GameController que o bloco chegou na posição desejada
                gameController.FreeClick();
            }
        }
    }

    /// <summary>
    /// Método público que inicia o movimento do bloco.
    /// </summary>
    /// <param name="position">Posição final que o bloco deve alcançar</param>
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

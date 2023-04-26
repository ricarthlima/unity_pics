using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG03AreaTracejadaController : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] public MG03Areas area;

    [Header("Components")]
    [SerializeField] private GameObject clickButton;
    [SerializeField] private SpriteRenderer tracejado;
    Vector3 clickButtonInitialPos;

    [Header("Sprites")]
    [SerializeField] private Sprite auditivoInterprete;
    [SerializeField] private Sprite auditivoMapaTatil, cadeirantePortasLargas, cadeiranteRampa, cadeiranteReservado, nanismo, visualBraile, visualPisoTatil;

    Vector3 toMove = Vector3.up;

    /// <summary>
    /// Posiciona o tracejado e o indicado conforme a área que foi definida.
    /// </summary>
    void Start()
    {
        clickButtonInitialPos = clickButton.transform.localPosition;

        switch (area)
        {
            case MG03Areas.AuditivoMapa:
                transform.position = new Vector3(-0.91f, -0.93f, 0);
                tracejado.sprite = auditivoMapaTatil;
                break;

            case MG03Areas.AuditivoLibras:
                transform.position = new Vector3(-0.61f, -0.23f, 0);
                tracejado.sprite = auditivoInterprete;
                break;

            case MG03Areas.FisicoPortasLargas:
                transform.position = new Vector3(-7.19f, -0.96f, 0);
                tracejado.sprite = cadeirantePortasLargas;
                break;

            case MG03Areas.FisicoAreaReservada:
                transform.position = new Vector3(-1.14f, 1.89f, 0);
                tracejado.sprite = cadeiranteReservado;
                break;

            case MG03Areas.FisicoRampa:
                transform.position = new Vector3(-4, -3.59f, 0);
                tracejado.sprite = cadeiranteRampa;
                break;

            case MG03Areas.Nanismo:
                transform.position = new Vector3(-3.76f, -1, 0);
                tracejado.sprite = nanismo;
                break;

            case MG03Areas.VisualBraille:
                transform.position = new Vector3(2.91f, -0.86f, 0);
                tracejado.sprite = visualBraile;
                break;

            case MG03Areas.VisualPiso:
                transform.position = new Vector3(-2.04f, -1.91f, 0);
                tracejado.sprite = visualPisoTatil;
                break;

        }

        
    }
    
    /// <summary>
    /// O update apenas faz o indicador subir e descer.
    /// </summary>
    void Update()
    {
        clickButton.transform.localPosition += (toMove * 0.5f * Time.deltaTime);

        if (clickButton.transform.localPosition.y >= clickButtonInitialPos.y + 0.25f)
        {
            toMove = Vector3.down;
        }

        if (clickButton.transform.localPosition.y <= clickButtonInitialPos.y)
        {
            toMove = Vector3.up;
        }
    }

    /// <summary>
    /// Método que define o que acontece se essa é uma área incorreta e foi clicada.
    /// </summary>
    public void IncorrectClick()
    {
        gameObject.SetActive(false);
    }
}

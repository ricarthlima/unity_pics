using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG03AreaClickController : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] public Layers layer;

    [Header("Components")]
    [SerializeField] private GameObject clickButton;
    [SerializeField] private SpriteRenderer tracejado;
    Vector3 clickButtonInitialPos;

    [Header("Sprites")]
    [SerializeField] private Sprite auditivoInterprete;
    [SerializeField] private Sprite auditivoMapaTatil, cadeirantePortasLargas, cadeiranteRampa, cadeiranteReservado, nanismo, visualBraile, visualPisoTatil;

    bool isMovingClicker = true;
    float timeToMove;
    
    void Start()
    {
        clickButtonInitialPos = clickButton.transform.localPosition;

        switch (layer)
        {
            case Layers.AuditivoMapa:
                transform.position = new Vector3(-0.91f, -0.93f, 0);
                tracejado.sprite = auditivoMapaTatil;
                break;

            case Layers.AuditivoLibras:
                transform.position = new Vector3(-0.61f, -0.23f, 0);
                tracejado.sprite = auditivoInterprete;
                break;

            case Layers.FisicoPortasLargas:
                transform.position = new Vector3(-7.19f, -0.96f, 0);
                tracejado.sprite = cadeirantePortasLargas;
                break;

            case Layers.FisicoAreaReservada:
                transform.position = new Vector3(-1.14f, 1.89f, 0);
                tracejado.sprite = cadeiranteReservado;
                break;

            case Layers.FisicoRampa:
                transform.position = new Vector3(-4, -3.59f, 0);
                tracejado.sprite = cadeiranteRampa;
                break;

            case Layers.Nanismo:
                transform.position = new Vector3(-3.76f, -1, 0);
                tracejado.sprite = nanismo;
                break;

            case Layers.VisualBraille:
                transform.position = new Vector3(2.91f, -0.86f, 0);
                tracejado.sprite = visualBraile;
                break;

            case Layers.VisualPiso:
                transform.position = new Vector3(-2.04f, -1.91f, 0);
                tracejado.sprite = visualPisoTatil;
                break;

        }

        
    }

    Vector3 toMove = Vector3.up;
    void Update()
    {
        if (isMovingClicker)
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
    }

    public void IncorrectClick()
    {
        gameObject.SetActive(false);
    }
}

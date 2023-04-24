using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MG03GameController;

public class MG03InGameAreasController : MonoBehaviour
{
    [SerializeField] MG03CameraController cameraController;

    private bool isLerpLayer = false;

    [Header("Layers")]
    [SerializeField] private SpriteRenderer FisicoRampa, FisicoAreaReservada, FisicoPortasLargas, Nanismo, VisualPiso, VisualBraille, AuditivoLibras, AuditivoMapa;

    SpriteRenderer toActiveSprite;

    [SerializeField] private MG03GameController gameController;

    private void Update()
    {
        DoLerpLayer();
    }

    public void OnClickAnimation(Layers layer, GameObject areaTouched)
    {
        switch (layer)
        {
            case Layers.FisicoRampa:
                toActiveSprite = FisicoRampa;
                break;
            case Layers.Nanismo:
                toActiveSprite = Nanismo;
                break;
            case Layers.FisicoAreaReservada:
                toActiveSprite = FisicoAreaReservada;
                break;
            case Layers.FisicoPortasLargas:
                toActiveSprite = FisicoPortasLargas;
                break;
            case Layers.VisualPiso:
                toActiveSprite = VisualPiso;
                break;
            case Layers.VisualBraille:
                toActiveSprite = VisualBraille;
                break;
            case Layers.AuditivoLibras:
                toActiveSprite = AuditivoLibras;
                break;
            case Layers.AuditivoMapa:
                toActiveSprite = AuditivoMapa;
                break;
        }

        cameraController.ShowLayerInAnimation(areaTouched.transform.position);
    }

    public void LerpLayer()
    {
        isLerpLayer = true;
    }

    private void DoLerpLayer()
    {
        if (isLerpLayer)
        {
            toActiveSprite.color = Color.Lerp(toActiveSprite.color, Color.white, Time.deltaTime * 10);
            if (toActiveSprite.color == Color.white)
            {
                isLerpLayer = false;
                toActiveSprite = null;
                cameraController.ShowLayerOutAnimation();
            }
        }        
    }
}

public enum Layers {FisicoRampa, FisicoAreaReservada, FisicoPortasLargas, Nanismo, VisualPiso, VisualBraille, AuditivoLibras, AuditivoMapa};
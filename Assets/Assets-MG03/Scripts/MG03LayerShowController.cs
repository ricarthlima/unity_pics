using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MG03GameController;

/// <summary>
/// Essa classe é um simples controlador para fazer a animação de aparição
/// da layer contendo uma nova acessibilidade, na tela.
/// </summary>
public class MG03LayerShowController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private MG03GameController gameController;
    [SerializeField] MG03CameraController cameraController;    

    [Header("Layers")]
    [SerializeField] private SpriteRenderer FisicoRampa, FisicoAreaReservada, FisicoPortasLargas, Nanismo, VisualPiso, VisualBraille, AuditivoLibras, AuditivoMapa;

    SpriteRenderer toActiveSprite;
    bool isLerpLayer = false;

    private void Update()
    {
        DoLerpLayer();
    }

    public void OnClickAnimation(MG03Areas layer, GameObject areaTouched)
    {
        switch (layer)
        {
            case MG03Areas.FisicoRampa:
                toActiveSprite = FisicoRampa;
                break;
            case MG03Areas.Nanismo:
                toActiveSprite = Nanismo;
                break;
            case MG03Areas.FisicoAreaReservada:
                toActiveSprite = FisicoAreaReservada;
                break;
            case MG03Areas.FisicoPortasLargas:
                toActiveSprite = FisicoPortasLargas;
                break;
            case MG03Areas.VisualPiso:
                toActiveSprite = VisualPiso;
                break;
            case MG03Areas.VisualBraille:
                toActiveSprite = VisualBraille;
                break;
            case MG03Areas.AuditivoLibras:
                toActiveSprite = AuditivoLibras;
                break;
            case MG03Areas.AuditivoMapa:
                toActiveSprite = AuditivoMapa;
                break;
        }

        cameraController.ShowLayerInAnimation(areaTouched.transform.position);
    }

    public void LerpLayer()
    {
        isLerpLayer = true;
    }

    /// <summary>
    /// Anima a aparição da layer
    /// </summary>
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


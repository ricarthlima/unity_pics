using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MG03GameController;

public class MG03InGameAreasController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera m_Camera;
    private bool animateCamera = false;
    private bool isTouchedZoom = false;

    [Header("Layers")]
    [SerializeField] private bool isToMove;
    [SerializeField] private SpriteRenderer FisicoRampa, FisicoAreaReservada, FisicoPortasLargas, Nanismo, VisualPiso, VisualBraille, AuditivoLibras, AuditivoMapa;


    SpriteRenderer toActiveSprite;

    private void Update()
    {
        //Animate Camera
        if (animateCamera)
        {
            if (!isTouchedZoom)
            {
                m_Camera.orthographicSize -= 1 * Time.deltaTime * 2;

                if (m_Camera.orthographicSize <= 2.45f)
                {
                    isTouchedZoom = true;

                }
            }
            else
            {
                if (toActiveSprite != null)
                {
                    LerpLayer();
                }
                else
                {
                    m_Camera.orthographicSize += 1 * Time.deltaTime * 2;
                    if (m_Camera.orthographicSize >= 4.42f)
                    {
                        animateCamera = false;
                        isTouchedZoom = false;
                    }
                }

            }
        }

    }

    private void OnClickAnimation(Layers layer)
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

        animateCamera = true;
    }

    private void LerpLayer()
    {
        toActiveSprite.color = Color.Lerp(toActiveSprite.color, Color.white, Time.deltaTime * 10);
        if (toActiveSprite.color == Color.white)
        {
            toActiveSprite = null;
        }
    }
}

public enum Layers {FisicoRampa, FisicoAreaReservada, FisicoPortasLargas, Nanismo, VisualPiso, VisualBraille, AuditivoLibras, AuditivoMapa};
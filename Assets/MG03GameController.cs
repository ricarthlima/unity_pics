using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MG03GameController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera m_Camera;
    private bool animateCamera = false;
    private bool isTouchedZoom = false;

    [Header("Layers")]
    [SerializeField] private GameObject groupButtons;
    [SerializeField] private SpriteRenderer layerCadeirante;
    [SerializeField] private SpriteRenderer layerNanismo;
    [SerializeField] private SpriteRenderer layerPessoaCega;
    [SerializeField] private SpriteRenderer layerPessoaMuda;
    [SerializeField] private SpriteRenderer layerPortasLargas;
    public enum Layers {Cadeirante, Nanismo, PessoaCega, PessoaMuda, PortasLargas};
    SpriteRenderer toActiveSprite;

    [Header("Game")]
    public bool canInteract = false;

    void Start()
    {
        groupButtons.SetActive(false);
        m_Camera.gameObject.transform.position = new Vector3(-1.51f, -3f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Camera.gameObject.transform.position.y <= -2.11f)
        {
            m_Camera.gameObject.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * 0.5f;
        }
        else
        {
            if (!canInteract)
            {
                canInteract = true;
                groupButtons.SetActive(true);
            }
        }
        
        
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
                    if (m_Camera.orthographicSize >= 3.4f)
                    {
                        animateCamera = false;
                        isTouchedZoom = false;
                    }
                }
                
            }
        }


    }
    //  Nanismo, PessoaCega, PessoaMuda, PortasLargas, Reiniciar
    public void OnClickCadeirante()
    {
        OnClickAnimation(Layers.Cadeirante);
    }

    public void OnClickNanismo()
    {
        OnClickAnimation(Layers.Nanismo);
    }

    public void OnClickPessoaCega()
    {
        OnClickAnimation(Layers.PessoaCega);
    }

    public void OnClickPessoaMuda()
    {
        OnClickAnimation(Layers.PessoaMuda);
    }

    public void OnClickPortasLargas()
    {
        OnClickAnimation(Layers.PortasLargas);
    }

    public void OnClickReiniciar()
    {
        layerCadeirante.color = new Color(1,1,1,0);
        layerNanismo.color = new Color(1, 1, 1, 1);
        layerPessoaCega.color = new Color(1, 1, 1, 0);
        layerPessoaMuda.color = new Color(1, 1, 1, 0);
        layerPortasLargas.color = new Color(1, 1, 1, 0);
    }

    private void OnClickAnimation(Layers layer)
    {          
        switch (layer)
        {
            case Layers.Cadeirante:
                toActiveSprite = layerCadeirante;
                break;
            case Layers.Nanismo:
                toActiveSprite = layerNanismo;
                break;
            case Layers.PessoaCega:
                toActiveSprite = layerPessoaCega;
                break;
            case Layers.PortasLargas:
                toActiveSprite = layerPortasLargas;
                break;
            case Layers.PessoaMuda:
                toActiveSprite= layerPessoaMuda;
                break;
        }

        animateCamera = true;
    }

    private void LerpLayer()
    {
        if (toActiveSprite == layerNanismo)
        {
            toActiveSprite.color = Color.Lerp(toActiveSprite.color, new Color(1, 1, 1, 0), Time.deltaTime * 10);
            if (toActiveSprite.color == new Color(1,1,1,0) )
            {
                toActiveSprite = null;
            }
        }
        else
        {
            toActiveSprite.color = Color.Lerp(toActiveSprite.color, Color.white, Time.deltaTime * 10);
            if (toActiveSprite.color == Color.white)
            {
                toActiveSprite = null;
            }
        }
               
    }
}

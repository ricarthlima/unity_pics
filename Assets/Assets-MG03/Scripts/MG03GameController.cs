using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MG03GameController : MonoBehaviour
{
    [Header("Controlllers")]
    [SerializeField] MG03CardController cardController;

    [Header("Camera")]
    [SerializeField] private Camera m_Camera;

    [Header("UI")]
    [SerializeField] private GameObject uiGameObject;
    [SerializeField] private GameObject uiIndicadorAcessibilidadeObjetivo;

    [Header("Prefabs")]
    [SerializeField] private GameObject areaClickPrefab;
    

    [Header("Game")]
    public bool canInteract = false;
    [SerializeField] int round = 0;
    bool moveCamera = false;
    List<Layers> remainingLayers = new List<Layers>() 
    { Layers.AuditivoLibras, Layers.AuditivoMapa, 
        Layers.FisicoAreaReservada, Layers.FisicoRampa , 
        Layers.FisicoPortasLargas, Layers.Nanismo, 
        Layers.VisualBraille, Layers.VisualPiso,};

    void Start()
    {
        uiGameObject.SetActive(false);
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
                uiGameObject.SetActive(true);
                StartGame();
            }
        }  
        
        if (moveCamera)
        {
            m_Camera.orthographicSize -= 1.7f * Time.deltaTime;
            m_Camera.transform.position += 1.7f * Vector3.up  * Time.deltaTime;
            if (m_Camera.orthographicSize <= 4.1 && m_Camera.transform.position.y > - 1.5)
            {               
                moveCamera = false;
            }
        }
    }

    void StartGame()
    {
        cardController.ShowCard(round);
    }
    

    public void CorrectCardSelect()
    {       
        cardController.HideCard();

        uiIndicadorAcessibilidadeObjetivo.SetActive(true);
        moveCamera = true;

        foreach (Layers layer in remainingLayers)
        {
            areaClickPrefab.GetComponent<MG03AreaClickController>().layer = layer;
            Instantiate(areaClickPrefab);
        }
    }

    public void CorrectClickedDashedArea()
    {
        uiIndicadorAcessibilidadeObjetivo.SetActive(false);

        //TESTE
        // TODO: Testar condição de vitória
        round += 1;
        cardController.ShowCard(round);
    }

}

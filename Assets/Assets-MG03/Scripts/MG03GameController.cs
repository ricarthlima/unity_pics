using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MG03GameController : MonoBehaviour
{
    [Header("Controlllers")]
    [SerializeField] MG03CardController cardController;
    [SerializeField] private MG03InGameAreasController inGameAreasController;

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
        VerifyClick();
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

    void VerifyClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Area"))
                    {
                        OnAreaTouched(hit.collider.gameObject);
                    }
                }
            }
        }
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

    void OnAreaTouched(GameObject areaTouched)
    {
        Layers layerFromTouch = areaTouched.GetComponent<MG03AreaClickController>().layer;

        List<Layers> correctList = new List<Layers>();
        switch (round)
        {
            case 0:
                correctList.Add(Layers.FisicoPortasLargas);
                correctList.Add(Layers.FisicoRampa);
                correctList.Add(Layers.FisicoAreaReservada);
                break; 
            case 1:
                correctList.Add(Layers.VisualPiso);
                correctList.Add(Layers.VisualBraille);
                break; 
            case 2:
                correctList.Add(Layers.AuditivoLibras);
                correctList.Add(Layers.AuditivoMapa);
                break; 
            case 3:
                correctList.Add(Layers.FisicoPortasLargas);
                correctList.Add(Layers.FisicoRampa);
                correctList.Add(Layers.FisicoAreaReservada);
                break; 
            case 4:
                correctList.Add(Layers.Nanismo);
                break; 
            case 5:
                correctList.Add(Layers.AuditivoLibras);
                correctList.Add(Layers.AuditivoMapa);
                break;
            case 6:
                correctList.Add(Layers.VisualPiso);
                correctList.Add(Layers.VisualBraille);
                break;
            case 7:
                correctList.Add(Layers.FisicoPortasLargas);
                correctList.Add(Layers.FisicoRampa);
                correctList.Add(Layers.FisicoAreaReservada);
                break;
        }

        if (correctList.Contains(layerFromTouch))
        {
            CorrectClickedDashedArea(layerFromTouch);
        }
        else
        {
            areaTouched.GetComponent<MG03AreaClickController>().IncorrectClick();
        }
    }

    public void CorrectClickedDashedArea(Layers layer)
    {
        remainingLayers.Remove(layer);
        uiIndicadorAcessibilidadeObjetivo.SetActive(false);
        DestroyAllAreas();

        inGameAreasController.OnClickAnimation(layer);    

    }

    public void ToNextRound()
    {
        //TESTE
        // TODO: Testar condição de vitória
        round += 1;
        cardController.ShowCard(round);
    }

    void DestroyAllAreas()
    {
        GameObject[] listAreas = GameObject.FindGameObjectsWithTag("Area");
        foreach (GameObject area in listAreas)
        {
            Destroy(area);
        }
    }
}

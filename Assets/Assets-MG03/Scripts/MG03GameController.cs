using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MG03GameController : MonoBehaviour
{
    [Header("Controlllers")]
    [SerializeField] MG03CardController cardController;
    [SerializeField] private MG03LayerShowController inGameAreasController;
    [SerializeField] private MG03CameraController cameraController;



    [Header("UI")]
    [SerializeField] private GameObject uiGameObject;
    [SerializeField] private GameObject uiIndicadorAcessibilidadeObjetivo;
    [SerializeField] private GameObject uiPanelWin;

    [Header("Prefabs")]
    [SerializeField] private GameObject areaClickPrefab;
    

    [Header("Game")]
    public bool canInteract = false;
    [SerializeField] int round = 0;


    List<MG03Areas> remainingLayers = new List<MG03Areas>() 
    { MG03Areas.AuditivoLibras, MG03Areas.AuditivoMapa, 
        MG03Areas.FisicoAreaReservada, MG03Areas.FisicoRampa , 
        MG03Areas.FisicoPortasLargas, MG03Areas.Nanismo, 
        MG03Areas.VisualBraille, MG03Areas.VisualPiso,};

    void Start()
    {
        uiGameObject.SetActive(false);
        cameraController.StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        VerifyClick();
        
    }

    public void StartGame()
    {
        uiGameObject.SetActive(true);
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

        cameraController.AreaAnimation();        

        uiIndicadorAcessibilidadeObjetivo.SetActive(true);

        foreach (MG03Areas layer in remainingLayers)
        {
            areaClickPrefab.GetComponent<MG03AreaTracejadaController>().area = layer;
            Instantiate(areaClickPrefab);
        }
    }

    void OnAreaTouched(GameObject areaTouched)
    {
        MG03Areas layerFromTouch = areaTouched.GetComponent<MG03AreaTracejadaController>().area;

        List<MG03Areas> correctList = new List<MG03Areas>();
        switch (round)
        {
            case 0:
                correctList.Add(MG03Areas.FisicoPortasLargas);
                correctList.Add(MG03Areas.FisicoRampa);
                correctList.Add(MG03Areas.FisicoAreaReservada);
                break; 
            case 1:
                correctList.Add(MG03Areas.VisualPiso);
                correctList.Add(MG03Areas.VisualBraille);
                break; 
            case 2:
                correctList.Add(MG03Areas.AuditivoLibras);
                correctList.Add(MG03Areas.AuditivoMapa);
                break; 
            case 3:
                correctList.Add(MG03Areas.FisicoPortasLargas);
                correctList.Add(MG03Areas.FisicoRampa);
                correctList.Add(MG03Areas.FisicoAreaReservada);
                break; 
            case 4:
                correctList.Add(MG03Areas.Nanismo);
                break; 
            case 5:
                correctList.Add(MG03Areas.AuditivoLibras);
                correctList.Add(MG03Areas.AuditivoMapa);
                break;
            case 6:
                correctList.Add(MG03Areas.VisualPiso);
                correctList.Add(MG03Areas.VisualBraille);
                break;
            case 7:
                correctList.Add(MG03Areas.FisicoPortasLargas);
                correctList.Add(MG03Areas.FisicoRampa);
                correctList.Add(MG03Areas.FisicoAreaReservada);
                break;
        }

        if (correctList.Contains(layerFromTouch))
        {
            CorrectClickedDashedArea(layerFromTouch, areaTouched);
        }
        else
        {
            areaTouched.GetComponent<MG03AreaTracejadaController>().IncorrectClick();
        }
    }

    public void CorrectClickedDashedArea(MG03Areas layer, GameObject areaTouched)
    {
        remainingLayers.Remove(layer);
        uiIndicadorAcessibilidadeObjetivo.SetActive(false);
        DestroyAllAreas();

        inGameAreasController.OnClickAnimation(layer, areaTouched);
    }

    public void ToNextRound()
    {
        // TODO: Testar condição de vitória
        if (remainingLayers.Count == 0)
        {
            uiPanelWin.SetActive(true);
        }
        else
        {
            round += 1;
            cardController.ShowCard(round);
        }        
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

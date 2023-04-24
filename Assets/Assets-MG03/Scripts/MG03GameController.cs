using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MG03GameController : MonoBehaviour
{
    [Header("Controlllers")]
    [SerializeField] MG03CardController cardController;
    [SerializeField] private MG03InGameAreasController inGameAreasController;
    [SerializeField] private MG03CameraController cameraController;



    [Header("UI")]
    [SerializeField] private GameObject uiGameObject;
    [SerializeField] private GameObject uiIndicadorAcessibilidadeObjetivo;

    [Header("Prefabs")]
    [SerializeField] private GameObject areaClickPrefab;
    

    [Header("Game")]
    public bool canInteract = false;
    [SerializeField] int round = 0;


    List<Layers> remainingLayers = new List<Layers>() 
    { Layers.AuditivoLibras, Layers.AuditivoMapa, 
        Layers.FisicoAreaReservada, Layers.FisicoRampa , 
        Layers.FisicoPortasLargas, Layers.Nanismo, 
        Layers.VisualBraille, Layers.VisualPiso,};

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
            CorrectClickedDashedArea(layerFromTouch, areaTouched);
        }
        else
        {
            areaTouched.GetComponent<MG03AreaClickController>().IncorrectClick();
        }
    }

    public void CorrectClickedDashedArea(Layers layer, GameObject areaTouched)
    {
        remainingLayers.Remove(layer);
        uiIndicadorAcessibilidadeObjetivo.SetActive(false);
        DestroyAllAreas();

        inGameAreasController.OnClickAnimation(layer, areaTouched);
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

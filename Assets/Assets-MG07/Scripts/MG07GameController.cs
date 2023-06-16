using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG07GameController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private List<GameObject> listDirt;

    [Header("Prefabs")]
    [SerializeField] private GameObject animationCleanPrefab;

    int gamePhase = 1; // 1 - Clean | 2 - Decorate | 3 - Grow
    bool canClick = true;

    // Phase One
    int activeDirtObjects = 0;

    // Phase Two
    [Header("Decoration Phase")]
    [SerializeField] private GameObject decorationIconGroup;
    [SerializeField] private GameObject uiCardWall, uiCardVase, uiCardGarden, uiCardHouse, uiCardTree;
    [SerializeField] private GameObject animationWall, animationVase, animationGarden, animationHouse, animationTree;
    [SerializeField] private GameObject vaseA, vaseB, wallA, wallB, gardenA, gardenB, houseOld, houseA, houseB, treeOld, treeA, treeB;
    bool isChoosed = false;

    void Start()
    {
        PhaseOneShowDirtObject();
        PhaseOneShowDirtObject();
    }

    void Update()
    {
        VerifyClickDown();
    }

    #region "Phase One - Clean"

    void PhaseOneShowDirtObject()
    {
        int randomIndex = Random.Range(0, listDirt.Count - 1);
        listDirt[randomIndex].GetComponent<MG07DirtController>().Activate();
        listDirt.RemoveAt(randomIndex);

        activeDirtObjects++;
    }

    public void PhaseOneDirtClicked()
    {
        activeDirtObjects--;
        if (listDirt.Count == 0)
        {
            if (activeDirtObjects == 0)
            {
                PhaseTwoStart();
            }
        }
        else
        {
            PhaseOneShowDirtObject();            
        }        
    }

    #endregion

    #region "Phase One - Decoration"
    void PhaseTwoStart()
    {
        // Passar de fase
        gamePhase = 2;
        decorationIconGroup.SetActive(true);
    }

    void PhaseTwoShowCard(MG07DecorationEnum decoration)
    {
        canClick = false;
        decorationIconGroup.SetActive(false);

        switch (decoration)
        {
            case MG07DecorationEnum.Wall:
            {
                    uiCardWall.SetActive(true);
                    break;
            }

            case MG07DecorationEnum.Garden:
                {
                    uiCardGarden.SetActive(true);
                    break;
                }

            case MG07DecorationEnum.Tree:
                {
                    uiCardTree.SetActive(true);
                    break;
                }

            case MG07DecorationEnum.Vase:
                {
                    uiCardVase.SetActive(true);
                    break;
                }

            case MG07DecorationEnum.House:
                {
                    uiCardHouse.SetActive(true);    
                    break;
                }
        } 
    }

    public void PhaseTwoHouse(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.House, choice);
        animationHouse.SetActive(true);
        PhaseTwoHighlight(uiCardHouse, choice);
    }
    public void PhaseTwoWall(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.Wall, choice);
        animationWall.SetActive(true);
        PhaseTwoHighlight(uiCardWall, choice);
    }

    public void PhaseTwoGarden(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.Garden, choice);
        animationGarden.SetActive(true);
        PhaseTwoHighlight(uiCardGarden, choice);
    }

    public void PhaseTwoTree(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.Tree, choice);
        animationTree.SetActive(true);
        PhaseTwoHighlight(uiCardTree, choice);
    }

    public void PhaseTwoVase(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.Vase, choice);
        animationVase.SetActive(true);
        PhaseTwoHighlight(uiCardVase, choice);
    }

    void PhaseTwoHighlight(GameObject panel, int choice)
    {
        GameObject highlightA = panel.transform.Find("HighlightA").gameObject;
        GameObject highlightB = panel.transform.Find("HighlightB").gameObject;

        if (choice == 0)
        {
            highlightA.SetActive(true);
            highlightB.SetActive(false);
        }
        else
        {
            highlightA.SetActive(false);
            highlightB.SetActive(true);
        }
    }

    public void PhaseTwoButtonOKClicked()
    {
        if (isChoosed)
        {
            canClick = true;
            decorationIconGroup.SetActive(true);
            uiCardGarden.SetActive(false);
            uiCardHouse.SetActive(false);
            uiCardTree.SetActive(false);
            uiCardVase.SetActive(false);
            uiCardWall.SetActive(false);
            isChoosed = false;
        }        
    }

    void PhaseTwoChoiceClicked(MG07DecorationEnum decoration, int choice)
    {
        isChoosed = true;
        switch (decoration)
        {
            case MG07DecorationEnum.Wall:
                {
                    if (choice == 0)
                    {
                        wallA.SetActive(true);
                        wallB.SetActive(false);
                    }
                    else
                    {
                        wallA.SetActive(false);
                        wallB.SetActive(true);
                    }
                    break;
                }

            case MG07DecorationEnum.Garden:
                {
                    if (choice == 0)
                    {
                        gardenA.SetActive(true);
                        gardenB.SetActive(false);
                    }
                    else
                    {
                        gardenA.SetActive(false);
                        gardenB.SetActive(true);
                    }
                    break;
                }

            case MG07DecorationEnum.Tree:
                {
                    treeOld.GetComponent<Animator>().SetTrigger("OnHide");

                    if (choice == 0)
                    {
                        treeA.SetActive(true);
                        treeB.SetActive(false);
                    }
                    else
                    {
                        treeA.SetActive(false);
                        treeB.SetActive(true);
                    }
                    break;
                }

            case MG07DecorationEnum.Vase:
                {
                    if (choice == 0)
                    {
                        vaseA.SetActive(true);
                        vaseB.SetActive(false);
                    }
                    else
                    {
                        vaseA.SetActive(false);
                        vaseB.SetActive(true);
                    }
                    break;
                }

            case MG07DecorationEnum.House:
                {
                    houseOld.GetComponent<Animator>().SetTrigger("OnHide");
                    if (choice == 0)
                    {
                        houseA.SetActive(true);
                        houseB.SetActive(false);
                    }
                    else
                    {
                        houseA.SetActive(false);
                        houseB.SetActive(true);
                    }
                    break;
                }
        }
    }
    #endregion

    #region Click and Touch
    void VerifyClickDown()
    {
        if (canClick)
        {
            bool isClicked = false;
            bool isMouse = false;
            Vector2 touchPosition = Vector2.negativeInfinity;

            if (Input.GetMouseButtonDown(0))
            {
                touchPosition = Input.mousePosition;
                isClicked = true;
                isMouse = true;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchPosition = touch.position;
                    isClicked = true;
                }
            }

            if (isClicked)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Dirt"))
                        {
                            MG07DirtController controller = hit.collider.gameObject.GetComponent<MG07DirtController>();
                            if (controller.isActive)
                            {
                                PhaseOneDirtClicked();
                                controller.Clicked(animationCleanPrefab);
                            }
                        }

                        if (hit.collider.CompareTag("Decoration"))
                        {
                            MG07DecorationIconController controller = hit.collider.gameObject.GetComponent<MG07DecorationIconController>();
                            PhaseTwoShowCard(controller.decoration);
                            Destroy(controller.gameObject);
                        }
                    }
                }
            }
        }
    }
    #endregion
}

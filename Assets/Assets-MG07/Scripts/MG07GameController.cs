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
        uiCardHouse.SetActive(false);
    }
    public void PhaseTwoWall(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.Wall, choice);
        animationWall.SetActive(true);
        uiCardWall.SetActive(false);
    }

    public void PhaseTwoGarden(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.Garden, choice);
        animationGarden.SetActive(true);
        uiCardGarden.SetActive(false);
    }

    public void PhaseTwoTree(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.Tree, choice);
        animationTree.SetActive(true);
        uiCardTree.SetActive(false);
    }

    public void PhaseTwoVase(int choice)
    {
        PhaseTwoChoiceClicked(MG07DecorationEnum.Vase, choice);
        animationVase.SetActive(true);
        uiCardVase.SetActive(false);
    }


    void PhaseTwoChoiceClicked(MG07DecorationEnum decoration, int choice)
    {
        canClick = true;
        decorationIconGroup.SetActive(true);

        switch (decoration)
        {
            case MG07DecorationEnum.Wall:
                {
                    if (choice == 0)
                    {
                        wallA.SetActive(true);
                    }
                    else
                    {
                        wallB.SetActive(true);
                    }
                    break;
                }

            case MG07DecorationEnum.Garden:
                {
                    if (choice == 0)
                    {
                        gardenA.SetActive(true);
                    }
                    else
                    {
                        gardenB.SetActive(true);
                    }
                    break;
                }

            case MG07DecorationEnum.Tree:
                {
                    //treeOld.SetActive(false);

                    if (choice == 0)
                    {
                        treeA.SetActive(true);
                    }
                    else
                    {
                        treeB.SetActive(true);
                    }
                    break;
                }

            case MG07DecorationEnum.Vase:
                {
                    if (choice == 0)
                    {
                        vaseA.SetActive(true);
                    }
                    else
                    {
                        vaseB.SetActive(true);
                    }
                    break;
                }

            case MG07DecorationEnum.House:
                {
                    //houseOld.SetActive(false);
                    if (choice == 0)
                    {
                        houseA.SetActive(true);
                    }
                    else
                    {
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

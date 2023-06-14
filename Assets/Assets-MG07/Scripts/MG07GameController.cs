using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG07GameController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private List<GameObject> listDirt;

    [Header("Prefabs")]
    [SerializeField] private GameObject animationCleanPrefab;

    int gamePhase = 0; // 0 - Clean | 
    bool canClick = true;

    // Phase Zero

    void Start()
    {
        PhaseZeroShowDirtObject();
        PhaseZeroShowDirtObject();
    }

    void Update()
    {
        VerifyClickDown();
    }

    void PhaseZeroShowDirtObject()
    {
        int randomIndex = Random.Range(0, listDirt.Count - 1);
        listDirt[randomIndex].GetComponent<MG07DirtController>().Activate();
        listDirt.RemoveAt(randomIndex);
    }

    public void PhaseZeroDirtClicked()
    {
        if (listDirt.Count == 0)
        {
            // Passar de fase
            gamePhase = 1;
        }
        else
        {
            PhaseZeroShowDirtObject();
        }
        
    }

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
                                PhaseZeroDirtClicked();
                                controller.Clicked(animationCleanPrefab);
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MG06GameController : MonoBehaviour
{
    bool isClickedWithMouse = true;
    GameObject lastFlowerTouched;

    [SerializeField] private GameObject panelEndgame;

    [Header("Flowers")]
    [SerializeField] private GameObject[] listFlowers;
    [SerializeField] private GameObject[] listCactos;
    int round = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VerifyClickDown();
        VerifyClickUp();
    }

    public void FlowerCicleComplete()
    {
        round++;

        if (round >= listFlowers.Length)
        {
            EndGame();
        }
        else
        {
            listFlowers[round].gameObject.SetActive(true);
            UpdateCacto();
        }
        
    }

    void UpdateCacto()
    {
        int cactoAtual = Mathf.FloorToInt(round / 2);
        for (int i = 0; i < listCactos.Length; i++) {
            if (i == cactoAtual)
            {
                listCactos[i].gameObject.SetActive(true);
            }
            else
            {
                //listCactos[i].gameObject.SetActive(false);
            }
        }

    }

    void EndGame()
    {
        panelEndgame.SetActive(true);
    }

    #region "Clicks"
    void VerifyClickDown()
    {
        bool isClicked = false;
        Vector2 touchPosition = Vector2.negativeInfinity;

        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            isClicked = true;
            isClickedWithMouse = true;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
                isClicked = true;
                isClickedWithMouse = false;
            }
        }

        if (isClicked)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Flor"))
                    {
                        hit.collider.gameObject.GetComponent<MG06FlorController>().StartedClick();
                        lastFlowerTouched = hit.collider.gameObject;
                    }
                }
            }
        }
    }

    void VerifyClickUp()
    {
        bool needToStopFlower = false;

        if (isClickedWithMouse)
        {
            if (Input.GetMouseButtonUp(0))
            {
                needToStopFlower = true;
            }
        } else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    needToStopFlower = true;
                }
            }
            else
            {
                needToStopFlower = true;
            }
        }

        if (needToStopFlower)
        {
            lastFlowerTouched.GetComponent<MG06FlorController>().StopedClick();
        }
    }
    #endregion
}

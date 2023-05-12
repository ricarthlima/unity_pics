using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG05AgulhaController : MonoBehaviour
{
    [HideInInspector] public MG05GameController gameController;

    bool isFollowingByMouse = false;
    bool isFollowingByTouch = false;

    [SerializeField] GameObject pontaAgulha;
    MG05PontaAgulhaController pontaAgulhaController;

    private void Start()
    {
        pontaAgulhaController = pontaAgulha.GetComponent<MG05PontaAgulhaController>();  
    }

    void Update()
    {
        Follow();
        VerifyClickUp();
    }

    public void FollowPosition(bool isMouse)
    {
        if (isMouse)
        {
            isFollowingByMouse = true;
        }
        else
        {
            isFollowingByTouch = true;
        }
               
    }

    void Follow()
    {

        if (isFollowingByMouse)
        {           
            if (Input.GetMouseButton(0))
            {
                transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (isFollowingByTouch)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
                }
            }

        }

    }

    void VerifyClickUp()
    {
        if (isFollowingByMouse)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isFollowingByMouse = false;
                VerifyPonta();
            }
        }

        if (isFollowingByTouch)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    isFollowingByTouch = false;
                    VerifyPonta();
                }
            }
            else
            {
                isFollowingByTouch = false;
                VerifyPonta();
            }
        }               
    }

    void VerifyPonta()
    {
        gameController.TouchedArea(pontaAgulhaController.parteTocada, gameObject);
    
    }
}


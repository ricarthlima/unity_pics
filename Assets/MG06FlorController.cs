using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG06FlorController : MonoBehaviour
{
    

    [Header("Controllers")]
    [SerializeField] private MG06GameController gameController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject backgroundFlower;

    [Header("Other Flowers")]
    [SerializeField] private GameObject flowerSmaller;
    [SerializeField] private GameObject flowerAnimation;
    [SerializeField] private GameObject flowerComplete;


    
    [HideInInspector] public bool canClick = true;
    bool isStatedClick = false;
    float timePressing = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (isStatedClick)
        {
            timePressing += Time.deltaTime;

            if (timePressing >= 7)
            {
                EndedCicle();
            }
        }
    }

    public void StartedClick()
    {
        if (canClick && !isStatedClick)
        {
            isStatedClick = true;
            spriteRenderer.color = new Color(0, 0, 0, 0);
            flowerSmaller.SetActive(true);
            flowerAnimation.SetActive(true);
            backgroundFlower.SetActive(true);
        }
    }

    public void StopedClick()
    {
        ResetFlower(false);
        isStatedClick = false;
        timePressing = 0;
    }

    void EndedCicle()
    {
        ResetFlower(true);
        canClick = false;
        isStatedClick = false;
        gameController.FlowerCicleComplete();        
        flowerComplete.SetActive(true);
    }

    void ResetFlower(bool keepTransparent)
    {
        if (!keepTransparent)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        flowerSmaller.SetActive(false);
        flowerAnimation.SetActive(false);

        backgroundFlower.SetActive(false);
    }
}

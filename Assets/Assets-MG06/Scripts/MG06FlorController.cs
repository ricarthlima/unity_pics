using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MG06FlorController : MonoBehaviour
{   

    [Header("Controllers")]
    [SerializeField] private MG06GameController gameController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject backgroundFlower;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI textInspire;
    [SerializeField] private TextMeshProUGUI textSegure;
    [SerializeField] private TextMeshProUGUI textExpire;


    [Header("Other Flowers")]
    [SerializeField] private GameObject flowerSmaller;
    [SerializeField] private GameObject flowerAnimation;
    [SerializeField] private GameObject flowerComplete;


    
    [HideInInspector] public bool canClick = true;
    bool isStatedClick = false;
    float timePressing = 0;


    float timeInspirando = 4;
    float timeSegurando = 6;    // (total) + 1
    float timeExpirando = 11;   // (total) + 5 

    void Start()
    {
        
    }

    void Update()
    {
        if (isStatedClick)
        {
            timePressing += Time.deltaTime;            

            // Fase de inspirar
            if (timePressing <= timeInspirando)
            {
                // Ativar texto de inspirar
                if (!textInspire.gameObject.activeSelf)
                {
                    textInspire.gameObject.SetActive(true);
                }
            }

            // Fase de segurar
            if ((timePressing > timeInspirando) && (timePressing <= timeSegurando))
            {
                // Desativar texto de inspirar
                if (textInspire.gameObject.activeSelf)
                {
                    textInspire.gameObject.SetActive(false);
                }

                // Ativar texto de segurar
                if (!textSegure.gameObject.activeSelf)
                {
                    textSegure.gameObject.SetActive(true);
                }
            }

            // Fase de expirar
            if ((timePressing > timeSegurando) && (timePressing <= timeExpirando))
            {
                // Desativar texto de segurar
                if (textSegure.gameObject.activeSelf) 
                {
                    textSegure.gameObject.SetActive(false);
                }

                // Ativar texto de segurar
                if (!textExpire.gameObject.activeSelf)
                {
                    textExpire.gameObject.SetActive(true);
                }
            }

            // Final do ciclo
            if (timePressing >= timeExpirando)
            {
                // Desativar texto de segurar
                textExpire.gameObject.SetActive(false);

                // Fim do ciclo
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

        textInspire.gameObject.SetActive(false);
        textExpire.gameObject.SetActive(false);
        textSegure.gameObject.SetActive(false);
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

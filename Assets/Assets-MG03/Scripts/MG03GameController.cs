using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MG03GameController : MonoBehaviour
{
    [Header("Controlllers")]
    [SerializeField] MG03CardController cardController;

    [Header("Camera")]
    [SerializeField] private Camera m_Camera;

    [Header("UI")]
    [SerializeField] private GameObject uiGameObject;
    

    [Header("Game")]
    public bool canInteract = false;
    [SerializeField] int round = 0;

    void Start()
    {
        uiGameObject.SetActive(false);
        m_Camera.gameObject.transform.position = new Vector3(-1.51f, -3f, -10);
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void StartGame()
    {
        cardController.ShowCard(round);
    }

   

    

    

    public void CorrectCardSelect()
    {       
        cardController.HideCard();

        //TESTE
        // TODO: Testar condição de vitória
        round += 1;
        cardController.ShowCard(round);
    }

}

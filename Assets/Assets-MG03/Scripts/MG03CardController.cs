using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG03CardController : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject groupCard;

    [Header("Persons")]
    [SerializeField] private RawImage imagePerson;
    [SerializeField] private Texture[] listPersonByRound;

    [Header("Buttons")]
    [SerializeField] private Button[] listButtons;

  
    [SerializeField] private Sprite iconCadeirante, iconVisual, iconInterpreteLibras, iconSurdez, iconNanismo;

    int correctButton = -1;
    

    void Start()
    {
        groupCard.SetActive(false);
    }

    
    public void ShowCard(int round)
    {
        // Por a pessoa no card
        imagePerson.texture = listPersonByRound[0];

        // Sortear o botão correto
        correctButton = Random.Range(0, 2);

        // Mostrar icone nos botões
        List<Sprite> buttonSprites =  new List<Sprite> {iconCadeirante, iconVisual, iconInterpreteLibras, iconSurdez, iconNanismo};

        switch (round)
        {
            case 0:
                listButtons[correctButton].image.sprite = iconCadeirante;
                buttonSprites.Remove(iconCadeirante);
                break;
        }

        for (int i = 0; i < listButtons.Length; i++)
        {
            if (i != correctButton)
            {   
                int selectedIndex = Random.Range(0, buttonSprites.Count - 1);
                listButtons[i].image.sprite = buttonSprites[selectedIndex];
                buttonSprites.RemoveAt(selectedIndex);
            }
        }

        // Mostrar na tela
        groupCard.SetActive(true);
    }

    public void HideCard()
    {
        groupCard.SetActive(false);
    }

    public void ResetCard()
    {
        groupCard.SetActive(false);

        correctButton = -1;

        foreach (Button button in listButtons)
        {
            button.image.color = Color.white;
        }
    }

    public void ButtonClicked(int button)
    {
        if (button == correctButton)
        {
            //TODO: Tocar som de vitoria
        }
        else
        {
            //TODO: Tocar som de erro
            listButtons[button].image.color = Color.red;
        }
    }
}

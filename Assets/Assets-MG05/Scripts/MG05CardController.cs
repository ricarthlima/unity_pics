using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG05CardController : MonoBehaviour
{
    [Header("ImageComponent")]
    [SerializeField] private Image image;

    [Header("Sprites")]
    [SerializeField] private Sprite cardBexiga;
    [SerializeField] private Sprite cardBoca, cardCoracao, cardEstomago, cardFigado, cardIntestino, cardMao, cardNariz, cardOlho, cardPe, cardPulmao, cardRins;

    

    public void ChangeParte(MG05PartesCorpo parte)
    {
        switch (parte)
        {
            case MG05PartesCorpo.Bexiga:
                {
                    image.sprite = cardBexiga;
                    break;
                }

            case MG05PartesCorpo.Nariz:
                {
                    image.sprite = cardNariz;
                    break;
                }

            case MG05PartesCorpo.Pe:
                {
                    image.sprite = cardPe;
                    break;
                }

            case MG05PartesCorpo.Coracao:
                {
                    image.sprite = cardCoracao;
                    break;
                }

            case MG05PartesCorpo.Pulmao:
                {
                    image.sprite = cardPulmao;
                    break;
                }

            case MG05PartesCorpo.Rins:
                {
                    image.sprite = cardRins;
                    break;
                }

            case MG05PartesCorpo.Estomago:
                {
                    image.sprite = cardEstomago;
                    break;
                }

            case MG05PartesCorpo.Boca:
                {
                    image.sprite = cardBoca;
                    break;
                }

            case MG05PartesCorpo.Mao:
                {
                    image.sprite = cardMao;
                    break;
                }

            case MG05PartesCorpo.Instestino:
                {
                    image.sprite = cardIntestino;
                    break;
                }

            case MG05PartesCorpo.Figado:
                {
                    image.sprite = cardFigado;
                    break;
                }

            case MG05PartesCorpo.Olho:
                {
                    image.sprite = cardOlho;
                    break;
                }

        }
    }
}

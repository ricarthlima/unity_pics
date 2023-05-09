using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG05GridController : MonoBehaviour
{
    [Header("Controladores")]
    [SerializeField] private MG05GameController gameController;

    [Header("Grids")]
    public GameObject gridUI;

    [Header("Cards Visíveis")]
    [SerializeField] private GameObject cardBexiga; 
    [SerializeField] private GameObject cardBoca, cardCoracao, cardEstomago, cardFigado, cardIntestino, cardMao, cardNariz, cardOlho, cardPe, cardPulmao, cardRins;

    List<MG05PartesCorpo> listPartes = new List<MG05PartesCorpo>() {
    MG05PartesCorpo.Bexiga,
    MG05PartesCorpo.Boca,
    MG05PartesCorpo.Coracao,
    MG05PartesCorpo.Estomago,

    MG05PartesCorpo.Figado,
    MG05PartesCorpo.Instestino,
    MG05PartesCorpo.Mao,
    MG05PartesCorpo.Nariz,

    MG05PartesCorpo.Olho,
    MG05PartesCorpo.Pe,
    MG05PartesCorpo.Pulmao,
    MG05PartesCorpo.Rins,
    };

    public void SortearGrid()
    {
        //TODO: Esconder com GridEscondidos
        //TODO: Embaralhar listPartes
        //TODO: Gerar a depender da ordem, adicionar no gridUI
        //TODO: Mostrar por um tempo (Remover GridEscondidos?)
        //TODO: Cobrir com Escondidos
    }

    public void ButtonClicked(int posicao)
    {
        //TODO: Sumir com o escondido específico

        bool isCorrect = gameController.ClickedMemoria(listPartes[posicao]);

        if (!isCorrect)
        {
            CardErrado(posicao);
        }
    }

    public void CardErrado(int posicao)
    {
        //TODO: Voltar com escondido e deixar vermelho
    }
}

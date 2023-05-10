using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MG05GridController : MonoBehaviour
{
    [Header("Controladores")]
    [SerializeField] private MG05GameController gameController;

    [Header("Grids")]
    public GameObject[] listCards;
    public GameObject[] listGridEscondidos;

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

    bool canClick;

    public void SortearGrid()
    {
        canClick = false;

        // Esconder com GridEscondidos
        foreach (GameObject cardEscondido in listGridEscondidos)
        {
            ToggleMostrarEscondidos(true);
        }

        // TODO: Embaralhar listPartes
        // listPartes.Sort((a, b) => Random.Range(-1, 2));  
        listPartes = RandomizeList(listPartes);

        // Atualizar cards com a ordem gerada
        for (int i = 0; i < listPartes.Count; i++)
        {
            listCards[i].GetComponent<MG05CardController>().ChangeParte(listPartes[i]);
        }

        // Mostrar por um tempo (Remover GridEscondidos?)
        foreach (GameObject cardEscondido in listGridEscondidos)
        {
            ToggleMostrarEscondidos(false);
        }

        // Cobrir com Escondidos
        StartCoroutine(EspereParaEsconder(5));
    }

    public void ButtonClicked(int posicao)
    {
        if (canClick)
        {
            canClick = false;

            // Sumir com o escondido específico
            listGridEscondidos[posicao].GetComponent<Image>().enabled = false;

            bool isCorrect = gameController.ClickedMemoria(listPartes[posicao]);

            if (!isCorrect)
            {
                StartCoroutine(CardErrado(posicao));
            }
        }
        
    }

    IEnumerator CardErrado(int posicao)
    {
        // Voltar com escondido e deixar vermelho
        yield return new WaitForSeconds(1);
        listGridEscondidos[posicao].GetComponent<Image>().enabled = true;
        listGridEscondidos[posicao].GetComponent<Image>().color = Color.red;
        canClick = true;
    }

    void ToggleMostrarEscondidos(bool show)
    {
        foreach (GameObject cardEscondido in listGridEscondidos)
        {
            cardEscondido.SetActive(show);
            if (show)
            {
                cardEscondido.GetComponent<Image>().enabled = true;
                cardEscondido.GetComponent<Image>().color = Color.white;
            }
        }
    }

    IEnumerator EspereParaEsconder(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        ToggleMostrarEscondidos(true);
        canClick = true;
    }

    List<MG05PartesCorpo> RandomizeList(List<MG05PartesCorpo> list)
    {
        List<MG05PartesCorpo> result = new List<MG05PartesCorpo>() { 
            MG05PartesCorpo.Bexiga, 
            MG05PartesCorpo.Bexiga, 
            MG05PartesCorpo.Bexiga, 
            MG05PartesCorpo.Bexiga, 
            
            MG05PartesCorpo.Bexiga, 
            MG05PartesCorpo.Bexiga, 
            MG05PartesCorpo.Bexiga, 
            MG05PartesCorpo.Bexiga, 
            
            MG05PartesCorpo.Bexiga,
            MG05PartesCorpo.Bexiga,
            MG05PartesCorpo.Bexiga,
            MG05PartesCorpo.Bexiga,
        };

        List<int> positions = new List<int>();

        foreach (MG05PartesCorpo parte in list)
        {
            while (true)
            {
                int index = Random.Range(0, list.Count);
                if (!positions.Contains(index))
                {
                    result[index] = parte;
                    positions.Add(index);
                    break;
                }

            }
        }

        return result;
    }
}
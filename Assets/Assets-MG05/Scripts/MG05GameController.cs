using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MG05GameController : MonoBehaviour
{
    

    [Header("Controladores")]
    public MG05GridController gridController = new MG05GridController();

    [Header("Objetos")]
    [SerializeField] private GameObject agulhaPrefab;
    [SerializeField] private GameObject indicadoresOrelha;
    [SerializeField] private TextMeshProUGUI textParteIndicator;
    
    MG05PartesCorpo parteAtual;

    List<MG05PartesCorpo> listPartesFaltantes = new List<MG05PartesCorpo>() {
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
    

    private void Start()
    {
        StartMemoria();
    }

    void StartMemoria()
    {
        // Ativa os indicadores na orelha
        indicadoresOrelha.SetActive(true);

        // Sorteia uma parte que não foi usada ainda
        int posicao = Random.Range(0, listPartesFaltantes.Count);
        parteAtual = listPartesFaltantes[posicao];

        // Mostra na tela a parte sorteada
        textParteIndicator.text = parteAtual.ToString();

        // Configura o Grid
        gridController.SortearGrid();
    }

    public bool ClickedMemoria(MG05PartesCorpo parte)
    {
        if (parte == parteAtual)
        {
            StartAgulha();
            return true;
        }
        else
        {
            //TODO: Quando clicar algo errado.
            return false;
        }
    }

    void StartAgulha()
    {
        //TODO: Esperar alguns segundos

        //TODO: Mostrar indicação em tela

        //Ativar a agulha
        Instantiate(agulhaPrefab, Vector2.zero, Quaternion.identity);

        //Sumir com indicadores na orelha
        indicadoresOrelha.SetActive(false);
    }

    public void TouchedArea(MG05PartesCorpo parte)
    {
        //TODO: Ao toque, destruir a agulha

        if (parte == parteAtual)
        {
            //TODO: Contabilizar ponto            

            //TODO: MOstrar indicação

            listPartesFaltantes.Remove(parte);

            if (listPartesFaltantes.Count == 0)
            {
                //TODO: END GAME
            }
            else
            {
                StartMemoria();
            }
        }
        else
        {
            Instantiate(agulhaPrefab, Vector2.zero, Quaternion.identity);
        }
    }
}

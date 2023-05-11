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

    [Header("UI")]
    [SerializeField] private GameObject panelWin;
    [SerializeField] private TextMeshProUGUI textInstrucoes;
    [SerializeField] private TextMeshProUGUI textParteIndicator;

    MG05PartesCorpo parteAtual;

    [SerializeField] List<MG05PartesCorpo> listPartesFaltantes = new List<MG05PartesCorpo>() {
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

    bool canClick = true;

    bool isMostrandoPontosOrelha = false;
    float timeMostrandoPontosOrelha = 0;
    readonly float waitMostrandoPontosOrelha = 4;

    private void Start()
    {
        StartMemoria();
    }

    private void Update()
    {
        VerifyClickDown();

        // Temporizadores
        if (isMostrandoPontosOrelha)
        {
            timeMostrandoPontosOrelha += Time.deltaTime;
            if (timeMostrandoPontosOrelha > waitMostrandoPontosOrelha)
            {
                isMostrandoPontosOrelha = false;
                DelayedStartAgulha();
            }
            
        }
        
    }

    void StartMemoria()
    {
        // Manda a instrução
        textInstrucoes.text = "Memorize os órgãos";

        // Desativa os indicadores na orelha
        indicadoresOrelha.SetActive(false);

        // Sorteia uma parte que não foi usada ainda
        int posicao = Random.Range(0, listPartesFaltantes.Count);
        parteAtual = listPartesFaltantes[posicao];

        // Oculta da tela a parte sorteada
        textParteIndicator.gameObject.SetActive(false);

        // Configura o Grid
        gridController.SortearGrid();
    }

    public void FinishedStartMemoria()
    {
        // Manda a instrução
        textInstrucoes.text = "Clique no órgão solicitado";

        // Mostra parte desejada na tela
        textParteIndicator.gameObject.SetActive(true);
        textParteIndicator.text = parteAtual.ToString();
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
        // Mostrar os pontos na orelha
        indicadoresOrelha.SetActive(true);

        // Mostrar indicação em tela
        textInstrucoes.text = "Memorize os pontos de auriculoterapia";

        // Esperar alguns segundos
        timeMostrandoPontosOrelha = 0;
        isMostrandoPontosOrelha = true;
    }

    void DelayedStartAgulha()
    {
        //Ativar a agulha
        agulhaPrefab.GetComponent<MG05AgulhaController>().gameController = this;
        InstantiateAgulha();

        //Sumir com indicadores na orelha
        indicadoresOrelha.SetActive(false);

        // Manda a instrução
        textInstrucoes.text = "Espete com a agulha no ponto do órgão";

    }

    public void TouchedArea(MG05PartesCorpo? parteNulavel, GameObject agulha)
    {
        //Ao toque, destruir a agulha
        Destroy(agulha);

        if (parteNulavel != null)
        {
            MG05PartesCorpo parte = parteNulavel ?? default(MG05PartesCorpo);

            if (parte == parteAtual)
            {
                //TODO: Contabilizar ponto            

                //TODO: Mostrar indicação

                listPartesFaltantes.Remove(parte);

                if (listPartesFaltantes.Count == 0)
                {
                    // ENDGAME
                    panelWin.SetActive(true);
                }
                else
                {
                    StartMemoria();
                }
            }
            else
            {
                InstantiateAgulha();
            }
        }
        else
        {
            InstantiateAgulha();
        }

        
    }

    void InstantiateAgulha()
    {
        Instantiate(agulhaPrefab, new Vector2(-1.63f, -2.32f), Quaternion.identity);
    }

    void VerifyClickDown()
    {
        if (canClick)
        {
            bool isClicked = false;
            bool isMouse = false;
            Vector2 touchPosition = Vector2.negativeInfinity;

            if (Input.GetMouseButtonDown(0))
            {
                touchPosition = Input.mousePosition;
                isClicked = true;
                isMouse = true;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchPosition = touch.position;
                    isClicked = true;
                }
            }

            if (isClicked)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Agulha"))
                        {
                            hit.collider.gameObject.GetComponent<MG05AgulhaController>().FollowPosition(isMouse);
                        }
                    }
                }
            }
        }
    }

    
}

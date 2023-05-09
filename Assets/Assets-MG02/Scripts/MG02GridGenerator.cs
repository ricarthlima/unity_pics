using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável por gerar o grid inicial de blocos, e embaralha-lo.
/// </summary>
public class MG02GridGenerator : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private MG02GameController gameController;

    [Header("Prefabs")]
    public GameObject blockPrefab;

    [Header("Settings")]
    public int gridColumns;
    public Vector2 startPoint;
    public Vector2 sizeCell;
    public float rateStaticBlocks;

    [Header("Colors")]
    public Color[] listColorsStart;
    public Color[] listColorsEnd;

    // Gerar o grid inicial
    [HideInInspector] public List<GameObject> toShuffleBlocks = new List<GameObject>();
    [HideInInspector] public List<List<GameObject>> blockInRightPositions = new List<List<GameObject>>();
    [HideInInspector] public bool isFinishedInstantiate = false;

    // Para randomizar
    float countToShuffle = 0;
    readonly float delayToShuffle = 1.5f;
    bool isShuffled = false;
       

    void Start()
    {
        for (int i = 0; i < listColorsStart.Length; i++)
        {
            Color currentColorStart = listColorsStart[i];
            Color currentColorEnd = listColorsEnd[i];

            List<GameObject> blocksLine = new List<GameObject>();

            for (int j = 0; j < gridColumns; j++)
            {                
                float t = (float) j / gridColumns;
                Color currentColor = Color.Lerp(currentColorStart, currentColorEnd, t);

                float x = startPoint.x + (j * sizeCell.x);
                float y = startPoint.y + (i * sizeCell.y);

                GameObject block = Instantiate(blockPrefab, new Vector3(x,y,0), Quaternion.identity);
                MG02BlockController blockController = block.GetComponent<MG02BlockController>();

                blockController.gameController = gameController;

                blockController.correctPosition.x = x;
                blockController.correctPosition.y = y;
                blockController.correctPosition.z = block.transform.position.z;

                blockController.baseColor = currentColor;                

                //if (j == 0 || j == gridColumns - 1)
                //if (j == 0 || j ==4 || j == 5 || j == 6 || j == gridColumns - 1)
                if (j == 0 || j == 5 || j == gridColumns - 1)
                {
                    blockController.isStatic = true;
                }
                else
                {
                    blockController.isStatic = DefineStaticBlock();                    
                }

                if (!blockController.isStatic)
                {
                    toShuffleBlocks.Add(block);
                }



                blocksLine.Add(block);
            }

            blockInRightPositions.Add(blocksLine);
        }

        isFinishedInstantiate = true;
    }

    private void Update()
    {
        NeedToShuffle();
    }

    /// <summary>
    /// Método para definir aleatorimente com base no atributo rateStaticBlocks
    /// se um bloco iniciará ou não como estático na posição correta.
    /// </summary>
    /// <returns>Um bool onde verdadeiro indica que o bloco deve permanecer na posição inicial</returns>
    bool DefineStaticBlock()
    {
        int number = Random.Range(0, 100);

        if (number < (rateStaticBlocks * 100))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Método para embaralhar os blocos. 
    /// A primeira etapa é esperar um tempo para que a pessoa jogadora veja os blocos na posição correta.
    /// Depois disso a lista de blocos que serão embaralhados é dividida ao meio,
    /// para cada bloco na primeira lista (kings), é escolhido um bloco na segunda lista (queens),
    /// daí os blocos trocam de lugar, e a queen é removida da segunda lista.
    /// Ao fim, o método avisa ao GameController que o embaralhamento acabou.
    /// </summary>
    void NeedToShuffle()
    {
        if (!isShuffled && isFinishedInstantiate)
        {
            countToShuffle += Time.deltaTime;

            if (countToShuffle < delayToShuffle)
            {
                return;
            }

            int centerList = (toShuffleBlocks.Count / 2);
            List<GameObject> listKings = toShuffleBlocks.GetRange(0, centerList);
            List<GameObject> listQueens = toShuffleBlocks.GetRange(centerList, centerList);

            foreach (GameObject king in listKings)
            {
                int queenNumber = Random.Range(0, listQueens.Count - 1);
                GameObject chosenQueen = listQueens[queenNumber];

                king.GetComponent<MG02BlockController>().MoveTo(chosenQueen.transform.position);
                chosenQueen.GetComponent<MG02BlockController>().MoveTo(king.transform.position);

                listQueens.RemoveAt(queenNumber);
            }
            isShuffled = true;
            gameController.canClick = true;
            gameController.blockInRightPositions = blockInRightPositions;
        }

    }
}

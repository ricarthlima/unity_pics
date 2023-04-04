using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG02GameController : MonoBehaviour
{
    public GameObject blockSelected;
    public GameObject gridGeneratorGameObject;
    GridGenerator gridGenerator;

    [Header("Shuffle")]
    bool isShuffled = false;
    float countToShuffle = 0;

    private void Start()
    {
        gridGenerator = gridGeneratorGameObject.GetComponent<GridGenerator>();
    }

    private void Update()
    {
        VerifyClick();
        NeedToShuffle();
    }

    void NeedToShuffle()
    {
        if (!isShuffled && gridGenerator.isFinishedInstantiate)
        {
            countToShuffle += Time.deltaTime;
            if (countToShuffle < 1)
            {
                return;
            }

            int centerList = (gridGenerator.toShuffleBlocks.Count / 2) - 1;
            List<GameObject> listKings = gridGenerator.toShuffleBlocks.GetRange(0, centerList);
            List<GameObject> listQueens = gridGenerator.toShuffleBlocks.GetRange(centerList, centerList);

            foreach (GameObject king in listKings)
            {
                int queenNumber = Random.Range(0, listQueens.Count-1);
                GameObject chosenQueen = listQueens[queenNumber];

                king.GetComponent<BlockController>().MoveTo(chosenQueen.transform.position);
                chosenQueen.GetComponent<BlockController>().MoveTo(king.transform.position);

                listQueens.RemoveAt(queenNumber);
            }
            isShuffled = true;
        }
    }

    void VerifyClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null )
                {
                    if (hit.collider.CompareTag("Block"))
                    {
                        OnBlockTouched(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    public void OnBlockTouched(GameObject block)
    {
        BlockController blockClickedController = block.GetComponent<BlockController>();

        if (!blockClickedController.isStatic)
        {
            if (blockSelected == null)
            {
                blockSelected = block;
                blockClickedController.ShowIndicatorCircle();
            }
            else
            {
                blockSelected.GetComponent<BlockController>().MoveTo(block.transform.position);
                blockClickedController.MoveTo(blockSelected.transform.position);
                blockSelected = null;
            }
        }
        
    }


}

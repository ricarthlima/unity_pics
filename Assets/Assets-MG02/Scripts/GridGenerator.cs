using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject blockPrefab;

    public int gridColumns;

    public Color[] listColorsStart;
    public Color[] listColorsEnd;

    public Vector2 startPoint;
    public Vector2 sizeCell;

    public float rateStaticBlocks;

    void Start()
    {
        for (int i = 0; i < listColorsStart.Length; i++)
        {
            Color currentColorStart = listColorsStart[i];
            Color currentColorEnd = listColorsEnd[i];

            for (int j = 0; j < gridColumns; j++)
            {                
                float t = (float) j / gridColumns;
                Color currentColor = Color.Lerp(currentColorStart, currentColorEnd, t);

                float x = startPoint.x + (j * sizeCell.x);
                float y = startPoint.y + (i * sizeCell.y);

                GameObject block = Instantiate(blockPrefab, new Vector3(x,y,0), Quaternion.identity);
                BlockController blockController = block.GetComponent<BlockController>();

                blockController.x = j;
                blockController.y = i;

                blockController.baseColor = currentColor;
                blockController.isStatic = DefineStaticBlock();
                
            }
        }
    }

    void Update()
    {
        
    }

    bool DefineStaticBlock()
    {
        int number = Random.Range(0, 100);

        if (number <= (rateStaticBlocks * 100))
        {
            return true;
        }
        return false;
    }
}

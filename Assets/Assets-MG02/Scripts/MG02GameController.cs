using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG02GameController : MonoBehaviour
{
    public GameObject blockSelected;

    private void Update()
    {
        VerifyClick();
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
        if (blockSelected == null)
        {
            blockSelected = block;
        }
        else
        {
            Vector2 finalPos = block.transform.position;
            block.transform.position = blockSelected.transform.position;
            blockSelected.transform.position = finalPos;
            blockSelected = null;
        }
    }


}

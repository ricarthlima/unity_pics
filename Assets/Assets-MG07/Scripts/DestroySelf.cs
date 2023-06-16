using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float timeToWaitInSeconds;
    public bool justHide;

    private void OnEnable()
    {
        StartCoroutine(HideOrDestroy());
    }

    IEnumerator HideOrDestroy()
    {
        yield return new WaitForSeconds(timeToWaitInSeconds);
        if (!justHide)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

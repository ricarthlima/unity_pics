using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float timeToWaitInSeconds;
    float countTime = 0;

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime >= timeToWaitInSeconds)
        {
            Destroy(gameObject);
        }
    }
}

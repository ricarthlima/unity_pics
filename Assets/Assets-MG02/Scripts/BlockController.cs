using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Color baseColor;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = baseColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG03GameController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera m_Camera;

    [Header("Layers")]
    [SerializeField] private SpriteRenderer layerCadeirante;
    [SerializeField] private SpriteRenderer layerNanismo;
    [SerializeField] private SpriteRenderer layerPessoaCega;
    [SerializeField] private SpriteRenderer layerPessoaMuda;
    [SerializeField] private SpriteRenderer layerPortasLargas;

    [Header("Game")]
    public bool canInteract = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Camera.gameObject.transform.position.y <= -2.11f)
        {
            m_Camera.gameObject.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * 0.5f;
        }
        else
        {
            if (!canInteract)
            {
                canInteract = true;
            }
        }
        
    }
}

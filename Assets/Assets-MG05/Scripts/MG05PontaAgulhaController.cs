using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG05PontaAgulhaController : MonoBehaviour
{
    public MG05PartesCorpo? parteTocada;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ParteOrelha"))
        {            
            parteTocada = collision.GetComponent<MG05OrelhaPartesController>().parteCorpo;
            Debug.Log(parteTocada);
        }

        if (collision.CompareTag("Orelha"))
        {
            spriteRenderer.color = new Color(255,0,0,0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ParteOrelha"))
        {
            parteTocada = null;
            Debug.Log("SAIU");
        }

        if (collision.CompareTag("Orelha"))
        {
            spriteRenderer.color = new Color(255, 0, 0, 0);
        }
    }

}

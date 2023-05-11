using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG05PontaAgulhaController : MonoBehaviour
{
    public MG05PartesCorpo? parteTocada;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ParteOrelha"))
        {            
            parteTocada = collision.GetComponent<MG05OrelhaPartesController>().parteCorpo;
            Debug.Log(parteTocada);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ParteOrelha"))
        {
            parteTocada = null;
            Debug.Log("SAIU");
        }
    }
}

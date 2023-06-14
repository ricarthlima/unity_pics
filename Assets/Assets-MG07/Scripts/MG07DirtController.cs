using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG07DirtController : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    public bool isActive = false;


    public void Activate()
    {
        isActive = true;
        pointer.SetActive(true);
    }

    public void Clicked(GameObject animationCleanPrefab)
    {
        if (isActive)
        {
            Instantiate(animationCleanPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z) , Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

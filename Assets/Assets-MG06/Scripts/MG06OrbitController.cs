using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG06OrbitController : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 25;
    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();

        float random = Random.Range(0f, 1f);

        if (random <= 0.5f)
        {
            rb.AddForce(Vector2.left * speed);
        }
        else
        {
            rb.AddForce(Vector2.right * speed);
        }
    }

}

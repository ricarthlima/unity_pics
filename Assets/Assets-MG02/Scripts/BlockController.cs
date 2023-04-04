using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Position Puzzle")]
    public int x;
    public int y;
    public bool isStatic = false;

    public Color baseColor;
    float speed = 3;

    Vector3 destination;
    bool isMoving;

    void Start()
    {
        Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b, 0.666f);
        GetComponent<SpriteRenderer>().color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                isMoving = false;
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        isMoving = true;
        destination = position;
    }
}

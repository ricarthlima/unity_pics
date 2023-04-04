using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Indicators")]
    [SerializeField] private GameObject indicatorBlock;
    [SerializeField] private GameObject indicatorCircle;
    [SerializeField] private GameObject indicatorCorrect;

    [Header("Position Puzzle")]
    public Vector2 correctPosition;
    public bool isStatic = false;

    public Color baseColor;
    float speed = 2;

    Vector3 destination;
    bool isMoving;

    void Start()
    {
        Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b, 0.666f);
        GetComponent<SpriteRenderer>().color = newColor;

        if (isStatic)
        {
            indicatorBlock.SetActive(true);
        }
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
                indicatorCircle.SetActive(false);
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        isMoving = true;
        destination = position;
    }

    public void ShowIndicatorCircle()
    {
        indicatorCircle.SetActive(true);
    }
}

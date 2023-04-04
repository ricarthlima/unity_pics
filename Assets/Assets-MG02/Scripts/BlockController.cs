using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Indicators")]
    [SerializeField] private GameObject indicatorBlock;
    [SerializeField] private GameObject indicatorCircle;
    [SerializeField] private GameObject indicatorCorrect;

    [Header("Position Puzzle")]
    public Vector3 correctPosition;
    public bool isStatic = false;

    public Color baseColor;
    float speed = 4;

    Vector3 destination;
    bool isMoving;

    void Start()
    {
        Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b, 0.666f);
        GetComponent<SpriteRenderer>().color = newColor;

        if (isStatic)
        {
            indicatorCorrect.SetActive(true);
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

                if (transform.position == correctPosition)
                {
                    indicatorCorrect.SetActive(true);
                    isStatic = true;
                }

                //TODO: Substituir por emit
                GameObject.Find("MG02GameController").GetComponent<MG02GameController>().FreeClick();
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

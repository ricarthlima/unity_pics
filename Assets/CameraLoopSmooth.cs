using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoopSmooth : MonoBehaviour
{

    [SerializeField] private Vector3[] listPositions;
    [SerializeField] private float speed;
    int seeking = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 positionSeeking = listPositions[seeking];
        Debug.Log(positionSeeking);

        if (positionSeeking == transform.position)
        {
            seeking += 1;

            if (seeking == listPositions.Length)
            {
                seeking = 0;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, positionSeeking, speed * Time.deltaTime);
        }

    }
}

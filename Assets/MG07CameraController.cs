using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG07CameraController : MonoBehaviour
{
    Camera m_camera;

    void Start()
    {
        m_camera = GetComponent<Camera>();
        m_camera.orthographicSize = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_camera.orthographicSize < 5)
        {
            m_camera.orthographicSize += Time.deltaTime;
        }
    }
}

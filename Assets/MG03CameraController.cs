using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG03CameraController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] MG03GameController gameController;
    [SerializeField] MG03InGameAreasController inGameAreasController;
    Camera m_Camera;

    [Header("Triggers")]
    bool isStartAnimation = false;
    bool isCardsAnimation = false;
    bool isLayerInAnimation = false;
    bool isLayerOutAnimation = false;


    [Header("Values")]
    Vector3 positionArea;

    readonly Vector3 initialPosition = new Vector3(-1.51f, -3f, -10);
    readonly float initialSize = 4.42f;

    readonly float cardHeight = -2.11f;

    readonly float areaHeight = -1.5f;
    readonly float areaSize = 4.1f;

    readonly float layerInSize = 2.75f;
    

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
        transform.position = initialPosition;
    }

    void Update()
    {
        DoStartAnimation();
        DoAreaAnimation();
        DoShowLayerInAnimation();
        DoShowLayerOutAnimation();
    }

    #region "Start Animation"
    public void StartAnimation()
    {
        isStartAnimation = true;
    }

    private void DoStartAnimation()
    {
        if (isStartAnimation)
        {
            m_Camera.gameObject.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * 0.5f;

            if (m_Camera.gameObject.transform.position.y > cardHeight)
            {
                isStartAnimation = false;
                gameController.StartGame();
            }            
        }
    }

    #endregion

    #region "Area Animation"
    public void AreaAnimation()
    {
        isCardsAnimation = true;
    }

    private void DoAreaAnimation()
    {
        if (isCardsAnimation)
        {
            m_Camera.orthographicSize -= 1.7f * Time.deltaTime;
            m_Camera.transform.position += 1.7f * Vector3.up * Time.deltaTime;
            if (m_Camera.orthographicSize <= areaSize && m_Camera.transform.position.y > areaHeight)
            {
                isCardsAnimation = false;
            }
        }
    }

    #endregion

    #region "Layer Animation"
    public void ShowLayerInAnimation(Vector3 position)
    {
        positionArea = new Vector3(position.x, position.y, initialPosition.z);
        isLayerInAnimation = true;
    }

    private void DoShowLayerInAnimation()
    {
        if (isLayerInAnimation)
        {
            if (m_Camera.orthographicSize > layerInSize)
            {
                m_Camera.orthographicSize -= Time.deltaTime * 2;
            }

            if (m_Camera.transform.position != positionArea)
            {
                m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, positionArea, Time.deltaTime * 2);
            }
            

            if (m_Camera.orthographicSize <= layerInSize && m_Camera.transform.position == positionArea)
            {                
                isLayerInAnimation = false;
                positionArea = new Vector3(initialPosition.x, cardHeight, initialPosition.z);
                inGameAreasController.LerpLayer();
            }
        }
    }

    public void ShowLayerOutAnimation()
    {
        isLayerOutAnimation = true;
    }

    private void DoShowLayerOutAnimation()
    {
        if (isLayerOutAnimation)
        {
            if (m_Camera.orthographicSize < initialSize)
            {
                m_Camera.orthographicSize += Time.deltaTime * 2;
            }

            if (m_Camera.transform.position != positionArea)
            {
                m_Camera.transform.position = Vector3.MoveTowards(m_Camera.transform.position, positionArea, Time.deltaTime * 2);
            }
            
            if (m_Camera.orthographicSize >= initialSize && m_Camera.transform.position == positionArea)
            {
                isLayerOutAnimation = false;
                gameController.ToNextRound();
            }
        }
    }
    #endregion
}

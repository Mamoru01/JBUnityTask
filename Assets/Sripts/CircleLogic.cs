using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleLogic : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 0F;
    //private Rigidbody2D rb2D;
    private Transform transform;
    private Vector2 currentTargetPoint = new Vector2(0, 0);
    private Queue<Vector2> keyPoints = new Queue<Vector2>();

    private Vector2 m_SpeedVector;

    // Start is called before the first frame update
    void Awake()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown (KeyCode.Mouse0)) {
            this.setNewPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            this.setNewPosition(Camera.main.ScreenToWorldPoint(touch.position));
        }
#endif

    }

    void FixedUpdate()
    {
        if (Math.Abs(transform.position.x - currentTargetPoint.x) < 0.4
            & Math.Abs(transform.position.y - currentTargetPoint.y) < 0.4)
        {
            if (keyPoints.Count > 0) 
            {
                currentTargetPoint =  keyPoints.Dequeue();
            }
        }
        else
        {
            Debug.Log("GoTo: " + currentTargetPoint);
            //rb2D.MovePosition(rb2D.position + currentTargetPoint * Time.fixedDeltaTime * m_Speed);
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPoint, m_Speed*0.1F);
        }
    }

    public void changeSpeed(float value)
    {
        m_Speed = value;
        Debug.Log("ChangeSpeed. New Speed = " + m_Speed);
    }

    private void setNewPosition(Vector2 newPosition)
    {
        keyPoints.Enqueue(newPosition);

        Debug.Log("Added new position to path: " + newPosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;
    private Quaternion initialRotation;
    private Vector2 initialTouchPosition;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initialTouchPosition = touch.position;
                initialRotation = transform.rotation; 
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.position - initialTouchPosition;
                float rotationFactor = touchDelta.x * 0.1f;

              
                transform.rotation = initialRotation * Quaternion.Euler(0, -rotationFactor, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
               
                initialRotation = transform.rotation;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch1.position, touch2.position);
                initialScale = transform.localScale;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                if (Mathf.Approximately(initialDistance, 0))
                    return;

                float scaleFactor = currentDistance / initialDistance;
                transform.localScale = initialScale * scaleFactor;
            }
        }
    }
}

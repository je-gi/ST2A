using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;

    private float rotationSpeed = 0.2f;
    private float initialRotationFingerPosX;

    private void Update()
    {
        if (Input.touchCount == 2)
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
        else if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initialRotationFingerPosX = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = touch.position.x - initialRotationFingerPosX;

                Vector3 currentRotation = transform.eulerAngles;

                float newRotationY = currentRotation.y - deltaX * rotationSpeed;

                transform.eulerAngles = new Vector3(currentRotation.x, newRotationY, currentRotation.z);

                initialRotationFingerPosX = touch.position.x;
            }
        }
    }
}

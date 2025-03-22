using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private float _scrollSpeed;

    private Vector3 touch;

    private Vector3 startPosition;
    private Camera cam;

    public bool IsDisabled = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(IsDisabled) 
            return;

        if (Input.GetMouseButtonDown(0))
        {
            startPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0LastPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1LastPos = touch1.position - touch1.deltaPosition;

            float distTouch = (touch1LastPos - touch0LastPos).magnitude;
            float currentDistTouch = (touch1.position - touch0.position).magnitude;

            float difference  = currentDistTouch - distTouch;
            Zoom(difference * 0.01f * _scrollSpeed);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = (startPosition - cam.ScreenToViewportPoint(Input.mousePosition)) * _cameraSpeed;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x - pos.x, -25.0f, 40.0f), transform.position.y, Mathf.Clamp(transform.position.z - pos.y, 0f, 45.0f));
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed);
    }

    private void Zoom(float increment)
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - increment, 5, 25), transform.position.z); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float m_panSpeed = 0.5f;
    private float m_zoomSpeed = 0.8f;

    void Update()
    {
        PanWithMiddleButton();
        ZoomWithScroll();
    }

    private void PanWithMiddleButton()
    {
        if (Input.GetMouseButton(2))
            transform.Translate(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * -m_panSpeed);
    }

    private void ZoomWithScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
            transform.Translate(new Vector3(0f, 0f, Input.mouseScrollDelta.y * m_zoomSpeed));
    }
}

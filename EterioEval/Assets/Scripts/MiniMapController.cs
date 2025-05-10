using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapController : MonoBehaviour, IPointerClickHandler
{
    public Camera m_miniMapCamera;
    private RectTransform m_miniMapRect;
    // Start is called before the first frame update
    void Start()
    {
       m_miniMapRect = this.GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData i_pointerEventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_miniMapRect, i_pointerEventData.position, null, out Vector2 l_mousePosOnRect);
        Vector2 l_normalizedPos = new Vector2(l_mousePosOnRect.x / m_miniMapRect.rect.width, l_mousePosOnRect.y / m_miniMapRect.rect.height);
        Vector3 l_worldPos = m_miniMapCamera.ViewportToWorldPoint(new Vector3(l_normalizedPos.x, l_normalizedPos.y, m_miniMapCamera.nearClipPlane));
        SetMainCamera(l_worldPos);
    }

    private void SetMainCamera(Vector3 i_pos)
    {
        ModelController l_modelController = FindAnyObjectByType<ModelController>();
        if (l_modelController != null)
        {
            Bounds l_bounds = new Bounds(l_modelController.m_boundingBox.position, l_modelController.m_boundingBox.localScale);
            Vector3 l_closestPointInBounds = l_bounds.ClosestPoint(i_pos);
            Camera.main.transform.position = new Vector3(l_closestPointInBounds.x, Camera.main.transform.position.y, l_closestPointInBounds.z);
        }
    }
}

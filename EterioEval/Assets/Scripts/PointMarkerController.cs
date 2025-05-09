using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMarkerController : MonoBehaviour
{

    private void OnMouseDrag()
    {
        EnableModelRotation(false);
        RaycastHit l_raycastHit;
        Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(l_ray, out l_raycastHit, 20f, LayerMask.GetMask("Model")))
        {
            transform.position = l_raycastHit.point;
        }
        AngleCalculator._angleCalculator.CalculateAngle();
    }

    private void OnMouseUp()
    {
        EnableModelRotation(true);
    }

    private void EnableModelRotation(bool i_value)
    {
        ModelController l_modelController = FindAnyObjectByType<ModelController>();
        if (l_modelController != null)
            l_modelController.EnableRotation(i_value);
    }
}

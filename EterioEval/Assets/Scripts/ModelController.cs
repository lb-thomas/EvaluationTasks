using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    //TODO: Set these dynamically
    public Transform m_pivot;
    public Transform m_axisMarker;
    //TODO: Make these private
    public Transform m_model;
    public bool m_rotationEnabled = true;
    public bool m_changingPivot = false;

    // Start is called before the first frame update
    void Start()
    {
        SetModel(m_model); //TODO: Set at initiallization 
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rotationEnabled)
            RotateWithMouse();
        if (m_changingPivot)
            ClickToSetPivot();
    }

#region PUBLIC_METHODS
    public void ResetRotation()
    {
        m_pivot.rotation = Quaternion.identity;
    }

    public void ResetPivot()
    {
        ResetRotation();
        SetPivotPosition(Vector3.zero);
    }
    public void EnableRotation(bool i_value)
    {
        m_rotationEnabled = i_value;
    }

    public void SetModel(Transform i_model)
    {
        m_model = i_model;
        m_model.SetParent(m_pivot);
        var l_meshRenderers = m_model.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer i_meshRenderer in l_meshRenderers)
        {
            if (i_meshRenderer.GetComponent<MeshCollider>() == null)
            {
                i_meshRenderer.gameObject.AddComponent<MeshCollider>();
                i_meshRenderer.gameObject.layer = LayerMask.NameToLayer("Model");
            }
        }
    }

    public void ChangePivot(bool i_value)
    {
        m_rotationEnabled = !i_value;
        m_changingPivot = i_value;
    }
#endregion PUBLIC_METHODS

#region PRIVATE_METHODS
    private void SetPivotPosition(Vector3 i_newPivotPos)
    {
        m_model.SetParent(null);
        m_pivot.position = i_newPivotPos;
        m_axisMarker.position = i_newPivotPos;
        m_model.SetParent(m_pivot);
        ChangePivot(false);
    }

    private bool ClickToSetPivot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit l_raycastHit;
            Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(l_ray, out l_raycastHit, 20f, LayerMask.GetMask("Model")))
            {
                SetPivotPosition(l_raycastHit.point);
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private void RotateWithMouse()
    {
        if (Input.GetMouseButton(0))
            m_pivot.Rotate(Input.GetAxis("Mouse Y") * 2f, Input.GetAxis("Mouse X") * 2f, 0);

    }
#endregion PRIVATE_METHODS
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class AngleCalculator : MonoBehaviour
{
    public static AngleCalculator _angleCalculator;

    public GameObject m_pointMarkerPrefab;
    public GameObject m_linePrefab;
    public GameObject m_textPrefab;

    public bool m_selectingPoints = false; //TODO: Make private
    private List<GameObject> m_selectedPoints = new List<GameObject>();
    private LineRenderer m_lineRenderer;
    private Transform m_modelParent;
    private TMP_Text m_text;
    // Start is called before the first frame update
    void Start()
    {
        if (_angleCalculator == null)
            _angleCalculator = this;

        //SelectPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_selectingPoints)
            SelectPoints();

        if (m_modelParent != null)
        {
            if (m_modelParent.hasChanged)
            {
                DrawLine();
                m_modelParent.hasChanged = false;
            }
        }
    }

    public void StartSelectPoints()
    {
        m_selectingPoints = true;
        SelectPoints();
    }

    public void Clear()
    {
        
    }

    public void CalculateAngle()
    {
        if (m_selectedPoints.Count != 3)
            return;
        print("Calculate Angle");
        float l_angle = Vector3.Angle(m_selectedPoints[0].transform.position - m_selectedPoints[1].transform.position, m_selectedPoints[2].transform.position - m_selectedPoints[1].transform.position);
        UpdateAngleText(l_angle);
        print($"Angle: {l_angle.ToString("#.##")}");
        DrawLine();
    }

    private void SelectPoints()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit l_raycastHit;
            Ray l_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(l_ray, out l_raycastHit, 20f, LayerMask.GetMask("Model")))
            {
                if (m_modelParent == null)
                {
                    m_modelParent = FindAnyObjectByType<ModelController>().transform.GetChild(0);
                }    
                GameObject l_pointMarker = Instantiate(m_pointMarkerPrefab, l_raycastHit.point, Quaternion.identity, m_modelParent);
                m_selectedPoints.Add(l_pointMarker);
            }               
        }
        if (m_selectedPoints.Count == 3)
        {
            m_selectingPoints = false;
            CalculateAngle();
        }
    }

    private void DrawLine()
    {
        if (m_selectedPoints.Count != 3)
            return;
        if (m_lineRenderer == null)
        {
            m_lineRenderer = Instantiate(m_linePrefab, m_modelParent).GetComponent<LineRenderer>();
        }
        m_lineRenderer.SetPositions(new Vector3[]{ m_selectedPoints[0].transform.position, m_selectedPoints[1].transform.position, m_selectedPoints[2].transform.position });
    }

    private void UpdateAngleText(float i_angle)
    {
        if (m_text == null)
            m_text = Instantiate(m_textPrefab, m_modelParent).GetComponent<TMP_Text>();
        m_text.text = i_angle.ToString("#.##");
        m_text.transform.position = m_selectedPoints[1].transform.position;
    }

}

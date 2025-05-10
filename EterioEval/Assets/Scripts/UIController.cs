using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button m_measureAngleButton;
    public Button m_clearPointsButton;
    public Button m_exitButton;
    public Button m_changePivotButton;

    private UIStates m_currentState;
    // Start is called before the first frame update
    void Start()
    {
        SetButtons();
        ChangeState(UIStates.S01_Default);
    }

    public void ChangeState(UIStates i_nextState)
    {
        m_currentState = i_nextState;
        switch(i_nextState)
        {
            case UIStates.S01_Default:
                m_measureAngleButton.gameObject.SetActive(true);
                m_clearPointsButton.gameObject.SetActive(false);
                m_changePivotButton.gameObject.SetActive(true);
                m_exitButton.gameObject.SetActive(false);
                break;
            case UIStates.S02_MeasureAngle:
                m_measureAngleButton.gameObject.SetActive(false);
                m_clearPointsButton.gameObject.SetActive(true);
                m_changePivotButton.gameObject.SetActive(false);
                m_exitButton.gameObject.SetActive(true);
                break;
            case UIStates.S03_ChangePivot:
                m_measureAngleButton.gameObject.SetActive(false);
                m_clearPointsButton.gameObject.SetActive(false);
                m_changePivotButton.gameObject.SetActive(true);
                m_exitButton.gameObject.SetActive(true);
                break;
        }
        
    }

    private void OnMeasureAngleClick()
    {
        ChangeState(UIStates.S02_MeasureAngle);
        AngleCalculator._angleCalculator.StartSelectPoints(true);
    }
    private void OnClearPointsClick()
    {
        AngleCalculator._angleCalculator.Clear();
        AngleCalculator._angleCalculator.StartSelectPoints(true);
    }
    private void OnChangePivotClick()
    {
        ChangeState(UIStates.S03_ChangePivot);
        ModelController l_modelController = FindAnyObjectByType<ModelController>();
        if (l_modelController != null)
            l_modelController.ChangePivot(true);
    }
    private void OnExitClick()
    {
        ChangeState(UIStates.S01_Default);

        AngleCalculator._angleCalculator.StartSelectPoints(false);
        AngleCalculator._angleCalculator.Clear();

        ModelController l_modelController = FindAnyObjectByType<ModelController>();
        if (l_modelController != null)
            l_modelController.ChangePivot(false);
        
    }

    private void SetButtons()
    {
        m_measureAngleButton.onClick.AddListener(OnMeasureAngleClick);
        m_clearPointsButton.onClick.AddListener(OnClearPointsClick);
        m_changePivotButton.onClick.AddListener(OnChangePivotClick);
        m_exitButton.onClick.AddListener(OnExitClick);
    }
}

public enum UIStates
{
    S01_Default,
    S02_MeasureAngle,
    S03_ChangePivot
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangePanelScript : MonoBehaviour
{

    [SerializeField] private GameObject OperationPanel;
    [SerializeField] public GameObject ExplainPanel;
    [SerializeField] private GameObject TaskPanel;
    [SerializeField] private GameObject KillPanel;
    [SerializeField] private GameObject LastPanel;


    public void OperationOnClick()
    {
        OperationPanel.SetActive(false);
        ExplainPanel.SetActive(true);
    }

    public void ExplainBackOnClick()
    {
        ExplainPanel.SetActive(false);
        OperationPanel.SetActive(true);
    }

    public void ExplainNextOnClick()
    {
        ExplainPanel.SetActive(false);
        TaskPanel.SetActive(true);
    }

    public void TaskBackOnClick()
    {
        TaskPanel.SetActive(false);
        ExplainPanel.SetActive(true);
    }

    public void TaskNextOnClick()
    {
        TaskPanel.SetActive(false);
        KillPanel.SetActive(true);
    }

    public void KillBackOnClick()
    {
        KillPanel.SetActive(false);
        TaskPanel.SetActive(true);
    }

    public void KillNextOnClick()
    {
        KillPanel.SetActive(false);
        LastPanel.SetActive(true);
    }

    public void LastBackOnClick()
    {
        LastPanel.SetActive(false);
        KillPanel.SetActive(true);
    }

    public void LastCloseOnClick()
    {
        SceneManager.LoadScene("selectScene");
    }

}

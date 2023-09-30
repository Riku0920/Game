using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SettingScript : MonoBehaviour
{
    [SerializeField] private GameObject panelObject;
    [SerializeField] private GameObject DamageUI;
    [SerializeField] private GameObject StatusUI;
    private OnlineScripts OS;
    private GameObject TimerUI;
    public bool WeponUI;
    public bool authenticity;

    // Start is called before the first frame update
    private void Start()
    {
        authenticity = true;
        WeponUI = true;
        TimerUI = GameObject.FindGameObjectWithTag("Timer");
        OS = GameObject.FindGameObjectWithTag("Online").GetComponent<OnlineScripts>();
    }
    // Update is called once per frame
    void Update()
    {
        if (OS.endgame)
        {
            WeponUI = false;
            panelObject.SetActive(false);
            DamageUI.SetActive(false);
            StatusUI.SetActive(false);
            TimerUI.SetActive(false);
            authenticity = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&!OS.endgame)
        {
            if (authenticity)
            {
                WeponUI = false;
                panelObject.SetActive(true);
                DamageUI.SetActive(false);
                StatusUI.SetActive(false);
                TimerUI.SetActive(false);
                authenticity = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                WeponUI = true;
                panelObject.SetActive(false);
                DamageUI.SetActive(true);
                StatusUI.SetActive(true);
                TimerUI.SetActive(true);
                authenticity = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    public void ReturnGame()
    {
        WeponUI = true;
        panelObject.SetActive(false);
        DamageUI.SetActive(true);
        StatusUI.SetActive(true);
        TimerUI.SetActive(true);
        authenticity = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ReturnHome()
    {
        PhotonNetwork.Disconnect();
        GameObject.FindGameObjectWithTag("Online").GetComponent<OnlineScripts>().ReturnLose();
        SceneManager.LoadScene("selectScene");
    }
}

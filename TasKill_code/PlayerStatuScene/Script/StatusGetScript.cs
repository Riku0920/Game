using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatusGetScript : MonoBehaviour
{
    [SerializeField] private Text SumKill;
    [SerializeField] private Text SumDeath;
    [SerializeField] private Text SumWin;
    [SerializeField] private Text SumLose;
    [SerializeField] private Text KillDeath;
    [SerializeField] private Text WinLose;
    [SerializeField] private Text SumTask;
    [SerializeField] private Text SumTaskPoint;
    [SerializeField] private Text MyName;

    private string Name;
    private float Kill;
    private float Death;
    private float win;
    private float lose;
    private int Task;
    private int TaskPoint;

    private DataBase gameManager;

    private void GetStatus()
    {
        GameObject DataBase = GameObject.FindGameObjectWithTag("DataBase");
        gameManager = DataBase.GetComponent<DataBase>(); 
        //‚±‚±‚ÅŽæ“¾
        /**/
        Name = gameManager.NameSetting;
        Kill = gameManager.KillSetting;
        Death = gameManager.DeathSetting;
        win = gameManager.WinSetting;
        lose = gameManager.LoseSetting;
        Task = gameManager.TaskSetting;
        TaskPoint = gameManager.TaskPointSetting;
        /**/
        if(Name == null)
        {
            Name = "No Name";
        }
        MyName.text = Name;
        SumKill.text = Kill.ToString();
        SumDeath.text = Death.ToString();
        SumWin.text = win.ToString();
        SumLose.text = lose.ToString();
        if(Kill == 0 && Death == 0)
        {
            KillDeath.text = "0.00";
        }
        else if (Death == 0)
        {
            KillDeath.text = Kill.ToString() + ".00";
        }
        else
        {
            KillDeath.text = (Kill / Death).ToString("N2");
        }
        if (win == 0 && lose == 0)
        {
            WinLose.text = "0.00%";
        }
        else if (lose == 0)
        {
            WinLose.text = "100.0%";
        }
        else
        {
            float SumPlay = win + lose;
            WinLose.text = ((win / SumPlay) * 100).ToString("N2") + "%";
        }
        SumTask.text = Task.ToString();
        SumTaskPoint.text = TaskPoint.ToString();
    }

    private void Start()
    {
        GetStatus();
    }

}

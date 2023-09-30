using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCrear : MonoBehaviour
{
    [SerializeField]
    private GameObject Time;
    private CountDown CrearTime;
    [SerializeField]
    private GameObject gameCrear;
    bool isCalledOnce = false;

    float Alltime;
    float totaltime;
    int minute;
    float seconds;
    private Text timerText;
    private Text RankText;

    private void Start()
    {
        CrearTime = Time.GetComponent<CountDown>();
        timerText = gameCrear.transform.GetChild(2).transform.GetChild(0).GetComponentInChildren<Text>();
        RankText = gameCrear.transform.GetChild(3).transform.GetChild(0).GetComponentInChildren<Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isCalledOnce)
        {
            Alltime = CrearTime.AllTime;
            
            this.totaltime = CrearTime.totalTime;
            this.minute = CrearTime.minute;
            this.seconds = CrearTime.seconds;
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            if (totaltime > Alltime*2/3)
            {
                RankText.text = "Sƒ‰ƒ“ƒN";
            }
            if (totaltime < Alltime * 2 / 3)
            {
                if (totaltime > Alltime * 1 / 3)
                {
                    RankText.text = "Aƒ‰ƒ“ƒN";
                }
            }
            if (totaltime < Alltime * 1 / 3)
            {
                RankText.text = "Bƒ‰ƒ“ƒN";
            }
            Instantiate(gameCrear);
            isCalledOnce = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

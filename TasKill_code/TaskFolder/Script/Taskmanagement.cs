using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Taskmanagement : MonoBehaviourPunCallbacks
{
    public GameObject _Task;
    int coin = 0;
    int _mato = 0;

    public GameObject _TaskCoin;

    public GameObject _TaskMato;
    private OnlineScripts OS;
    // Update is called once per frame
    public void StartGame()
    {
        if (photonView.IsMine)
        {
            OS = GameObject.FindGameObjectWithTag("Online").GetComponent<OnlineScripts>();
            int task = Random.Range(1, 100);
            if (task > 50)//コインのタスク出現
            {
                _Task.GetComponent<Text>().text = "コインを5枚集めろ";
                Instantiate(_TaskCoin);
            }
            if (task < 50)//的当てのタスク出現
            {
                _Task.GetComponent<Text>().text = "的を当てろ";
                Instantiate(_TaskMato);
            }
        }
    }
    public void GetCoin()
    {
        coin = coin + 1;
        if(photonView.IsMine)
        {
            this.gameObject.GetComponent<PlayerStatus>().GetScore(5);
            _Task.GetComponent<Text>().text = "コインを5枚集めろ" + coin.ToString("F0")+"/5";
            if (coin == 5)
            {
                this.gameObject.GetComponent<PlayerStatus>().GetScore(40);
                OS.TaskClear(25);
                OS.TaskNum();
                Destroy(GameObject.FindWithTag("Coin"));
                _Task.GetComponent<Text>().text = "的を当てろ";
                Instantiate(_TaskMato);
                coin = 0;
            }
        }
    }

    public void Mato()
    {
        _mato = _mato + 1;
        _Task.GetComponent<Text>().text = "的を3つ当てろ" + _mato.ToString("F0") + "/3";
        this.gameObject.GetComponent<PlayerStatus>().GetScore(5);
        OS.TaskClear(5);
        if (_mato == 3)
        {
            this.gameObject.GetComponent<PlayerStatus>().GetScore(50);
            OS.TaskClear(50);
            OS.TaskNum();
            Destroy(GameObject.FindWithTag("target"));
            _Task.GetComponent<Text>().text = "コインを5枚集めろ";
            Instantiate(_TaskCoin);
            _mato = 0;
        }
    }
}

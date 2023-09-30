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
            if (task > 50)//�R�C���̃^�X�N�o��
            {
                _Task.GetComponent<Text>().text = "�R�C����5���W�߂�";
                Instantiate(_TaskCoin);
            }
            if (task < 50)//�I���Ẵ^�X�N�o��
            {
                _Task.GetComponent<Text>().text = "�I�𓖂Ă�";
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
            _Task.GetComponent<Text>().text = "�R�C����5���W�߂�" + coin.ToString("F0")+"/5";
            if (coin == 5)
            {
                this.gameObject.GetComponent<PlayerStatus>().GetScore(40);
                OS.TaskClear(25);
                OS.TaskNum();
                Destroy(GameObject.FindWithTag("Coin"));
                _Task.GetComponent<Text>().text = "�I�𓖂Ă�";
                Instantiate(_TaskMato);
                coin = 0;
            }
        }
    }

    public void Mato()
    {
        _mato = _mato + 1;
        _Task.GetComponent<Text>().text = "�I��3���Ă�" + _mato.ToString("F0") + "/3";
        this.gameObject.GetComponent<PlayerStatus>().GetScore(5);
        OS.TaskClear(5);
        if (_mato == 3)
        {
            this.gameObject.GetComponent<PlayerStatus>().GetScore(50);
            OS.TaskClear(50);
            OS.TaskNum();
            Destroy(GameObject.FindWithTag("target"));
            _Task.GetComponent<Text>().text = "�R�C����5���W�߂�";
            Instantiate(_TaskCoin);
            _mato = 0;
        }
    }
}

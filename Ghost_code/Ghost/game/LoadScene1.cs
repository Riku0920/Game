using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene1 : MonoBehaviour
{
    AudioClip clip;
    AudioSource audio;
    bool maveFlag;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        clip = gameObject.GetComponent<AudioSource>().clip;

        maveFlag = false;   //�ړ��{�^���͉�����Ă��Ȃ�
    }
    void Update()
    {
        //�ړ��{�^����������Ă���@and
        //���̍Đ����I����Ă�����ړ�
        
        if (maveFlag == true && audio.isPlaying == false)
        {
            Application.LoadLevel("game");
            SceneManager.LoadScene("title");
        }
    }

    public void ButtonClicked()
    {
        GameObject obj = GameObject.Find("Player");
        Destroy(obj);
        audio.PlayOneShot(clip);
        maveFlag = true;    //�ړ��{�^���������ꂽ
    }
}
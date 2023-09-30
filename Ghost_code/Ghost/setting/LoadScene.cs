using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    AudioClip clip;
    AudioSource audio;
    bool maveFlag;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        clip = gameObject.GetComponent<AudioSource>().clip;

        maveFlag = false;   //移動ボタンは押されていない
    }
    void Update()
    {
        //移動ボタンがおされている　and
        //音の再生が終わっていたら移動
        if (maveFlag == true && audio.isPlaying == false)
        {
            SceneManager.LoadScene("title");
        }
    }

    public void ButtonClicked()
    {
        /*GameObject obj = GameObject.Find("Player");
        Destroy(obj);*/
        audio.PlayOneShot(clip);
        maveFlag = true;    //移動ボタンが押された
    }
}
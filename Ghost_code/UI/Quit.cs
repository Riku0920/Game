using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//�V�[�������[�h����ꍇ�ɕK�v

public class Quit : MonoBehaviour
{
    public void ButtonClicked()
    {
        SceneManager.LoadScene("title");
        //SceneManager.LoadScene(0);
        //GameObject obj = GameObject.Find("Player");
        //Destroy(obj);

    }
}

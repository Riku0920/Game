using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    //�g�p��
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;//�J�[�\����\��
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftAlt))//Alt�������Ă���ԁA�J�[�\���\��
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

          
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))//Alt�����ꂽ��A�J�[�\����\��
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

          
        }
        
    }
}

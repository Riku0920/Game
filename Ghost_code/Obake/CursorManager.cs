using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    //使用例
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;//カーソル非表示
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftAlt))//Altを押している間、カーソル表示
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

          
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))//Altが離れたら、カーソル非表示
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

          
        }
        
    }
}

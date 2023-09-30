using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CameraControll : MonoBehaviour
{
    private FPSController FPSc;
    private SphereCollider sphc;
    private ThirdPersonCharacter tpc;

    private GameObject mainCamera;      //メインカメラ格納用
    private Camera cam;
    private GameObject subCamera;       //サブカメラ格納用 
    private GameObject FPSCamera;       //FPSカメラ格納用 
    bool m_switching = true;
 //   private GameObject XYZCamera;

    //呼び出し時に実行される関数
    void Start()
    {
        FPSc = GetComponent<FPSController>();
        sphc = GetComponent<SphereCollider>();
        tpc = GetComponent<ThirdPersonCharacter>();
        FPSc.enabled = false;
        sphc.enabled = false;
        //メインカメラとサブカメラをそれぞれ取得
        mainCamera = GameObject.Find("MainCamera");
        cam = mainCamera.GetComponent<Camera>();
        subCamera = GameObject.Find("SubCamera");
        FPSCamera = GameObject.Find("FPSCamera");

        //サブカメラを非アクティブにする
        subCamera.SetActive(false);
        FPSCamera.SetActive(false);
 //       XYZCamera.SetActive(false);
    }


    //単位時間ごとに実行される関数
    void Update()
    {
        //任意のボタンを押したら、カメラをアクティブにする
        if (Input.GetKeyDown("r"))
        {
            if (m_switching == true)//もう一度ボタンを押したらメインに戻す
            {
                m_switching = false;
                main();
            }
            else
            {
                sub();
            }
        }
        if (Input.GetKeyDown("f"))//もう一度ボタンを押したらメインに戻す
        {
            if(m_switching == true)
            {
                m_switching = false;
                main();
            }
            else
            {
                FPS();
            }
        }

        if (Input.GetKey("v"))
        {
            main();
        }

    }
    void main()
    {
        FPSc.enabled = false;
        sphc.enabled = false;
        //サブカメラをアクティブに設定
        tpc.OffFPS();
        cam.enabled = true;
        subCamera.SetActive(false);
        FPSCamera.SetActive(false);
    }
    void sub()
    {
        //サブカメラをアクティブに設定
        cam.enabled = false;
        subCamera.SetActive(true);
        FPSCamera.SetActive(false);
        m_switching = true;
        //        XYZCamera.SetActive(false);
    }
    void FPS()
    {
        FPSc.enabled = true;
        sphc.enabled = true;
        tpc.OnFPS();
        //FPSカメラをアクティブに設定
        subCamera.SetActive(false);
        FPSCamera.SetActive(true);
        cam.enabled = false;
        m_switching = true;
    }
}
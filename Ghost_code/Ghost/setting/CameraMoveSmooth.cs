using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class CameraMoveSmooth : MonoBehaviour
{
    public Transform StartPosition;
    public Transform GoalPosition;

    //スピードの設定
    public float speed = 1.0f;

    //2点間の距離を入れる
    private float distance_two;

    void Start()
    {
        //二点間の距離を代入
        distance_two = Vector3.Distance(StartPosition.position, GoalPosition.position);
    }

    void Update()
    {
        //メインカメラを取得
        Camera camere = Camera.main;
        //カメラを前に移動し続ける
        GetComponent<Camera>().gameObject.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f));
        //オブジェクトの名前で検索して取得
        GameObject camera_object = GameObject.Find("Home");

        if(camere.gameObject.transform.position.z == 0)
        {
            camere.gameObject.transform.Translate(new Vector3(0.0f, 0.0f, 0.0f));
        }

        //現在の位置
        float present_Location = (Time.time * speed) / distance_two;
        //オブジェクトの移動
        transform.position = Vector3.Lerp(StartPosition.position, GoalPosition.position, present_Location);
    }
}
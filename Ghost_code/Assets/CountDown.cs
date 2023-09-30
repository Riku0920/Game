using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

	//　トータル制限時間
	public float totalTime;
	//　制限時間（分）
	public int minute;
	//　制限時間（秒）
	public float seconds;
	//　前回Update時の秒数
	private float oldSeconds;
	private Text timerText;

	[SerializeField]
	private GameObject gameOver;
	bool isCalledOnce = false;

	public float AllTime;


	void Start()
	{
		totalTime = minute * 60 + seconds;
		AllTime = totalTime;
		oldSeconds = 0f;
		timerText = GetComponentInChildren<Text>();
	}
	
	
	void FixedUpdate()
	{
		//　制限時間が0秒以下なら何もしない
		if (totalTime <= 0f)
		{
			return;
		}
		//　一旦トータルの制限時間を計測；
		totalTime = minute * 60 + seconds;
		totalTime -= Time.deltaTime;

		//　再設定
		minute = (int)totalTime / 60;
		seconds = totalTime - minute * 60;

		//　タイマー表示用UIテキストに時間を表示する
		if ((int)seconds != (int)oldSeconds)
		{
			timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
		}
		oldSeconds = seconds;

		//　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
		if (totalTime <= 0f)
		{
			Debug.Log("制限時間終了");
			Instantiate(gameOver);//gameover
		}
		
	}
	

	//設定ボタン押したらtimer停止
	public void SettingOnClick()
	{
		Time.timeScale = 0.0f;
		
	}

	//restart押したらtimer再開
	public void restartOnClick()
	{
		Time.timeScale = 1.0f;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	//Quit押したらtimerリセット
	public void QuitOnClick()
	{
		Time.timeScale = 1.0f;

	}

}
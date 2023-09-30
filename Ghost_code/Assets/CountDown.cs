using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

	//�@�g�[�^����������
	public float totalTime;
	//�@�������ԁi���j
	public int minute;
	//�@�������ԁi�b�j
	public float seconds;
	//�@�O��Update���̕b��
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
		//�@�������Ԃ�0�b�ȉ��Ȃ牽�����Ȃ�
		if (totalTime <= 0f)
		{
			return;
		}
		//�@��U�g�[�^���̐������Ԃ��v���G
		totalTime = minute * 60 + seconds;
		totalTime -= Time.deltaTime;

		//�@�Đݒ�
		minute = (int)totalTime / 60;
		seconds = totalTime - minute * 60;

		//�@�^�C�}�[�\���pUI�e�L�X�g�Ɏ��Ԃ�\������
		if ((int)seconds != (int)oldSeconds)
		{
			timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
		}
		oldSeconds = seconds;

		//�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�������ԏI���x�Ƃ����������\������
		if (totalTime <= 0f)
		{
			Debug.Log("�������ԏI��");
			Instantiate(gameOver);//gameover
		}
		
	}
	

	//�ݒ�{�^����������timer��~
	public void SettingOnClick()
	{
		Time.timeScale = 0.0f;
		
	}

	//restart��������timer�ĊJ
	public void restartOnClick()
	{
		Time.timeScale = 1.0f;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	//Quit��������timer���Z�b�g
	public void QuitOnClick()
	{
		Time.timeScale = 1.0f;

	}

}
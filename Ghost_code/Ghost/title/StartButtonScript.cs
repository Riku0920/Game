using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
	AudioClip clip;
	AudioSource audio;
	bool maveFlag;

	//�@�񓯊�����Ŏg�p����AsyncOperation
	private AsyncOperation async;
	//�@�V�[�����[�h���ɕ\������UI���
	[SerializeField]
	private GameObject loadUI;
	//�@�ǂݍ��ݗ���\������X���C�_�[
	[SerializeField]
	private Slider slider;

	public void NextScene()
	{
		//�@���[�h���UI���A�N�e�B�u�ɂ���
		loadUI.SetActive(true);

		StartCoroutine("LoadData");

		audio.PlayOneShot(clip);
		maveFlag = true;    //�ړ��{�^���������ꂽ
	}

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
			StartCoroutine("LoadData");
		}
	}

	IEnumerator LoadData()
	{

		async = SceneManager.LoadSceneAsync("game");
		//�@�ǂݍ��݂��I���܂Ői���󋵂��X���C�_�[�̒l�ɔ��f������
		while (!async.isDone)
		{
			var progressVal = Mathf.Clamp01(async.progress / 0.9f);
			slider.value = progressVal;
			yield return null;
		}
	}
}
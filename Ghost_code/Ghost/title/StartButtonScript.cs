using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
	AudioClip clip;
	AudioSource audio;
	bool maveFlag;

	//　非同期動作で使用するAsyncOperation
	private AsyncOperation async;
	//　シーンロード中に表示するUI画面
	[SerializeField]
	private GameObject loadUI;
	//　読み込み率を表示するスライダー
	[SerializeField]
	private Slider slider;

	public void NextScene()
	{
		//　ロード画面UIをアクティブにする
		loadUI.SetActive(true);

		StartCoroutine("LoadData");

		audio.PlayOneShot(clip);
		maveFlag = true;    //移動ボタンが押された
	}

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
			StartCoroutine("LoadData");
		}
	}

	IEnumerator LoadData()
	{

		async = SceneManager.LoadSceneAsync("game");
		//　読み込みが終わるまで進捗状況をスライダーの値に反映させる
		while (!async.isDone)
		{
			var progressVal = Mathf.Clamp01(async.progress / 0.9f);
			slider.value = progressVal;
			yield return null;
		}
	}
}
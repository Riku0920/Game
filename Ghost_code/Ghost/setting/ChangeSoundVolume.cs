using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//"AudioSource"コンポーネントがアタッチされていない場合アタッチ
[RequireComponent(typeof(AudioSource))]
public class ChangeSoundVolume : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject obj;
    [SerializeField] Slider s;

    private void Start()
    {
        //"AudioSource"コンポーネントを取得
        obj = GameObject.Find("GameObject.BGM");
        audioSource = obj.GetComponent<AudioSource>();
        s.value = audioSource.volume;
        s.onValueChanged.AddListener(VolChange);
    }

    public void VolChange(float vol)
    {
        //音楽の音量をスライドバーの値に変更
        audioSource.volume = vol;
    }
}
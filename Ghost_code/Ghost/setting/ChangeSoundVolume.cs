using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//"AudioSource"�R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�A�^�b�`
[RequireComponent(typeof(AudioSource))]
public class ChangeSoundVolume : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject obj;
    [SerializeField] Slider s;

    private void Start()
    {
        //"AudioSource"�R���|�[�l���g���擾
        obj = GameObject.Find("GameObject.BGM");
        audioSource = obj.GetComponent<AudioSource>();
        s.value = audioSource.volume;
        s.onValueChanged.AddListener(VolChange);
    }

    public void VolChange(float vol)
    {
        //���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        audioSource.volume = vol;
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//"AudioSource"�R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�A�^�b�`
[RequireComponent(typeof(AudioSource))]
public class ChangeSoundVolume1 : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject obj;
    [SerializeField] Slider h;

    private void Start()
    {
        //"AudioSource"�R���|�[�l���g���擾
        obj = GameObject.Find("GameObject.S");
        audioSource = obj.GetComponent<AudioSource>();
        h.value = audioSource.volume;
        h.onValueChanged.AddListener(VolChange);
    }

    public void VolChange(float vol)
    {
        //���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        audioSource.volume = vol;
    }
}*/
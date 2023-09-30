using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundScript : MonoBehaviour
{
    [SerializeField] private Text sound;

    //Slider�̒l���擾
    public void SliderSoundOnValueChange(float newSliderValue)
    {
        //���݂�Slider�̒l�����ʂƂ���
        AudioListener.volume = newSliderValue;
        //Slider�̒l�𕶎��Ƃ��ĕ\��
        float nowSound = newSliderValue / 1 * 100.0f;
        sound.text = nowSound.ToString("N0");
    }
}

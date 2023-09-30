using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundScript : MonoBehaviour
{
    [SerializeField] private Text sound;

    //Sliderの値を取得
    public void SliderSoundOnValueChange(float newSliderValue)
    {
        //現在のSliderの値を音量とする
        AudioListener.volume = newSliderValue;
        //Sliderの値を文字として表示
        float nowSound = newSliderValue / 1 * 100.0f;
        sound.text = nowSound.ToString("N0");
    }
}

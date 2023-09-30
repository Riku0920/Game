using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mousesensitivity : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private MouseLookScript mouseLookScript;
    private OnlineScripts onlineScripts;
    private GunScript gunScript;
    private GameObject Player;
    private GameObject OnlinePlayer;
    public float SliderValue;
    [SerializeField] private Text sensitivity;

    // Start is called before the first frame update
    void Start()
    {

    }

    //Sliderの値を取得
    public void MouseSliderOnChangeSensitivity(float newSliderValue)
    {
        SliderValue = newSliderValue;

        //感度値表示
        float nowsensitivity = SliderValue / 4 * 100.0f;
        sensitivity.text = nowsensitivity.ToString("N0");

        if (SliderValue <= 0.1f)
        {
            SliderValue = 0.1f;
        }

        try
        {
            GameObject Gun = GameObject.FindGameObjectWithTag("Weapon");
            GunScript GS = Gun.gameObject.GetComponent<GunScript>();
            GS.mouseSensitvity_notAiming = SliderValue;
            GS.mouseSensitvity_aiming = SliderValue;
            GS.mouseSensitvity_running = SliderValue;
        }
        catch
        {

        }
    }
}

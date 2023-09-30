using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminum : MonoBehaviour
{
    [SerializeField]
    private float staminum = 100f;

    public GameObject StaminumSlider;
    public Slider Slider;
    // Start is called before the first frame update
    void Start()
    {
        Slider = StaminumSlider.transform.Find("StaminumSlider").GetComponent< Slider>();
        Slider.value = staminum;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Slider.value -= 0.001f;
            }
            else
            {
                Slider.value += 0.0012f;
            }
        }
        else
        {
            Slider.value += 0.0012f;
        }
            
    }

}

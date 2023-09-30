using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help : MonoBehaviour
{
    [SerializeField] private GameObject help1;
    bool button = false;
    // Start is called before the first frame update

    public void OPEN()
    {
        if(!button)
        {
            help1.SetActive(true);
            button = true;
        }
        else if(button)
        {
            help1.SetActive(false);
            button = false;
        }
        
    }
}

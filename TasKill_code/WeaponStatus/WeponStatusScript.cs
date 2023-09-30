using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeponStatusScript : MonoBehaviour
{
    [SerializeField] public GameObject gameObject;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("selectScene");
            //gameObject.SetActive(!gameObject.activeSelf);
        }
    }
    public void returnbutton()
    {
        SceneManager.LoadScene("selectScene");
    }
}

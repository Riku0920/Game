using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    static int cnt = 0;
    private AudioSource audioSource;
    //private AudioSource audioSourec1;

    private void Awake()
    {

        if (cnt == 0)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            DontDestroyOnLoad(gameObject);//ÉVÅ[ÉìÇêÿÇËë÷Ç¶ÇƒÇ‡âπÇñ¬ÇÁÇµë±ÇØÇÈ
        }
        cnt++;

        
    }

    void Update()
    {
       /*

            if (SceneManager.GetActiveScene().name == "game")
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
        */

        /*if(gameObject == game)
        {
            audioSource = this.GetComponent<AudioSuurce>();
            audioSource.Stop();
        }*/
    }

    //Use this for initialization
    void Start()
    {
 
        //Invoke("LoadScene", 3f);
    }
    void LoadScene()
    {
        //SceneManager.LoadScene("Scene2");
    }

 
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatusButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClickReturnButton);
    }

    private void OnClickReturnButton()
    {
        //Ç±Ç±Ç…ñﬂÇÈèàóù
        SceneManager.LoadScene("selectScene");
    }
}

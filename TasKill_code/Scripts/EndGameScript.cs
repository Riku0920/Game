using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Text WinText;
    [SerializeField]
    Text RankText;
    public void ReturnButton()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("selectScene");
    }
    public void SetWin(bool win,bool net)
    {
        if (!net)
        {
            WinText.text = "‘Îí‘Šè‚ª‘Şo\nŸ—˜";
            return;
        }
        if (win)
        {
            WinText.text = "Ÿ—˜";
        }
        else
        {
            WinText.text = "”s–k";
        }
    }
    public void SetScore(int score)
    {
        RankText.text = "Score\n"+score.ToString();
    }
}

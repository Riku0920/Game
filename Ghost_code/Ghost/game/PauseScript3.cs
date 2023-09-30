using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript3 : MonoBehaviour
{
    //アイテムメニューを開くボタン
    [SerializeField]
    private GameObject Setting;
    //ゲーム再開ボタン
    [SerializeField]
    private GameObject start;
    //アイテムメニューパネル
    [SerializeField]
    private GameObject itemPanel;

    public void StopGame()
    {
        Time.timeScale = 0f;
        Setting.SetActive(false);
        start.SetActive(true);
        itemPanel.SetActive(true);
    }

    public void ReStartGame()
    {
        itemPanel.SetActive(false);
        start.SetActive(false);
        Setting.SetActive(true);
        Time.timeScale = 1f;
    }
}

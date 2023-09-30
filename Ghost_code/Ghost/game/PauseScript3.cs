using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript3 : MonoBehaviour
{
    //�A�C�e�����j���[���J���{�^��
    [SerializeField]
    private GameObject Setting;
    //�Q�[���ĊJ�{�^��
    [SerializeField]
    private GameObject start;
    //�A�C�e�����j���[�p�l��
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

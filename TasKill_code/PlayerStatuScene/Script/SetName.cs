using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SetName : MonoBehaviour
{
    [SerializeField] private Text NameText;
    [SerializeField] private Text OldName;
    private DataBase gameManager;
    private void Start()
    {
        GameObject DataBase = GameObject.FindGameObjectWithTag("DataBase");
        gameManager = DataBase.GetComponent<DataBase>();
        OldName.text = gameManager.NameSetting;
        NameText.text = gameManager.NameSetting;
    }
    //–ß‚é‚Ì‚Æ‚«
    public void ReturnSelect()
    {
        gameManager.NameSetting = OldName.text;
        SceneManager.LoadScene("selectScene");
    }
    public void SettingName()
    {
        GameObject DataBase = GameObject.FindGameObjectWithTag("DataBase");
        gameManager = DataBase.GetComponent<DataBase>();
        gameManager.NameSetting = NameText.text;
        SceneManager.LoadScene("selectScene");
    }
    public void DBug()
    {
        SceneManager.LoadScene("selectScene");
    }
}

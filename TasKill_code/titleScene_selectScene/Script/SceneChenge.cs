using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChenge : MonoBehaviour
{
    [SerializeField] private Object Object;
    // Start is called before the first frame update
    public void onSceneChengeButton()
    {
        SceneManager.LoadScene(Object.name);
    }

    public void PlayerScene()
    {
        SceneManager.LoadScene("PlayerStatus");
    }
    public void WeaponScene()
    {
        SceneManager.LoadScene("WeaponStatus");
    }
    public void SettingScene()
    {
        SceneManager.LoadScene("NameSetScene");
    }
    public void TutorialScene()
    {
        SceneManager.LoadScene("tutorial");
    }
    public void GamaScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}

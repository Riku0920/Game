using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    private static bool Loaded { get; set; }

    void Awake()
    {
        if (Loaded) return;

        Loaded = true;
        SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
    }
}
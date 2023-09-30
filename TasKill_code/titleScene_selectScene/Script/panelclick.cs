using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class panelclick: MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject panel;

    public void OnPointerClick(PointerEventData eventData)
    {
        // クリックされた時に行いたい処理
        panel.SetActive(true);
        SceneManager.LoadScene("selectScene");
    }
}
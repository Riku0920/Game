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
        // �N���b�N���ꂽ���ɍs����������
        panel.SetActive(true);
        SceneManager.LoadScene("selectScene");
    }
}
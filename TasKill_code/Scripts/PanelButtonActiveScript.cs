using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButtonActiveScript : MonoBehaviour
{
    [SerializeField]
    private GameObject LastPanel;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (LastPanel.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}

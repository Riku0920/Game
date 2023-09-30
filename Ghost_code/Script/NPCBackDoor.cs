using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBackDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject box;
    private Collider col;
    void Start()
    {
        col = box.GetComponent<BoxCollider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "NPC")
        {
            if(col.enabled == false)
            {
                col.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("è¡Ç¶ÇΩ");
        if (col.enabled == true)
        {
            col.enabled = false;
        }
    }
}

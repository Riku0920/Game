using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcObakeBack : MonoBehaviour
{
    [SerializeField]
    private Collider box;
    void Start()
    {
        box = GetComponent<BoxCollider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "NPC")
        {
            if (other.tag == "Player")
            {
                if (box.enabled == false)
                {
                    box.enabled = true;
                }
            }
            else
            {
                if (box.enabled == true)
                {
                    box.enabled = false;
                }   
            }

        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (box.enabled == false)
        {
            box.enabled = true;
        }
    }
}

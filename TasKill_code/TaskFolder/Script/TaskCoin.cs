using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TaskCoin : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Taskmanagement>().GetCoin();
            Destroy(gameObject);
        }
    }
}

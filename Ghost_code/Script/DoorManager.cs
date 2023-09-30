using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject DoorIcon = null;
    public AudioClip s_Open;
    public AudioClip s_Close;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        audioSource.PlayOneShot(s_Open);
    }
    public void Close()
    {
        audioSource.PlayOneShot(s_Close);
    }
}

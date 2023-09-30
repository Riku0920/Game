using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GollDoorScript : MonoBehaviour
{
    private GameObject talkIcon = null;
    //�h�A�G���A�ɓ����Ă��邩�ǂ���
    private bool isNear;

    public bool EventsisKey;//null�ł���΃C�x���g�����݂���

    private bool OpenOne = true;
    //�@�h�A�̃A�j���[�^�[
    private Animator animator;

    private DoorManager m_Door;

    private TalkScript talk;

    private ConversationScopeScript CSS;

    private TalkManager s_tm;
    void Start()
    {
        isNear = false;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && isNear && OpenOne && EventsisKey)
        {
            animator.SetBool("isNear", true);
            if(OpenOne)
            {
                m_Door.Open();
                talkIcon.SetActive(false);
                OpenOne = false;
            }

        }
        if (!isNear)
        {
            animator.SetBool("isNear", false);
            OpenOne = true;
        }
    }

    public void EventKey()
    {
        EventsisKey = true;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            m_Door = col.GetComponent<DoorManager>();
            talkIcon = m_Door.DoorIcon;
            talkIcon.transform.Find("Panel").position = Camera.main.GetComponent<Camera>().WorldToScreenPoint(col.transform.position + Vector3.up * 4.5f);
            isNear = true;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            m_Door = col.GetComponent<DoorManager>();
            talkIcon = m_Door.DoorIcon;
            talkIcon.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            talkIcon.SetActive(false);
            isNear = false;
        }
    }
}

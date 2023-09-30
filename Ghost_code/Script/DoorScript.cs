using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private GameObject talkIcon = null;
    //ドアエリアに入っているかどうか
    private bool isNear;

    public bool EventsisKey = false;//trueであればイベントが存在する

    private bool OpenOne = true;
    //　ドアのアニメーター
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
        if (Input.GetKeyDown("space") && isNear && OpenOne && !EventsisKey)
        {
            animator.SetBool("isNear", true);
            if (OpenOne)
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
        EventsisKey = false;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "NPC")
        {
            m_Door = col.transform.parent.GetComponent<DoorManager>();
            talkIcon = m_Door.DoorIcon;
            talkIcon.transform.Find("Panel").position = Camera.main.GetComponent<Camera>().WorldToScreenPoint(col.transform.position + Vector3.up * 4.5f);
            isNear = true;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "NPC")
        {
            m_Door = col.transform.parent.GetComponent<DoorManager>();
            talkIcon = m_Door.DoorIcon;
            talkIcon.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "NPC")
        {
            talkIcon.SetActive(false);
            isNear = false;
        }
    }
}

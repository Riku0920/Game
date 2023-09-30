using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationScopeScript : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player"
            && col.GetComponent<TalkController>().GetState() != TalkController.State.Talk
            )
        {
            //�@�v���C���[���߂Â������b����Ƃ��Ď����̃Q�[���I�u�W�F�N�g��n��
            col.GetComponent<TalkManager>().SetConversationPartner(gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player"
            && col.GetComponent<TalkController>().GetState() != TalkController.State.Talk
            )
        {
            //�@�v���C���[���������������b���肩��O��
             col.GetComponent<TalkManager>().ResetConversationPartner(gameObject);
        }
    }

}

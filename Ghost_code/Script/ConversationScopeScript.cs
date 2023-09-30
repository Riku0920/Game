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
            //　プレイヤーが近づいたら会話相手として自分のゲームオブジェクトを渡す
            col.GetComponent<TalkManager>().SetConversationPartner(gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player"
            && col.GetComponent<TalkController>().GetState() != TalkController.State.Talk
            )
        {
            //　プレイヤーが遠ざかったら会話相手から外す
             col.GetComponent<TalkManager>().ResetConversationPartner(gameObject);
        }
    }

}

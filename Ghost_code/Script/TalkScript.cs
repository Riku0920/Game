using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScript : MonoBehaviour
{
    [SerializeField]
    private Conversation conversation = null;
    [SerializeField]
    private Item item = null;

    public Item.ItemType itemType;

    public int evet = 0;//イベントの始まるページ数、０であればイベントが発生しない。
    public Conversation GetConversation()
    {
        if(item)
        {
            item.getitem();
        }
        Debug.Log("会話開始");
        return conversation;
    }
}

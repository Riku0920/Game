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

    public int evet = 0;//�C�x���g�̎n�܂�y�[�W���A�O�ł���΃C�x���g���������Ȃ��B
    public Conversation GetConversation()
    {
        if(item)
        {
            item.getitem();
        }
        Debug.Log("��b�J�n");
        return conversation;
    }
}

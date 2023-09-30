using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    //�@��b�\�ȑ���
    private GameObject conversationPartner;
    //�@��b�\�A�C�R��
    [SerializeField]
    private GameObject talkIcon = null;

    private bool npc = false;
    private bool events = false;

    // TalkUI�Q�[���I�u�W�F�N�g
    [SerializeField]
    private GameObject talkUI = null;
    //�@���b�Z�[�WUI
    private Text messageText = null;
    //�@�\�����郁�b�Z�[�W
    private string allMessage = null;
    //�C�x���g�����邩�Ȃ���
    private int Event = 0;
    //�C�x���g��������
    private bool EventKey = false;
    //�@�g�p���镪��������
    [SerializeField]
    private string splitString = "<>";
    //�@�����������b�Z�[�W
    private string[] splitMessage;
    //�@�����������b�Z�[�W�̉��Ԗڂ�
    private int messageNum;
    //�@�e�L�X�g�X�s�[�h
    [SerializeField]
    private float textSpeed = 0.05f;
    //�@�o�ߎ���
    private float elapsedTime = 0f;
    //�@�����Ă��镶���ԍ�
    private int nowTextNum = 0;
    //�@�}�E�X�N���b�N�𑣂��A�C�R��
    [SerializeField]
    private Image clickIcon = null;
    //�@�N���b�N�A�C�R���̓_�ŕb��
    [SerializeField]
    private float clickFlashTime = 0.2f;
    //�@1�񕪂̃��b�Z�[�W��\���������ǂ���
    private bool isOneMessage = false;
    //�@���b�Z�[�W�����ׂĕ\���������ǂ���
    private bool isEndMessage = false;

    [SerializeField]
    private GameObject selectionUI = null;
    private bool selectionYes = false;

    private Item.ItemType itemType = 0;
    private Item.ItemType Type = 0;
    public OwnedItemsData.OwnedItem _ownedItem;
    [SerializeField] private ItemButton itemButton;
    [SerializeField] private ItemsDialog itemDi;

    public List<Transform> myList;
    bool kekkai = false;

    void Start()
    {
        clickIcon.enabled = false;
        messageText = talkUI.GetComponentInChildren<Text>();
    }
    public  OwnedItemsData.OwnedItem OwnedItem
    {

        get { return _ownedItem; }
        set
        {
            _ownedItem = value;
            var isEmpty = null == _ownedItem;
            if (!isEmpty)
            {
                //Debug.Log(isEmpty + "wwwwww" + _ownedItem.Type);
                Type = _ownedItem.Type;
            }
                
        }
    }
    void Update()
    {
        
        //_ownedItem = itemButton._ownedItem;
        events = OwnedItemsData.Instance.a;
        //�@���b�Z�[�W���I����Ă��邩�A���b�Z�[�W���Ȃ��ꍇ�͂���ȍ~�������Ȃ�
        if (isEndMessage || allMessage == null)
        {
            return;
        }

        //�@1��ɕ\�����郁�b�Z�[�W��\�����Ă��Ȃ�	
        if (!isOneMessage)
        {
            //�@�e�L�X�g�\�����Ԃ��o�߂����烁�b�Z�[�W��ǉ�
            if (elapsedTime >= textSpeed)
            {
                messageText.text += splitMessage[messageNum][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;
                //�@���b�Z�[�W��S���\���A�܂��͍s�����ő吔�\�����ꂽ
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //�@���b�Z�[�W�\�����Ƀ}�E�X�̍��{�^������������ꊇ�\��
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                //�@�����܂łɕ\�����Ă���e�L�X�g�Ɏc��̃��b�Z�[�W�𑫂�
                messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }
            //�@1��ɕ\�����郁�b�Z�[�W��\������
        }
        else
        {

            elapsedTime += Time.deltaTime;

            //�@�N���b�N�A�C�R����_�ł��鎞�Ԃ𒴂������A���]������
            if (elapsedTime >= clickFlashTime)
            {
                clickIcon.enabled = !clickIcon.enabled;
                elapsedTime = 0f;
            }

            //�@�}�E�X�N���b�N���ꂽ�玟�̕����\������
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                
                for (var i = 0; i < itemDi.buttonNumber; i++)
                {
                    OwnedItem = OwnedItemsData.Instance.OwnedItems.Length > i
                          ? OwnedItemsData.Instance.OwnedItems[i]
                           : null;
                }
                
                if (conversationPartner.tag == "ohuda")
                {
                    selectionYes = true;

                    if (Event == messageNum + 1)
                    {
                        foreach (Transform childObject in this.gameObject.transform)
                        {
                            myList.Add(childObject);
                            if (childObject.tag == "NPC")
                            {
                                selectionUI.SetActive(true);
                                Cursor.lockState = CursorLockMode.None;
                                Cursor.visible = true;

                                kekkai = true;
                            }
                            else
                            {
                                EndTalking();
                            }
                        }
                    }
                    else if(Event > messageNum + 1)
                    {
                        nowTextNum = 0;
                        messageNum++;
                        messageText.text = "";
                        clickIcon.enabled = false;
                        elapsedTime = 0f;
                        isOneMessage = false;
                    }

                }
                else if (Event > messageNum && !events)//�C�x���g������ꍇ���C�x���g���������𖞂����Ă��Ȃ��ꍇ
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;

                }
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
               

                if (events && Event >= 1 && Event != messageNum + 1)//�C�x���g���������𖞂����Ă���ꍇ
                {
                    if (Type == itemType)
                    {
                        nowTextNum = 0;
                        messageNum++;
                        messageText.text = "";
                        clickIcon.enabled = false;
                        elapsedTime = 0f;
                        isOneMessage = false;
                        selectionUI.SetActive(true);
                        selectionYes = true;
                    }
                    else if (Type != itemType)
                    {
                        EndTalking();
                    }
                }
                
                 
                if (Event == 0)//�C�x���g���Ȃ��ꍇ
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;
                }
                
                if (Event == messageNum + 1 && !events && conversationPartner.tag != "ohuda")//�C�x���g���������𖞂����Ă��Ȃ��ꍇ
                {
                    Debug.Log(messageNum + "������������" + selectionYes);
                    EndTalking();

                }
                if(!selectionYes)
                {
                    //�@���b�Z�[�W���S���\������Ă�����Q�[���I�u�W�F�N�g���̂̍폜
                    if (messageNum >= splitMessage.Length)
                    {
                        Debug.Log(messageNum + "����������" + selectionYes);
                        EndTalking();
                    }
                }
               
            }
        }
    }

    public void YesEventEnd()
    {
        
        if(kekkai == true)
        {
            selectionUI.SetActive(false);
            selectionYes = false;
            kekkai = false;
            var TkSt = conversationPartner.GetComponent<TalkScript>();
            var CSS = conversationPartner.GetComponent<ConversationScopeScript>();
            Destroy(TkSt);
            Destroy(CSS);
            Destroy(conversationPartner);
            talkIcon.SetActive(false);
            conversationPartner = null;
            ResetConversationPartner(conversationPartner);
            

        }
        else{
            selectionYes = false;
            selectionUI.SetActive(false);
            EndTalking();
            var TkSt = conversationPartner.GetComponent<TalkScript>();
            var CSS = conversationPartner.GetComponent<ConversationScopeScript>();
            Destroy(TkSt);
            Destroy(CSS);
            conversationPartner.GetComponent<DoorScript>().EventKey();
            ResetConversationPartner(conversationPartner);
        }
    }

    public void NoEvent()
    {
        selectionYes = false;
        selectionUI.SetActive(false);
        EndTalking();
        ResetConversationPartner(conversationPartner);
    }

    private void LateUpdate()
    {
        //�@��b���肪����ꍇ��TalkIcon�̈ʒu����b����̓���ɕ\��
        if (conversationPartner != null)
        {
            talkIcon.transform.Find("Panel").position = Camera.main.GetComponent<Camera>().WorldToScreenPoint(conversationPartner.transform.position + Vector3.up * 4.5f);
        }
    }

    //�@��b�����ݒ�
    public void SetConversationPartner(GameObject col)
    {
        talkIcon.SetActive(true);
        conversationPartner = col;
        if(col.CompareTag("NPC"))
        {
            npc = true;
        }
    }

    //�@��b��������Z�b�g
    public void ResetConversationPartner(GameObject parterObj)
    {
        //�@��b���肪���Ȃ��ꍇ�͉������Ȃ�
        if (conversationPartner == null)
        {
            return;
        }
        //�@��b����ƈ����Ŏ󂯎�������肪�����C���X�^���XID�����Ȃ��b������Ȃ���
        if (conversationPartner.GetInstanceID() == parterObj.GetInstanceID())
        {
            talkIcon.SetActive(false);
            conversationPartner = null;
        }
    }
    //�@��b�����Ԃ�
    public GameObject GetConversationPartner()
    {
        return conversationPartner;
    }

    //�@��b���J�n����
    public void StartTalking()
    {
        var TalkScript = conversationPartner.GetComponent<TalkScript>();
        if(npc == true)
        {
            var NPCManager = conversationPartner.GetComponent<NPCManager>();
            NPCManager.SetState(NPCManager.State.Talk, transform);
        }
        
        this.allMessage = TalkScript.GetConversation().GetConversationMessage();
        this.Event = TalkScript.evet;
        this.itemType = TalkScript.itemType;
        Debug.Log("�C�x���g" + Event);
        //�@����������ň��ɕ\�����郁�b�Z�[�W�𕪊�����
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        //�@����������
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        talkUI.SetActive(true);
        talkIcon.SetActive(false);
        isOneMessage = false;
        isEndMessage = false;
        //�@��b�J�n���̓��͈͂�U���Z�b�g
        Input.ResetInputAxes();
    }
    //�@��b���I������
    void EndTalking()
    {
        isEndMessage = true;
        talkUI.SetActive(false);
        //�@���j�e�B�����Ƒ��l�����̏�Ԃ�ύX����
        var TalkScript = conversationPartner.GetComponent<TalkScript>();
        if (npc == true)
        {
            var NPCManager = conversationPartner.GetComponent<NPCManager>();
            NPCManager.SetState(NPCManager.State.Wait);
            npc = false;
        }
           
        GetComponent<TalkController>().SetState(TalkController.State.Normal);
        Input.ResetInputAxes();
    }
}
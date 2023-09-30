using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    //　会話可能な相手
    private GameObject conversationPartner;
    //　会話可能アイコン
    [SerializeField]
    private GameObject talkIcon = null;

    private bool npc = false;
    private bool events = false;

    // TalkUIゲームオブジェクト
    [SerializeField]
    private GameObject talkUI = null;
    //　メッセージUI
    private Text messageText = null;
    //　表示するメッセージ
    private string allMessage = null;
    //イベントがあるかないか
    private int Event = 0;
    //イベント発生条件
    private bool EventKey = false;
    //　使用する分割文字列
    [SerializeField]
    private string splitString = "<>";
    //　分割したメッセージ
    private string[] splitMessage;
    //　分割したメッセージの何番目か
    private int messageNum;
    //　テキストスピード
    [SerializeField]
    private float textSpeed = 0.05f;
    //　経過時間
    private float elapsedTime = 0f;
    //　今見ている文字番号
    private int nowTextNum = 0;
    //　マウスクリックを促すアイコン
    [SerializeField]
    private Image clickIcon = null;
    //　クリックアイコンの点滅秒数
    [SerializeField]
    private float clickFlashTime = 0.2f;
    //　1回分のメッセージを表示したかどうか
    private bool isOneMessage = false;
    //　メッセージをすべて表示したかどうか
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
        //　メッセージが終わっているか、メッセージがない場合はこれ以降何もしない
        if (isEndMessage || allMessage == null)
        {
            return;
        }

        //　1回に表示するメッセージを表示していない	
        if (!isOneMessage)
        {
            //　テキスト表示時間を経過したらメッセージを追加
            if (elapsedTime >= textSpeed)
            {
                messageText.text += splitMessage[messageNum][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;
                //　メッセージを全部表示、または行数が最大数表示された
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //　メッセージ表示中にマウスの左ボタンを押したら一括表示
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                //　ここまでに表示しているテキストに残りのメッセージを足す
                messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }
            //　1回に表示するメッセージを表示した
        }
        else
        {

            elapsedTime += Time.deltaTime;

            //　クリックアイコンを点滅する時間を超えた時、反転させる
            if (elapsedTime >= clickFlashTime)
            {
                clickIcon.enabled = !clickIcon.enabled;
                elapsedTime = 0f;
            }

            //　マウスクリックされたら次の文字表示処理
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
                else if (Event > messageNum && !events)//イベントがある場合かつイベント発生条件を満たしていない場合
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
               

                if (events && Event >= 1 && Event != messageNum + 1)//イベント発生条件を満たしている場合
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
                
                 
                if (Event == 0)//イベントがない場合
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;
                }
                
                if (Event == messageNum + 1 && !events && conversationPartner.tag != "ohuda")//イベント発生条件を満たしていない場合
                {
                    Debug.Log(messageNum + "いいいいいい" + selectionYes);
                    EndTalking();

                }
                if(!selectionYes)
                {
                    //　メッセージが全部表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        Debug.Log(messageNum + "あああああ" + selectionYes);
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
        //　会話相手がいる場合はTalkIconの位置を会話相手の頭上に表示
        if (conversationPartner != null)
        {
            talkIcon.transform.Find("Panel").position = Camera.main.GetComponent<Camera>().WorldToScreenPoint(conversationPartner.transform.position + Vector3.up * 4.5f);
        }
    }

    //　会話相手を設定
    public void SetConversationPartner(GameObject col)
    {
        talkIcon.SetActive(true);
        conversationPartner = col;
        if(col.CompareTag("NPC"))
        {
            npc = true;
        }
    }

    //　会話相手をリセット
    public void ResetConversationPartner(GameObject parterObj)
    {
        //　会話相手がいない場合は何もしない
        if (conversationPartner == null)
        {
            return;
        }
        //　会話相手と引数で受け取った相手が同じインスタンスIDを持つなら会話相手をなくす
        if (conversationPartner.GetInstanceID() == parterObj.GetInstanceID())
        {
            talkIcon.SetActive(false);
            conversationPartner = null;
        }
    }
    //　会話相手を返す
    public GameObject GetConversationPartner()
    {
        return conversationPartner;
    }

    //　会話を開始する
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
        Debug.Log("イベント" + Event);
        //　分割文字列で一回に表示するメッセージを分割する
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        //　初期化処理
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        talkUI.SetActive(true);
        talkIcon.SetActive(false);
        isOneMessage = false;
        isEndMessage = false;
        //　会話開始時の入力は一旦リセット
        Input.ResetInputAxes();
    }
    //　会話を終了する
    void EndTalking()
    {
        isEndMessage = true;
        talkUI.SetActive(false);
        //　ユニティちゃんと村人両方の状態を変更する
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
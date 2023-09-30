using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    public OwnedItemsData.OwnedItem OwnedItem
    {

        get { return _ownedItem; }
        set
        {
            _ownedItem = value;

            // アイテムが割り当てられたかどうかでアイテム画像や所持個数の表示を切り替える
            var isEmpty = null == _ownedItem;
            image.gameObject.SetActive(!isEmpty);
            number.gameObject.SetActive(!isEmpty);            
            name.gameObject.SetActive(!isEmpty);
            _button.interactable = !isEmpty;
            //Debug.Log("yyyyyyy" + _ownedItem);
            if (!isEmpty)
            {
                if (_ownedItem.Type == Item.ItemType.key)
                {
                    //Debug.Log("yyyyyyy" + _ownedItem.Type);
                    name.text = "玄関のカギ";
                    s_events = true;
                    number.gameObject.SetActive(false);

                }
                if (_ownedItem.Type == Item.ItemType.key2)
                {
                    //Debug.Log("yyyyyyy" + _ownedItem.Type);
                    name.text = "寝室のカギ";
                    number.gameObject.SetActive(false);
                }
                image.sprite = itemSprites.First(x => x.itemType == _ownedItem.Type).sprite;
                number.text = "" + _ownedItem.Number;
            }
        }
    }

    [SerializeField] private ItemTypeSpriteMap[] itemSprites; // 各アイテム用の画像を指定するフィールド
    [SerializeField] private Image image;
    [SerializeField] private Text number;
    [SerializeField] private Text name;

    private Button _button;
    public bool s_events = false;
    public OwnedItemsData.OwnedItem _ownedItem;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // TODO ボタンを押した時の処理はここに書く
    }

    /// <summary>
    /// アイテムの種類とSpriteをインスペクタで紐付けられるようにするためのクラス
    /// </summary>
    [Serializable]
    public class ItemTypeSpriteMap
    {
        public Item.ItemType itemType;
        public Sprite sprite;
    }
}
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

            // �A�C�e�������蓖�Ă�ꂽ���ǂ����ŃA�C�e���摜�⏊�����̕\����؂�ւ���
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
                    name.text = "���ւ̃J�M";
                    s_events = true;
                    number.gameObject.SetActive(false);

                }
                if (_ownedItem.Type == Item.ItemType.key2)
                {
                    //Debug.Log("yyyyyyy" + _ownedItem.Type);
                    name.text = "�Q���̃J�M";
                    number.gameObject.SetActive(false);
                }
                image.sprite = itemSprites.First(x => x.itemType == _ownedItem.Type).sprite;
                number.text = "" + _ownedItem.Number;
            }
        }
    }

    [SerializeField] private ItemTypeSpriteMap[] itemSprites; // �e�A�C�e���p�̉摜���w�肷��t�B�[���h
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
        // TODO �{�^�������������̏����͂����ɏ���
    }

    /// <summary>
    /// �A�C�e���̎�ނ�Sprite���C���X�y�N�^�ŕR�t������悤�ɂ��邽�߂̃N���X
    /// </summary>
    [Serializable]
    public class ItemTypeSpriteMap
    {
        public Item.ItemType itemType;
        public Sprite sprite;
    }
}
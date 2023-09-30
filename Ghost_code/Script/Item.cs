//using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    /// <summary>
    /// アイテムの種類定義
    /// </summary>
    public enum ItemType
    {

        a,
        key, 
        key2,
        key3
    }
    [SerializeField] private ItemType type;

    public void getitem()
    {
        // プレイヤーの所持品として追加
        OwnedItemsData.Instance.Add(type);
        //OwnedItemsData.Instance.Save();
        // 所持アイテムのログ出力
        foreach (var item in OwnedItemsData.Instance.OwnedItems)
        {
            Debug.Log(item.Type + "を" + item.Number + "個所持");
        }
    }
}
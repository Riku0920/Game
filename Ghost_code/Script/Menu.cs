using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private ItemsDialog itemsDialog;

    [SerializeField] private Button itemsButton;

    private void Start()
    {
        itemsButton.onClick.AddListener(ToggleItemsDialog);
    }
    /// <summary>
    /// アイテムウインドウを開閉します。
    /// </summary>
    private void ToggleItemsDialog()
    {
        itemsDialog.Toggle();
    }

    /// <summary>
    /// レシピウインドウを開閉します。
    /// </summary>
    private void ToggleRecipeDialog()
    {
        // TODO 後で実装
    }
}

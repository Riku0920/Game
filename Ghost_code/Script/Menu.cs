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
    /// �A�C�e���E�C���h�E���J���܂��B
    /// </summary>
    private void ToggleItemsDialog()
    {
        itemsDialog.Toggle();
    }

    /// <summary>
    /// ���V�s�E�C���h�E���J���܂��B
    /// </summary>
    private void ToggleRecipeDialog()
    {
        // TODO ��Ŏ���
    }
}

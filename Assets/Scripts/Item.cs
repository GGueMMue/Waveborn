using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData ItemData;
    public int level;
    public WeaponManager weapon;

    Image icon;
    Text textLeve;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = ItemData.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLeve = texts[0];
    }

    private void LateUpdate()
    {
        textLeve.text = $"Lv.{level + 1}";
    }

    public void OnClick()
    {
        switch (ItemData.itemType)
        {
            case ItemData.ItemType.MELEE:
            case ItemData.ItemType.RANGE:

                break;
            case ItemData.ItemType.GLOVE:

                break;
            case ItemData.ItemType.SHOE:

                break;
            case ItemData.ItemType.HEAL:

                break;

            default:
                break;
        }

        level++;

        if(level == ItemData.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

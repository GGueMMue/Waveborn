using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData ItemData;
    public int level;
    public WeaponManager weapon;
    public Gear gear;

    Image icon;
    Text textLeve;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = ItemData.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLeve = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = ItemData.itemName;
    }

    private void OnEnable()
    {
        textLeve.text = $"Lv.{level + 1}";
        switch (ItemData.itemType)
        {
            case ItemData.ItemType.MELEE:
            case ItemData.ItemType.RANGE:
                textDesc.text = string.Format(ItemData.itemDesc, ItemData.damages[level] * 100, ItemData.cnts[level]);
                break;

            case ItemData.ItemType.GLOVE:
            case ItemData.ItemType.SHOE:
                textDesc.text = string.Format(ItemData.itemDesc, ItemData.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(ItemData.itemDesc);
                break;
        }
    }


    public void OnClick()
    {
        switch (ItemData.itemType)
        {
            case ItemData.ItemType.MELEE:
            case ItemData.ItemType.RANGE:
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<WeaponManager>();
                    weapon.Init(ItemData);
                }
                else
                {
                    float nextDamage = ItemData.baseDamage;
                    int nextCount = 0;

                    nextDamage += ItemData.baseDamage * ItemData.damages[level];
                    nextCount += ItemData.cnts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;

            case ItemData.ItemType.GLOVE:
            case ItemData.ItemType.SHOE:
                if (level == 0)
                {                    
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(ItemData);                    
                }
                else
                {
                    float nextRate = ItemData.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;

            case ItemData.ItemType.HEAL:
                GameManager.instance.hp = GameManager.instance.maxHP;
                break;

            default:
                break;
        }
        if(level == ItemData.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

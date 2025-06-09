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

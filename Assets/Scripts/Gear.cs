using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    
    public void Init(ItemData data)
    {
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.pc.transform;
        transform.localScale = Vector3.zero;


        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.GLOVE:
                RateUp();
                break;
            case ItemData.ItemType.SHOE:
                SpeedUp();
                break;

            default: break;
        }
    }

    void RateUp()
    {
        WeaponManager[] weapons = transform.parent.GetComponentsInChildren<WeaponManager>();
        foreach (WeaponManager weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0: 
                    weapon.speed = 150 + (150 * rate);                     
                    break;
                default:
                    weapon.speed = 0.5f * (1f * rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 4.5f;
        GameManager.instance.pc.characterSpeed = speed + speed *rate;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble OBJ/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { 
        MELEE= 0,
        RANGE,
        GLOVE,
        SHOE,
        HEAL
    }

    [Header("Basic Data")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("Level Data")]
    public float baseDamage;
    public int baseCnt;
    public float[] damages;
    public int[] cnts;

    [Header("Weapon Data")]
    public GameObject projectTile;

}

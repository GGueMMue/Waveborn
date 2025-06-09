using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum InfoType 
    { 
        EXP = 0,
        LEVEL,
        KILL,
        TIME,
        HP
    }

    public InfoType info;
    public Text text;
    public Slider slider;

    private void Awake()
    {
        text = GetComponent<Text>();
        slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (info)
        {
            case InfoType.EXP:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                slider.value = curExp / maxExp;
                break;
            case InfoType.LEVEL:

                break;
            case InfoType.KILL:

                break;
            case InfoType.TIME:

                break;
            case InfoType.HP:

                break;

            default:
                break;
        }
    }
}

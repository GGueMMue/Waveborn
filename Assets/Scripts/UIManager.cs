using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    float rgbTimer = 0;
    float rgbSpeed = 1f;
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
                text.text = $"Lv.{GameManager.instance.level.ToString()}";
                break;
            case InfoType.KILL:
                text.text = $"{GameManager.instance.kill.ToString()}";
                break;
            case InfoType.TIME:
                float remainTime = GameManager.instance.maxPlayTime - GameManager.instance.playTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);

                text.text = $"{min:D2} : {sec:D2}";

                rgbTimer = Mathf.Repeat(Time.time * rgbSpeed, 1f);
                Color color = Color.HSVToRGB(rgbTimer, 1f, 1f);
                text.color = color;

                break;
            case InfoType.HP:
                float curHP = GameManager.instance.hp;
                float maxHP = GameManager.instance.maxHP;
                slider.value = curHP / maxHP;

                break;

            default:
                break;
        }
    }
}

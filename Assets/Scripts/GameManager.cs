using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float playTime;
    public float maxPlayTime = 2 * 10f; // num * min
    public PlayerController pc;
    public static GameManager instance;
    public ObjPooling pools;
    public int hp;
    public int maxHP = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public LevelManager levelManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hp = maxHP;

        levelManager.Select(0);
    }

    private void Update()
    {
        playTime += Time.deltaTime;

        if(playTime > maxPlayTime )
        {
            playTime = maxPlayTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
            levelManager.Show();
        }
    }
}

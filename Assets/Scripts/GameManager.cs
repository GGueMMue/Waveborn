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

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        playTime += Time.deltaTime;

        if(playTime > maxPlayTime )
        {
            playTime = maxPlayTime;
        }
    }
}

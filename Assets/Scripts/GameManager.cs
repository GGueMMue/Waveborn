using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController pc;
    public static GameManager instance;
    public ObjPooling pools;

    private void Awake()
    {
        instance = this;
    }
}

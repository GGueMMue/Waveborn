using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connecter : MonoBehaviour
{
    public void ConnecterFuc()
    {
        GetComponentInParent<EnemyScript>().Dead();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spwanPos;

    float spawnTimer = 0;
    float spawnTime = 0.2f;

    private void Awake()
    {
        spwanPos = GetComponentsInChildren<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.instance.pools.GetComponent<ObjPooling>().GetGameObject(1);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnTime)
        {
            spawnTime = 0;
            GameObject enemy = GameManager.instance.pools.GetComponent<ObjPooling>().GetGameObject(Random.Range(0, 4);
            enemy.transform.position = spwanPos[Random.Range(1, spwanPos.Length)].position;
        }
    }
}

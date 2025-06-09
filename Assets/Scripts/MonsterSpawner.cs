using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SpawnData 
{
    public string monsterName;
    public float spawnTime;
    public int hp;
    public float speed;
}


public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spwanPos;
    public SpawnData[] spawnDataList;
    int nowLevel;
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

        nowLevel = Mathf.Min(Mathf.FloorToInt(GameManager.instance.playTime / 10f), spawnDataList.Length - 1);

        if(spawnTimer >= spawnDataList[nowLevel].spawnTime)
        {
            spawnTime = 0;
            GameObject enemy = GameManager.instance.pools.GetComponent<ObjPooling>().GetGameObject(nowLevel);
            enemy.transform.position = spwanPos[Random.Range(1, spwanPos.Length)].position;
            enemy.GetComponent<EnemyScript>().InitData(spawnDataList[nowLevel]);
        }
    }
}

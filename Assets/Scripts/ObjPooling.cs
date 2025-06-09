using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPooling : MonoBehaviour
{
    public GameObject[] enemyObjectsList;

    [SerializeField] List<GameObject>[] enemyObjectPools;

    private void Awake()
    {
        enemyObjectPools = new List<GameObject>[enemyObjectsList.Length];

        for(int i = 0; i < enemyObjectPools.Length; i++)
        {
            enemyObjectPools[i] = new List<GameObject>();
        }
    }

    public GameObject GetGameObject(int i)
    {
        GameObject sel = null;

        foreach(GameObject obj in enemyObjectPools[i])
        {
            if (!obj.activeSelf)
            {
                sel = obj;
                sel.SetActive(true);

                break;
            }
        }

        if(sel == null)
        {
            sel = Instantiate(enemyObjectsList[i], this.gameObject.transform.position, this.gameObject.transform.rotation);
            enemyObjectPools[i].Add(sel);
        }
        return sel;
    }
}

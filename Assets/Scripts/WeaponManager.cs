using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;

    float time;
    PlayerController playerController;

    private void Awake()
    {
        playerController = GameManager.instance.pc;
    }
    [SerializeField] float timer;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStop) return;

        timer += Time.deltaTime;

        switch (id)
        {
            case 0:
                Debug.Log("In");
                transform.Rotate(0f, 0f, speed * Time.deltaTime);
                break;

            case 1:
                if(timer > speed)
                {
                    timer = 0;
                    FIre();
                }
                break;
            default:
                break;


        }


        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            LevelUp(10, 1);
        }
    }

    public void FIre()
    {
        if (playerController.scanner.nearEnemy == null) return;

        Vector3 enemyPos = playerController.scanner.nearEnemy.position;
        Vector3 dir = (enemyPos - this.transform.position).normalized;
        
        Transform bullet = GameManager.instance.pools.GetComponent<ObjPooling>().GetGameObject(prefabID).transform;
        bullet.position = this.gameObject.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        bullet.GetComponent<WeaponScript>().Init(damage, count, dir); // count = per
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id ==0)
        {
            InstantiateWeapon();
        }
        playerController.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void InstantiateWeapon()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pools.GetComponent<ObjPooling>().
                    GetGameObject(prefabID).transform;
                bullet.transform.parent = this.transform;
            }
            //bullet.transform.position = Vector3.zero;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.identity;
            Vector3 rotationVector = Vector3.forward * 360 * i / count;

            bullet.Rotate(rotationVector);
            bullet.Translate(bullet.up * 2f, Space.World);
            //if(id == 0)
            //    bullet.transform.position = this.gameObject.transform.position + new Vector3(0f, 2f, 0f);
            bullet.GetComponent<WeaponScript>().Init(damage, -1, Vector3.zero); // -1 == inf
        }
    }

    public void Init(ItemData data)
    {
        name = "Weapon" + data.itemId;
        transform.parent = playerController.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCnt;

        for(int i =0; i < GameManager.instance.pools.enemyObjectsList.Length; i++)
        {
            if(data.projectTile == GameManager.instance.pools.enemyObjectsList[i])
            {
                prefabID = i;
                break;
            }
        }
        switch (id) 
        {
            case 0:
                speed = -150;
                InstantiateWeapon();
                break;

            case 1:
                speed = 0.3f;
                break;
            default:

                break;
        
        }
        playerController.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

    }
}

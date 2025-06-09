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
        playerController = GetComponentInParent<PlayerController>();
    }
    [SerializeField] float timer;

    private void Start()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
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
            LevelUp(20, 10);
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

    public void Init()
    {
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

    }
}

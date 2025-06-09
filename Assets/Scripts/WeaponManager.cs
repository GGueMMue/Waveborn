using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                Debug.Log("In");
                transform.Rotate(0f, 0f, speed * Time.deltaTime);
                break;

            default:

                break;

        }


        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            LevelUp(20, 10);
        }
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
            bullet.GetComponent<WeaponScript>().Init(damage, -1); // -1 == inf
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

            default:
                
                break;
        
        }

    }
}

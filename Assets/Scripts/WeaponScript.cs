using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        if(per > - 1)
        {
            rb.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || per != -1)
        {
            per--;

            if (per == -1)
            {
                rb.velocity = Vector2.zero;
                this.gameObject.SetActive(false);
            }
        }
    }
}

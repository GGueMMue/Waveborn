using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed;
    public float hp;
    public float maxHP;
    public Rigidbody2D playerRB;
    public float knockBack = 3;

    [SerializeField] Animator animator;
    [SerializeField] Quaternion lerpRot;
    [SerializeField] float lerpSpeed = 10f;
    [SerializeField] Collider2D col2;
    [SerializeField] bool _isDead;

    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (_isDead || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dir = playerRB.position - rb.position;
        Vector2 moveVector = dir.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(moveVector + rb.position);
        rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (_isDead) return;

        if (playerRB.position.x < rb.position.x)
        {
            lerpRot = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            lerpRot = Quaternion.Euler(0f, 0f, 0f);
        }
        rb.transform.rotation = Quaternion.Lerp(
            rb.transform.rotation,
            lerpRot,
            Time.fixedDeltaTime* lerpSpeed);
    }

    public void InitData(SpawnData data)
    {
        maxHP = data.hp;
        moveSpeed = data.speed;
        hp = data.hp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            hp -= collision.GetComponent<WeaponScript>().damage;

            StartCoroutine(KnockBack());

            if(hp > 0)
            {
                animator.SetTrigger("Hit");
            }
            else
            {
                _isDead = true;
                col2.enabled = false;
                rb.simulated = false;
                animator.SetTrigger("Death");
            }
        }
        else
        {

        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator KnockBack()
    {
        yield return new WaitForFixedUpdate();
        Vector3 playerPos = GameManager.instance.pc.transform.position;
        Vector3 dir = this.transform.position - playerPos;

        rb.AddForce(dir.normalized * knockBack, ForceMode2D.Impulse);
    }
    private void OnEnable()
    {
        col2 = GetComponent<Collider2D>();
        animator = rb.GetComponentInChildren<Animator>();
        playerRB = GameManager.instance.pc.GetComponent<Rigidbody2D>();
        col2.enabled = true;
        rb.simulated = true;
        _isDead = false;
        hp = maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

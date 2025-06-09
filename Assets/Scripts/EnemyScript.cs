using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed;
    public float hp;
    public float maxHP;
    public Rigidbody2D playerRB;

    [SerializeField] Quaternion lerpRot;
    [SerializeField] float lerpSpeed = 10f;

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
        if (_isDead) return;

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
    private void OnEnable()
    {
        playerRB = GameManager.instance.pc.GetComponent<Rigidbody2D>();
        _isDead = false;
        hp = maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

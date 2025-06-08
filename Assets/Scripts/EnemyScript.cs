using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D playerRB;

    [SerializeField] bool _isDead;

    [SerializeField] Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameManager.instance.pc.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 dir = playerRB.position - rb.position;
        Vector2 moveVector = dir.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(moveVector + rb.position);
        rb.velocity = Vector2.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

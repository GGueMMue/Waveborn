using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed;
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
        playerRB = GameManager.instance.pc.GetComponent<Rigidbody2D>();
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
    // Update is called once per frame
    void Update()
    {
        
    }
}

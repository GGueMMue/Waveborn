using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 keyboardInputVector2d;
    public Scanner scanner;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] SPUM_Prefabs playerAnim;
    [SerializeField] GameObject playerBody;
    [SerializeField] Quaternion lerpRot;
    [SerializeField] float lerpSpeed = 10f;

    public float characterSpeed;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<SPUM_Prefabs>();
        scanner = GetComponent<Scanner>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isStop) return;
        keyboardInputVector2d.x = Input.GetAxisRaw("Horizontal");
        keyboardInputVector2d.y = Input.GetAxisRaw("Vertical");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.instance.isStop) return;

        GameManager.instance.hp -= Time.deltaTime * 10;

        if(GameManager.instance.hp < 0)
        {
            playerAnim.PlayAnimation(2);
            StartCoroutine(GameManager.instance.Delay());
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isStop) return;

        if (keyboardInputVector2d != Vector2.zero)
        {

            if (keyboardInputVector2d.x > 0)
            {
                lerpRot = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (keyboardInputVector2d.x < 0)
            {
                lerpRot = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                lerpRot = playerBody.transform.rotation;
            }
            
            playerAnim.PlayAnimation(1);
        }
        else playerAnim.PlayAnimation(0);
        playerBody.transform.rotation = Quaternion.Lerp(
            playerBody.transform.rotation,
            lerpRot,
            Time.fixedDeltaTime * lerpSpeed);

        Vector2 moveVector = keyboardInputVector2d.normalized * characterSpeed * Time.fixedDeltaTime;
        playerRB.MovePosition(playerRB.position + moveVector);
    }
}

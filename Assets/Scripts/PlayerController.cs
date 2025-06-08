using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 keyboardInputVector2d;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] SPUM_Prefabs playerAnim;
    public float characterSpeed;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<SPUM_Prefabs>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyboardInputVector2d.x = Input.GetAxisRaw("Horizontal");
        keyboardInputVector2d.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if(keyboardInputVector2d != Vector2.zero)
        {
            playerAnim.PlayAnimation(1);
        }
        else playerAnim.PlayAnimation(0);

        Vector2 moveVector = keyboardInputVector2d.normalized * characterSpeed * Time.fixedDeltaTime;
        playerRB.MovePosition(playerRB.position + moveVector);
    }
}

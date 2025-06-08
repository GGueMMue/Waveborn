using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 keyboardInputVector2d;
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
            playerBody.transform.rotation = Quaternion.Lerp(
                playerBody.transform.rotation,
                lerpRot,
                Time.fixedDeltaTime * lerpSpeed);
            
            playerAnim.PlayAnimation(1);
        }
        else playerAnim.PlayAnimation(0);

        Vector2 moveVector = keyboardInputVector2d.normalized * characterSpeed * Time.fixedDeltaTime;
        playerRB.MovePosition(playerRB.position + moveVector);
    }
}

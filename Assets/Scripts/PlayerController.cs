using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 keyboardInputVector2d;
    [SerializeField] Rigidbody2D playerRB;

    public float characterSpeed;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
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
        Vector2 moveVector = keyboardInputVector2d.normalized * characterSpeed * Time.fixedDeltaTime;
        playerRB.MovePosition(playerRB.position + moveVector);
    }
}

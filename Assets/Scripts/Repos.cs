using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repos : MonoBehaviour
{
    Collider2D c2;

    private void Awake()
    {
        c2 = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Area")
        {
            Vector3 playerPos = GameManager.instance.pc.gameObject.transform.position;
            Vector3 myPos = transform.position;

            float calDisX = Mathf.Abs(playerPos.x - myPos.x);
            float calDisY = Mathf.Abs(playerPos.y - myPos.y);

            Vector3 playerDir = GameManager.instance.pc.keyboardInputVector2d;

            float xDir;
            float yDir;

            if(playerDir.x < 0) xDir = -1;
            else xDir = 1;

            if (playerDir.y < 0) yDir = -1;
            else yDir = 1;

            switch(transform.tag) 
            {
                case "Ground":
                    if(calDisX > calDisY)
                    {
                        transform.Translate(Vector3.right * xDir * 40);
                    }
                    else if(calDisX < calDisY)
                    {
                        transform.Translate(Vector3.up * yDir * 40);
                    }
                    break;

                case "Enemy":
                    if(c2.enabled)
                    {
                        transform.Translate(playerDir * 20 + 
                            new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                    }

                    break;

                default: 

                    break;
                
            }
        }
    }
}

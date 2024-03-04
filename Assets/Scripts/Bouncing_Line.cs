using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing_Line : MonoBehaviour
{
   // GameObject player;
   // Rigidbody2D playerrb;
    Animator lineAnimator;

    private bool isActivated = false;
    void Start()
    {
        /*
        if(player == null)
        {
            Debug.LogError("플레이어 게임오브젝트를 할당해 주세요. 이번 한 번만입니다.");
            player = FindObjectOfType<PlayerController>().gameObject;
        }
        playerrb = player.GetComponent<Rigidbody2D>();
        */
        lineAnimator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isActivated)
        {
            isActivated = true;
            lineAnimator.SetBool("isActivated", isActivated);
            Rigidbody2D other = collision.gameObject.GetComponent<Rigidbody2D>();
            //collision.gameObject.GetComponent<SpriteRenderer>().flipY ^= true;
            
            other.gravityScale *= -1;
            other.velocity *= 0f;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isActivated = false;
        lineAnimator.SetBool("isActivated", isActivated);
    }

}

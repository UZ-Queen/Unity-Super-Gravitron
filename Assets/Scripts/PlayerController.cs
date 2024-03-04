using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    SpriteRenderer playerRenderer;

    public float moveSpeed { get; private set; } = 500f;
    [HideInInspector]
    public int maxJumpCount = 2;
    public int jumpCount { get; private set; }
    public float jumpHeight = 5f;
    private bool isGrounded = false;

    public bool isVMode { get; private set; } = true;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("플레이어의 속도: " + moveSpeed);
        Debug.Log("중력: " + playerRigidBody.gravityScale);
    }
    private void Awake()
    {

    }
    private void FixedUpdate()
    {
        if (GameManager.instance.isGameOver == true)
            return;

        Move();
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver == true)
            return;

        if (!isVMode)
        {
            Jump();
        }
        else if(playerInput.jump && isGrounded)
        {
            Debug.Log("중력 -1배");
            playerRigidBody.gravityScale *= -1;
        }



        //gravityScale이랑 gravity.y와 헷갈리지 말자
        if(playerRigidBody.gravityScale > 0 && playerRenderer.flipY)
        {
            playerRenderer.flipY = false;
        }
        else if(playerRigidBody.gravityScale < 0 && !playerRenderer.flipY)
        {
            playerRenderer.flipY = true;
        }
        
        
    }


    private void Move()
    {
        Vector2 move = new Vector2(moveSpeed * Time.deltaTime * playerInput.move, playerRigidBody.velocity.y);
        //playerRigidBody.AddForce()
        playerRigidBody.velocity = move;
        //playerRigidBody.MovePosition(playerRigidBody.position + move);

        if (isVMode)
        {
            if (playerRigidBody.velocity.y >= 18)
            {
                //Debug.LogWarning("경고: 수직 속도가 18을 초과했습니다. 수직 속도는 18보다 클 수 없습니다.");
               // playerRigidBody.velocity = new Vector2(0, 18);
            }
        }

    }

    private void Jump()
    {

        if (jumpCount < maxJumpCount && playerInput.jump)
        {
            Debug.Log("점프 시도함!");
            //playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
            playerRigidBody.velocity -= new Vector2(0,playerRigidBody.velocity.y);
           // playerRigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            playerRigidBody.AddForce(Vector2.up * Mathf.Sqrt(Physics.gravity.y * -2f * jumpHeight) * Mathf.Sign(playerRigidBody.gravityScale), ForceMode2D.Impulse);
            jumpCount++;
        }
        else if (playerInput.jumpRelease && playerRigidBody.velocity.y > 0 )
        {
            playerRigidBody.velocity -= playerRigidBody.velocity * 0.5f;
        }
    }

    void Die()
    {
        GameManager.instance.isGameOver = true;
        playerAnimator.enabled = false;
        playerRigidBody.gravityScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "아야해요")
        {
            Die();
            playerRigidBody.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isVMode)
        {
            if (Mathf.Abs(collision.GetContact(0).normal.y) >= 0.7)
            {
                isGrounded = true;
                jumpCount = 0;
            }
        }
        else
        {
            if (collision.GetContact(0).normal.y >= 0.7)
            {
                isGrounded = true;
                jumpCount = 0;
            }
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        Debug.Log("현재 공중에 떠 있는 중!");
    }

}

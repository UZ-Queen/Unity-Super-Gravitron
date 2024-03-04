using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float defaultCameraLocationY = -3;
    public bool isStop { get; private set; }

    PlayerController player;
    EdgeCollider2D eowTrigger;
    Rigidbody2D rb2D;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        // eowTrigger = GetComponentInChildren<EdgeCollider2D>();
        eowTrigger = GetComponent<EdgeCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        if(player == null)
        {
            Debug.LogError("CameraFollower : 플레이어를 찾지 못했습니다.");
        }
        if (eowTrigger == null)
        {
            Debug.LogError("CameraFollower : 콜라이더를 할당하지 못했습니다.");
        }
        Debug.LogWarning("리지드바디의 isKinematic = " + rb2D.isKinematic);

    }

    // Update is called once per frame
    void Update()
    {
        if(isStop != true)
        {
            rb2D.velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, rb2D.velocity.y);
            //rb2D.MovePosition(new Vector3(player.gameObject.transform.position.x, 0, 0));
            //transform.position = new Vector3(player.gameObject.transform.position.x, 0, 0);
        }
        

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning("충돌 감지");
        if (collision.gameObject.tag == "EOW")
        {
            Vector2 dir = collision.GetContact(0).normal;
            Debug.Log("충돌지점의 법선백터: " + dir);

        }
    }



}

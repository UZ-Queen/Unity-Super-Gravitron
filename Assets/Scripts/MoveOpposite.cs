using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOpposite : MonoBehaviour
{

    bool isActivated = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("충돌 감지됨. 플레이어를 반대쪽으로 이동시킵니다.");
            Vector2 location = collision.gameObject.transform.position;
            Vector2 gap = Vector3.right * 0.12f;

            collision.gameObject.transform.position = location * new Vector2(-1,1) + gap * Mathf.Sign(location.x);
            isActivated = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isActivated = false;
    }
}

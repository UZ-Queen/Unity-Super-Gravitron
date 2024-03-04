using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangerous_Coin : MonoBehaviour
{
    int hp;
    private void OnEnable()
    {
        hp = 2;
    }

    private void Update()
    {
        if( hp == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "OtherDead")
        {
            hp--;
        }
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
        }

        // GameManager.instance.GetScore();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 20f;

    // Update is called once per frame
    private Vector2 dirVector;
    public Vector2 dir { get; set; } = Vector2.right;

    private void OnEnable()
    {
       // dir = Vector2.right;
    }
    void Update()
    {
        //if (GameManager.instance.isGameOver == true)
            //return;
        //transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void Up()
    {
        dirVector = Vector2.up;
    }
    public void Down()
    {
        dirVector = Vector2.down;
    }
    public void Left()
    {
        dirVector = Vector2.left;
    }
    public void Right()
    {
        dirVector = Vector2.right;
    }


}

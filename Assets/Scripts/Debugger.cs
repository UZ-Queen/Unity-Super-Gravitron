using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Debugger : MonoBehaviour
{
    
    public TextMeshProUGUI velocity;
    public TextMeshProUGUI maxVelocity;
    public TextMeshProUGUI vChekcer;
    public TextMeshProUGUI jumpCount;
    public TextMeshProUGUI currentPattern;
    public TextMeshProUGUI remainingCount;

    public GameObject player;
    public GameObject spawner;
    Rigidbody2D rb2D;
    PlayerController playerController;
    Super_Gravitron gravitron;


    void Start()
    {
        
        rb2D = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
        gravitron = spawner.GetComponent<Super_Gravitron>();
    }

    
    float t = 0;
    float maxX, maxY;
    void Update()
    {
        if (Mathf.Abs(rb2D.velocity.x) > Mathf.Abs( maxX))
            maxX = rb2D.velocity.x;
        if (Mathf.Abs(rb2D.velocity.y) > Mathf.Abs(maxY))
            maxY = rb2D.velocity.y;
        maxVelocity.text = string.Format("Max Velocity: x: {0:0.#} y: {1:0.#}", maxX, maxY);
        jumpCount.text = string.Format("Jump:{0}", playerController.isVMode ? "Disabled" : (playerController.maxJumpCount - playerController.jumpCount).ToString());

        //vChekcer.text = string.Format($"How is velocity calculated: {playerController.moveSpeed} * {Time.deltaTime:0.##} * {player.GetComponent<PlayerInput>().move:0.#}");
        currentPattern.text = "Current Pattern = " + gravitron.iChooseYou;
        remainingCount.text = "Pattern Changes after.. " + gravitron.count;
        t += Time.deltaTime;
        if(t > 0.2)
        {
            velocity.text = string.Format("Velocity: {0,8}", rb2D.velocity);
            t = 0;
        }
        
    }
}

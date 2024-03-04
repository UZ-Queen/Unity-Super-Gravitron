using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using static System.Console;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool isGameOver = false;
    public float score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverUI;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("씬에 두 개 이상의 게임메니저가 존재하려고 했습니다.");
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime;
        }
        

        scoreText.text = string.Format("Score : {0:##.##}", score);
        if(isGameOver == true)
        {
            gameOverUI.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.R) == true)
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void GetScore()
    {
        if(isGameOver == true)
        {
            Debug.LogWarning("참고: 게임오버 상태에서 점수를 증가하려 했습니다.");
        }
        else
        {
            //this.score++;
            Debug.LogError("비활성화된 점수 시스템이니 스크립트를 확인하세요.");
        }
        
    }

    
}

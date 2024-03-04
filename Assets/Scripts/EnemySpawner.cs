using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    private GameObject[] enemies;

    private Vector2 poolTransform = new Vector2(-50, -50);
    private int poolCapacity = 10;
    private static int currentIdx = 0;

    public float timeBetSpawnMax = 1f;
    public float timeBetSpawnMin = 0.3f;
    private float timeBetSpawn = 0f;
    private float lastSpawnTime = 0f;

    public GameObject LWall;
    public GameObject RWall;

    private float LWallX, RWallX;

    private float spawnStartX = -5;
    private float spawnEndX = 5;
    private float spawnY = 8;

    private bool patternChange = false;
    int iChooseYou = 0;
    private int[] patternLimit = new int[4];
    // Start is called before the first frame update
    void Start()
    {
        if(LWall != null && RWall != null)
        {
            LWallX = LWall.transform.position.x + LWall.GetComponent<BoxCollider2D>().size.y * 0.5f;
            RWallX = RWall.transform.position.x - LWall.GetComponent<BoxCollider2D>().size.y * 0.5f;
        }

        SetSpawnRange(LWallX, RWallX);
        patternChange = false;
        iChooseYou = 0;


        enemies = new GameObject[poolCapacity];

        for(int i=0; i<enemies.Length; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, poolTransform, Quaternion.identity); 
        }

        //timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver == true)
            return;
        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            if (patternChange)
            {
                //iChooseYou = Random.Range(1, 4);
               // iChooseYou = 1;
            }

            switch (iChooseYou)
            {
                case 1:
                    SpawnPattern1();
                    break;
                case 2:

                    break;

                case 3:

                    break;
            }
            SpawnPattern1();
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            if(currentIdx > enemies.Length-1)
            {
                Debug.Log(string.Format("현재 Idx가 {0}라서 0으로 초기화합니다.", currentIdx));
                currentIdx = 0;
            }
            lastSpawnTime = Time.time;
        }






    }

    void SetSpawnRange(float start, float end)
    {
        spawnStartX = start;
        spawnEndX = end;
    }

    //랜덤 위치에 한 개의 적 스폰
    void SpawnPattern1()
    {
        enemies[currentIdx].SetActive(true);
        enemies[currentIdx].transform.position = new Vector2(Random.Range(spawnStartX, spawnEndX), spawnY);
        currentIdx++;
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_Gravitron : MonoBehaviour
{

    public GameObject enemyPrefab;
    private GameObject[] enemies;

    public GameObject[] arrows = new GameObject[2];

    private Vector2 poolTransform = new Vector2(-50, -50);
    private int poolCapacity = 16;
    private int currentIdx = 0;


    private float timeBetSpawn = 0f;
    private float lastSpawnTime = 0f;

    public GameObject[] spawnPoint = new GameObject[6];



    private bool changePattern = false;
    public int iChooseYou { get; private set; } = 0;
    public int count { get; private set; } = 0;
    private bool isReverse = false;
    void Start()
    {
        //Debug.LogError(string.Format($"(3,2) * (-1,-0) = {new Vector2(3, 2) * Vector2.left}"));
        changePattern = false;
        iChooseYou = 0;
        enemies = new GameObject[poolCapacity];

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, poolTransform, Quaternion.identity);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver == true)
            return;
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            //패턴을 바꿀 때, 카운트를 초기화하고 왼쪽 오른쪽 어디서 스폰할 지 설정함.
            if (changePattern)
            {
                iChooseYou = Random.Range(1, 10);
                
                //iChooseYou = 1;
                Debug.Log(string.Format($"패턴 {iChooseYou} 시작."));
                count = 0;
                changePattern = false;
                if(Random.Range(0,2) == 0)
                {
                    isReverse = false;
                }
                else
                {
                    isReverse = true;
                }
            }

            switch (iChooseYou)
            {
                case 1:
                    SpawnRandomly();
                    break;
                case 2:
                    SpawnUp3Down3();
                    break;

                case 3:
                    SpawnDiamond();
                    break;
                case 4:
                    SpawnCross();
                    break;
                case 5:
                    SpawnRandomCross();
                    break;
                case 6:
                    SpawnUpDown1();
                    break;
                case 7:
                    SpawnUpDown2();
                    break;
                case 8:
                    SpawnMid();
                    break;
                case 9:
                    SpawnSlash();
                    break;
                default:
                    changePattern = true;
                    Debug.LogWarning("해당되는 패턴을 찾을 수 없어서 초기화합니다.");
                    break;
            }
            //스폰 시간은 패턴에서 정함.
            //timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            lastSpawnTime = Time.time;
        }

    }

    bool damn = false;
    IEnumerator Arrow2(int idx)
    {
        
        int a = isReverse == false ? 0 : 1;
        GameObject arrow = arrows[a].transform.GetChild(idx).gameObject;
        arrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        arrow.SetActive(false);
    }
    //opposite가 0이면 왼쪽, 1이면 오른쪽에서 스폰됨 ㅇㅇ
    void Spawn(Vector2 location, int idx)
    {
        //안내 화살표가 나오고서 코인이 나오게 하기 위해 위치 적절히 수정.
        location += Vector2.left * 2;
        StartCoroutine(Arrow2(idx));
        
        enemies[currentIdx].SetActive(true);
        enemies[currentIdx].transform.position = location;
        if(isReverse)
        {
            enemies[currentIdx].transform.position = location * new Vector2(-1, 1);
            enemies[currentIdx].GetComponent<MovingObject>().dir = Vector2.left;

        }
        else
        {
            enemies[currentIdx].GetComponent<MovingObject>().dir = Vector2.right;
        }
        Debug.Log(string.Format($"{location}에 코인 배치!"));
        currentIdx++;
        if (currentIdx > enemies.Length - 1)
        {
            Debug.Log(string.Format("현재 Idx가 {0}라서 0으로 초기화합니다.", currentIdx));
            currentIdx = 0;
        }
    }


    //랜덤 위치에 한 개의 장애물 스폰, 6회 반복
    void SpawnRandomly()
    {
        int idx = Random.Range(0, spawnPoint.Length);
        Spawn(spawnPoint[idx].transform.position, idx);
        count++;
        timeBetSpawn = 0.3f;
        if(count > 5)
        {
            changePattern = true;
        }

    }

    //위 3개, 또는 밑 3개를 동시에 스폰, 6회 반복
    void SpawnUp3Down3()
    {
        if(count %2 == 0)
        {
            for(int i = 0; i<3; i++)
            {
                Spawn(spawnPoint[i].transform.position, i);
            }
        }
        else
        {
            for (int i = 3; i < 6; i++)
            {
                Spawn(spawnPoint[i].transform.position, i);
            }
        }
        count++;
        timeBetSpawn = 0.5f;
        if (count > 5)
        {
            changePattern = true;
            timeBetSpawn = 1f;
        }


    }
    //가운데에 다이아몬드 모양의 장애물을 날립니다. 1회 반복합니다.
    void SpawnDiamond()
    {
        Spawn(spawnPoint[1].transform.position,1);

        Spawn(spawnPoint[2].transform.position + new Vector3(0.6f,0,0),2);
        Spawn(spawnPoint[2].transform.position + new Vector3(-0.6f, 0, 0),2);

        Spawn(spawnPoint[3].transform.position + new Vector3(0.6f, 0, 0),3);
        Spawn(spawnPoint[3].transform.position + new Vector3(-0.6f, 0, 0),3);

        Spawn(spawnPoint[4].transform.position,4);
        timeBetSpawn = 1f;
        changePattern = true;
    }

    //왼쪽i, 오른쪽7-i에서 장애물 날림. i<7, i++
    void SpawnCross()
    {
        Spawn(spawnPoint[count].transform.position, count);
        isReverse ^= true;
        Spawn(spawnPoint[5 - count].transform.position, 5 - count);
        count++;
        timeBetSpawn = 0.7f;
        if (count > 5)
        {
            changePattern = true;
            timeBetSpawn = 1.1f;
        }
    }

    void SpawnRandomCross()
    {
        int random = Random.Range(0, 6);
        Spawn(spawnPoint[random].transform.position, random);
        isReverse ^= true;
        Spawn(spawnPoint[5 - random].transform.position, 5 - random);
        count++;
        timeBetSpawn = 0.7f;
        if (count > 5)
        {
            changePattern = true;
            timeBetSpawn = 1.1f;
        }
    }

    void SpawnUpDown1()
    {
        Spawn(spawnPoint[0].transform.position, 0);
        Spawn(spawnPoint[5].transform.position, 5);

        changePattern = true;
        timeBetSpawn = 0.7f;
    }
    void SpawnUpDown2()
    {
        Spawn(spawnPoint[1].transform.position, 1);
        Spawn(spawnPoint[4].transform.position, 4);

        changePattern = true;
        timeBetSpawn = 0.7f;
    }
    void SpawnMid()
    {
        Spawn(spawnPoint[2].transform.position, 2);
        Spawn(spawnPoint[3].transform.position, 3);

        changePattern = true;
        timeBetSpawn = 0.7f;
    }
    void SpawnSlash()
    {
        Spawn(spawnPoint[0].transform.position, 0);
        Spawn(spawnPoint[1].transform.position - Vector3.right * 0.4f, 1);
        Spawn(spawnPoint[4].transform.position - Vector3.right * 1.6f, 4);
        Spawn(spawnPoint[5].transform.position - Vector3.right * 2.0f, 5);

        changePattern = true;
        timeBetSpawn = 0.7f;
    }
    void SpawnReverseSlash()
    {
        Spawn(spawnPoint[5].transform.position, 0);
        Spawn(spawnPoint[4].transform.position - Vector3.right * 0.4f, 1);
        Spawn(spawnPoint[1].transform.position - Vector3.right * 1.6f, 4);
        Spawn(spawnPoint[0].transform.position - Vector3.right * 2.0f, 5);

        changePattern = true;
        timeBetSpawn = 0.7f;
    }

    void SpawnSideMid()
    {
        Spawn(spawnPoint[0].transform.position, 0);
        Spawn(spawnPoint[5].transform.position, 0);

        Spawn(spawnPoint[0].transform.position, 0);
        Spawn(spawnPoint[0].transform.position, 0);
    }
}

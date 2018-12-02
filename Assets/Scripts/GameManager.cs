using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> enemies;
    public List<Transform> spawnPoints;

    private bool gameIsRunning;
    private bool waitForSpawn;
    private float spawnTime;

    public List<GameObject> objectives;
    public GameObject enemyPrefab;

    System.Random rnd;

    private int counter;
    private int roundCounter;
    public int Round { get; private set; }
    public int Lives { get; private set; }
    public int Points { get; private set; }

	// Use this for initialization
	void Start () {
        gameIsRunning = true;
        waitForSpawn = false;
        rnd = new System.Random();
        spawnTime = 3.0f;
        Round = 1;
        counter = 0;
        roundCounter = 0;
        Lives = 3;
        Points = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameIsRunning && !waitForSpawn)
            SpawnEnemy();
	}

    public void RemoveLive()
    {
        Lives -= 1;
        if (Lives == 0)
            LostGame();
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
        counter += 1;
        roundCounter += 1;
        if (roundCounter == 20)
        {
            roundCounter = 0;
            Round += 1;
            spawnTime -= 0.5f;
            if (spawnTime <= 0)
                WonGame();
        }
        Points += 10 * Round;
    }

    public Transform GetTarget()
    {
        int nr = rnd.Next(2);
        return objectives[nr].transform;
    }

    private void SpawnEnemy()
    {
        int spawn = rnd.Next(4);
        Instantiate(enemyPrefab, spawnPoints[spawn].gameObject.transform.position, Quaternion.identity);
        waitForSpawn = true;
        StartCoroutine(WaitSecs(spawnTime));
    }

    private void WonGame()
    {
        Debug.Log("YouWon");
        gameIsRunning = false;
    }

    private void LostGame()
    {
        Debug.Log("You Lost");
        gameIsRunning = false;
    }

    IEnumerator WaitSecs(float spTime)
    {
        yield return new WaitForSeconds(spTime);
        waitForSpawn = false;
    }
}

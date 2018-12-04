using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> enemies;
    public List<Transform> spawnPoints;

    public bool gameIsRunning;
    private bool waitForSpawn;
    private float spawnTime;
    private bool spawnOver;

    public List<GameObject> objectives;
    public GameObject enemyPrefab;

    System.Random rnd;

    private int counter;
    private int roundCounter;
    public int Round { get; private set; }
    public int Lives { get; private set; }
    public int Points { get; private set; }
    public int Highscore { get; private set; }

    public AudioClip hit;
    public AudioClip end;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        rnd = new System.Random();
        Highscore = 0;
        ResetGame();
        source = GetComponent<AudioSource>();
        spawnOver = false;
    }

	
	// Update is called once per frame
	void Update () {
        if (gameIsRunning && !waitForSpawn && !spawnOver)
            SpawnEnemy();
        if (spawnOver)
            if (enemies.Count <= 0)
                WonGame();
	}

    public void StartGame()
    {
        ResetGame();
        gameIsRunning = true;
    }

    public void EndGame()
    {
        gameIsRunning = false;
        if (Points > Highscore)
            Highscore = Points;
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GetComponent<GameUI>().OpenUI();
    }

    public void ResetGame()
    {
        waitForSpawn = false;
        Round = 1;
        counter = 0;
        roundCounter = 0;
        Lives = 3;
        Points = 0;
        spawnTime = 3.0f;

        Vector3 player = new Vector3(0, 1, 0);
        GameObject.Find("Spieler").transform.position = player;
    }

    public void RemoveLive()
    {
        source.PlayOneShot(end);
        Lives -= 1;
        if (Lives == 0)
            LostGame();
    }

    public void AddPoints()
    {
        Points += 10 * Round;
    }

    public void RemoveEnemy(GameObject enemy, bool addPoints)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
        if (addPoints)
            AddPoints();
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

        counter += 1;
        roundCounter += 1;
        if (roundCounter == 20)
        {
            roundCounter = 0;
            Round += 1;
            spawnTime -= 0.5f;
            if (spawnTime <= 0)
                spawnOver = true;
        }
    }

    private void WonGame()
    {
        GetComponent<GameUI>().headline.SetText("Du hast gewonnen!");
        EndGame();
    }

    private void LostGame()
    {
        GetComponent<GameUI>().headline.SetText("Du hast verloren...");
        EndGame();
    }

    public void PlayHit()
    {
        source.PlayOneShot(hit);
    }

    IEnumerator WaitSecs(float spTime)
    {
        yield return new WaitForSeconds(spTime);
        waitForSpawn = false;
    }
}

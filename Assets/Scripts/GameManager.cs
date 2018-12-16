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
    private bool wonGame;

    public List<GameObject> objectives;
    public GameObject enemyPrefab;

    public GameObject flakonPrefab;

    System.Random rnd;

    private int counter;
    private int roundCounter;
    public int Round { get; private set; }
    public int Lives { get; private set; }
    public int Points { get; private set; }
    public int Highscore { get; private set; }

    public AudioClip hit;
    public AudioClip end;
    public AudioClip runde;
    private AudioSource source;


    // Use this for initialization
    void Start () {
        rnd = new System.Random();
        Highscore = 0;
        ResetGame();
        source = GetComponent<AudioSource>();
    }

	
	// Update is called once per frame
	void Update () {
        if (gameIsRunning && !waitForSpawn && !spawnOver)
            SpawnEnemy();
        if (spawnOver && !wonGame)
            if (enemies.Count <= 0)
            {
                wonGame = true;
                WonGame();
            }
	}

    public void StartGame()
    {
        ResetGame();
        gameIsRunning = true;
    }

    public void EndGame()
    {
        gameIsRunning = false;
        AddPoints(Lives * 30);
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
        wonGame = false;
        spawnOver = false;
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

    public void AddPoints(int punkte)
    {
        Points += punkte * Round;
    }

    public void RemoveEnemy(GameObject enemy, bool addPoints)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
        if (addPoints)
            AddPoints(10);
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

        //spawn flakon
        int flakon;
        do
        {
            flakon = rnd.Next(4);
        } while (flakon == spawn);
        Instantiate(flakonPrefab, spawnPoints[flakon].gameObject.transform.position, Quaternion.Euler(-90.0f, 0, 0));

        counter += 1;
        roundCounter += 1;
        if (roundCounter == 20)
        {
            source.PlayOneShot(runde);
            roundCounter = 0;
            Round += 1;
            spawnTime -= 0.5f;
            if (spawnTime <= 0.5f)
                spawnOver = true;
            Debug.Log("Runde " + spawnTime.ToString());
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

    public void SoundOnOff()
    {
        //GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = !GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled;
        if (AudioListener.volume == 0)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }

    IEnumerator WaitSecs(float spTime)
    {
        yield return new WaitForSeconds(spTime);
        waitForSpawn = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour {

    public TextMeshProUGUI lebenObj;
    public TextMeshProUGUI punkteObj;
    public TextMeshProUGUI rundeObj;
    private GameManager gm;

    public TextMeshProUGUI startText;
    public TextMeshProUGUI headline;
    public List<GameObject> startUI;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        lebenObj.SetText("Leben: " + gm.Lives.ToString());
        punkteObj.SetText("Punkte: " + gm.Points.ToString());
        rundeObj.SetText("Runde: " + gm.Round.ToString());
	}

    public void CloseUI()
    {
        foreach (GameObject go in startUI)
        {
            go.SetActive(false);
        }
        gm.StartGame();
    }

    public void OpenUI()
    {
        foreach (GameObject go in startUI)
        {
            go.SetActive(true);
        }
        startText.SetText("Deine Punkte: " + gm.Points.ToString() + "\nDein Highscore: " + gm.Highscore.ToString());
    }
}

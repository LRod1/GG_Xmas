using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GegnerAI : MonoBehaviour {

    public NavMeshAgent agent;
    public Transform target;
    GameManager gm;

    public GameObject flakonPrefab;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Use this for initialization
    void Start () {
        agent.SetDestination(gm.GetTarget().position);
        gm.enemies.Add(this.gameObject);
	}
}

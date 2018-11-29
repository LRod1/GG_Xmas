using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GegnerAI : MonoBehaviour {

    public NavMeshAgent agent;
    public Transform target;

	// Use this for initialization
	void Start () {
        agent.SetDestination(target.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

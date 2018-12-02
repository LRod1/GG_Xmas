using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flakon : MonoBehaviour {

    public GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(WaitSecs());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.AddPoints();
            Destroy(this.gameObject);
        }
    }

    IEnumerator WaitSecs()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}

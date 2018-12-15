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
            gm.PlayHit();
            gm.AddPoints(20);
            Destroy(this.gameObject);
        }
    }

    IEnumerator WaitSecs()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}

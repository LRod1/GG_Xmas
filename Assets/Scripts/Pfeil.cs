using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pfeil : MonoBehaviour {

    //Bewegung
    public Rigidbody body;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed = 20;

    public ParticleSystem part;
    public float xdir;
    public float ydir;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        body.velocity = new Vector3(xdir * runSpeed, 0, ydir * runSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(this.gameObject);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(part, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}

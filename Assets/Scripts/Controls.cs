using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    //Bewegung
    Rigidbody body;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed;

    //Schiessen
    public enum Direction
    {
        oben,
        unten,
        links,
        rechts
    }
    public Direction direction;
    public bool fireReady;
    public GameObject pfeil;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        fireReady = true;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //fürs schießen
        if (horizontal != 0 || vertical != 0)
            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
                direction = (horizontal > 0) ? Direction.rechts : Direction.links;
            else
                direction = (vertical > 0) ? Direction.oben : Direction.unten;

        if (Input.GetButton("Fire1") && fireReady)
        {
            Feuer();
            fireReady = false;
            StartCoroutine(WaitSecs());
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
            body.velocity = new Vector3((horizontal * runSpeed) * moveLimiter, 0, (vertical * runSpeed) * moveLimiter);
        else
            body.velocity = new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
    }


    private void Feuer()
    {
        float xdir;
        float ydir;
        Quaternion quat;

        switch (direction)
        {
            case Direction.oben:
                quat = Quaternion.identity;
                xdir = 0;
                ydir = 1.0f;
                break;
            case Direction.unten:
                quat = Quaternion.Euler(0, 180.0f, 0);
                xdir = 0;
                ydir = -1.0f;
                break;
            case Direction.rechts:
                quat = Quaternion.Euler(0, 90.0f, 0);
                xdir = 1.0f;
                ydir = 0;
                break;
            case Direction.links:
                quat = Quaternion.Euler(0, -90.0f, 0);
                xdir = -1.0f;
                ydir = 0;
                break;
            default:
                quat = Quaternion.identity;
                xdir = 0;
                ydir = 1.0f;
                break;
        }
        GameObject neuerPfeil = Instantiate(pfeil, transform.position, quat);
        neuerPfeil.GetComponent<Pfeil>().xdir = xdir;
        neuerPfeil.GetComponent<Pfeil>().ydir = ydir;
    }



    IEnumerator WaitSecs()
    {
        yield return new WaitForSeconds(0.3f);
        fireReady = true;
    }
}

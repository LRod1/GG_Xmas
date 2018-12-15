using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    private GameManager gm;

    //Bewegung
    Rigidbody body;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float runSpeed;
    private AudioSource source;
    public AudioClip bow;
    public GameObject weihnachtsmann;

    //Schiessen
    public enum Direction
    {
        oben,
        unten,
        links,
        rechts,
        obenlinks,
        obenrechts,
        untenlinks,
        untenrechts
    }
    public Direction direction;
    public bool fireReady;
    public GameObject pfeil;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        body = GetComponent<Rigidbody>();
        fireReady = true;
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gm.gameIsRunning)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                gm.EndGame();

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            //fürs schießen
            if (horizontal != 0 || vertical != 0)
                if (horizontal > 0.5f)
                {
                    //rechts
                    if (vertical > 0.5f)
                        direction = Direction.obenrechts;
                    else if (vertical < -0.5f)
                        direction = Direction.untenrechts;
                    else
                        direction = Direction.rechts;
                }
                else if (horizontal < -0.5f)
                {
                    //links
                    if (vertical > 0.5f)
                        direction = Direction.obenlinks;
                    else if (vertical < -0.5f)
                        direction = Direction.untenlinks;
                    else
                        direction = Direction.links;
                }
                else if (vertical > 0.5f)
                {
                    direction = Direction.oben;
                }
                else if (vertical < -0.5f)
                {
                    direction = Direction.unten;
                }

            RotateWeihnachtsmann();


            if (Input.GetButton("Fire1") && fireReady)
            {
                Feuer();
                fireReady = false;
                StartCoroutine(WaitSecs());
                source.PlayOneShot(bow);
            }
        }
        else
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
            body.velocity = new Vector3((horizontal * runSpeed) * moveLimiter, 0, (vertical * runSpeed) * moveLimiter);
        else
            body.velocity = new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);
    }


    private void RotateWeihnachtsmann()
    {
        Quaternion quat;
        switch (direction)
        {
            case Direction.unten:
                quat = Quaternion.identity;
                break;
            case Direction.oben:
                quat = Quaternion.Euler(0, 180.0f, 0);
                break;
            case Direction.links:
                quat = Quaternion.Euler(0, 90.0f, 0);
                break;
            case Direction.rechts:
                quat = Quaternion.Euler(0, -90.0f, 0);
                break;
            case Direction.untenlinks:
                quat = Quaternion.Euler(0, 45.0f, 0);
                break;
            case Direction.obenlinks:
                quat = Quaternion.Euler(0, 135.0f, 0);
                break;
            case Direction.obenrechts:
                quat = Quaternion.Euler(0, 235.0f, 0);
                break;
            case Direction.untenrechts:
                quat = Quaternion.Euler(0, 315.0f, 0);
                break;
            default:
                quat = Quaternion.identity;
                break;
        }
        weihnachtsmann.transform.rotation = quat;
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
            case Direction.obenrechts:
                quat = Quaternion.Euler(0, 45.0f, 0);
                xdir = 0.7f;
                ydir = 0.7f;
                break;
            case Direction.untenrechts:
                quat = Quaternion.Euler(0, 135.0f, 0);
                xdir = 0.7f;
                ydir = -0.7f;
                break;
            case Direction.untenlinks:
                quat = Quaternion.Euler(0, 235.0f, 0);
                xdir = -0.7f;
                ydir = -0.7f;
                break;
            case Direction.obenlinks:
                quat = Quaternion.Euler(0, 315.0f, 0);
                xdir = -0.7f;
                ydir = 0.7f;
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
        yield return new WaitForSeconds(0.2f);
        fireReady = true;
    }
}

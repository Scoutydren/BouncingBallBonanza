using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    GlobalScript global;
    private int numHits;
    private int hitThreshold;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<GlobalScript>();

        this.numHits = 0;
        this.hitThreshold = 0;

        // Place ball
        this.transform.position = new Vector3(0.75f, 1.0f, -4.4f);

        // Ignore collision between ball and player
        Physics.IgnoreLayerCollision(6, 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall()
    {
        // Reset ball in front of player hand
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        this.transform.position = GameObject.Find("RightHand").transform.position + new Vector3(0, 0, .3f);

        this.numHits = 0;
        this.hitThreshold = 20;
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("BackWallTag"))
        {
            this.ResetBall();
        }
        else if (collider.CompareTag("10PtTileTag"))
        {
            global.score += 10;
            this.numHits += 1;
        } else if (collider.CompareTag("20PtTileTag"))
        {
            global.score += 20;
            this.numHits += 1;
        } else if (collider.CompareTag("30PtTileTag"))
        {
            global.score += 30;
            this.numHits += 1;
        } else if (collider.CompareTag("EmptyTileTag"))
        {
            this.numHits += 1;
        }

        // Check if ball has collided more than 20 times
        if (this.numHits > this.hitThreshold)
        {
            this.ResetBall();
        }
    }
}

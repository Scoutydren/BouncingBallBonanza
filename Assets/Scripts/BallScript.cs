using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GlobalScript global;

    private Rigidbody rb;
    private int hitThreshold;
    private int numHits;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();

        this.rb = GetComponent<Rigidbody>(); ;
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
        this.rb.velocity = new Vector3(0, 0, 0);
        this.rb.angularVelocity = new Vector3(0, 0, 0);
        this.transform.position = GameObject.Find("RightHand").transform.position + new Vector3(0, 0, .3f);
        
        this.hitThreshold = 20;
        this.global.FinishThrow();
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        // Point tiles
        if (collider.CompareTag("BackWallTag"))
        {
            this.ResetBall();
        }
        else if (collider.CompareTag("10PtTileTag"))
        {
            this.global.accumulatedScore += 10;
            this.numHits += 1;
        } 
        else if (collider.CompareTag("20PtTileTag"))
        {
            this.global.accumulatedScore += 20;
            this.numHits += 1;
        }
        else if (collider.CompareTag("30PtTileTag"))
        {
            this.global.accumulatedScore += 30;
            this.numHits += 1;
        } 
        
        // Force tiles
        else if (collider.CompareTag("LeftForceTileTag"))
        {
            this.rb.velocity += new Vector3(-4f, 0, 0);
            this.numHits += 1;
        }
        else if (collider.CompareTag("RightForceTileTag"))
        {
            this.rb.velocity += new Vector3(4f, 0, 0);
            this.numHits += 1;
        }
        else if (collider.CompareTag("UpForceTileTag"))
        {
            this.rb.velocity += new Vector3(0, 4f, 0);
            this.numHits += 1;
        }
        else if (collider.CompareTag("DownForceTileTag"))
        {
            this.rb.velocity += new Vector3(0, -4f, 0);
            this.numHits += 1;
        }
        
        // Multiplier tiles
        else if (collider.CompareTag("2xMultiplier"))
        {
            this.global.multiplier = 2;
        } 

        // Empty tiles
        else if (collider.CompareTag("EmptyTileTag"))
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

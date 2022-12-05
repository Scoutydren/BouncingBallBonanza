using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BallScript : MonoBehaviour
{
    public GlobalScript global;

    private Rigidbody rb;
    private Interactable interactable;
    private int hitThreshold;
    private int numHits;

    private bool isGrabbed = false;
    private bool isThrown = false;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.interactable = GetComponent<Interactable>();

        this.rb = GetComponent<Rigidbody>(); ;
        this.numHits = 0;
        this.hitThreshold = 10;

        // Place ball
        this.transform.position = new Vector3(0.75f, 1.0f, -4.4f);

        // Ignore collision between ball and player
        Physics.IgnoreLayerCollision(6, 7);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(interactable != null);
        // if (interactable != null && interactable.attachedToHand != null)
        {
        }
    }

    void ResetBall()
    {
        // Reset ball in front of player hand
        this.rb.velocity = new Vector3(0, 0, 0);
        this.rb.angularVelocity = new Vector3(0, 0, 0);
        this.transform.position = GameObject.Find("RightHand").transform.position + new Vector3(0, 0, .3f);

        this.numHits = 0;
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
            Debug.Log("10");
            this.global.accumulatedScore += 10;
            this.numHits += 1;
        } 
        else if (collider.CompareTag("20PtTileTag"))
        {
            Debug.Log("20");
            this.global.accumulatedScore += 20;
            this.numHits += 1;
        }
        else if (collider.CompareTag("30PtTileTag"))
        {
            Debug.Log("30");
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
            this.numHits += 1;
        }

        // Empty tiles
        else if (collider.CompareTag("EmptyTileTag"))
        {
            this.numHits += 1;
        }


        // Resets to prevent infinite loops
        if (this.numHits > this.hitThreshold)
        {
            this.ResetBall();
        }
    }
}

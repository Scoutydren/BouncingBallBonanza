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

    private bool isGrabbed;
    private bool isThrown;

    private float forceAmt;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.interactable = GetComponent<Interactable>();

        this.rb = GetComponent<Rigidbody>(); ;
        this.numHits = 0;
        this.hitThreshold = 50;

        this.isGrabbed = false;
        this.isThrown = false;

        this.forceAmt = 2f;

        // Place ball
        //this.transform.position = new Vector3(0.6f, 1.2f, -3.1f);
        this.transform.position = new Vector3(0f, 1.2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (interactable.attachedToHand == null)
        {
            if (!isThrown)
            {
                if (!isGrabbed)
                {
                    // Ball is in front of player
                    // Allow hand to go through ball
                    Physics.IgnoreLayerCollision(6, 7, true);
                }
                else
                {
                    // Ball was just thrown
                    isThrown = true;
                }
            }
            else
            {
                // Ball moving in cube
                // Allow player to hit ball
                Physics.IgnoreLayerCollision(6, 7, false);
            }
        }
        else
        {
            if (!isThrown)
            {
                // First time player picks it up
                isGrabbed = true;
            }
            else
            {
                if (numHits == 0)
                {
                    // Grabbed it before it collides with a wall
                    isThrown = false;
                }
                else
                {
                    // Picked up after thrown
                    this.global.accumulatedScore += 50;
                    ResetBall();
                }

            }
        }*/
    }

    void ResetBall()
    {
        // Reset ball in front of player hand
        this.rb.velocity = new Vector3(0, 0, 0);
        this.rb.angularVelocity = new Vector3(0, 0, 0);
        this.transform.position = GameObject.Find("VRCamera").transform.position + new Vector3(0, 0, 0.5f);

        this.numHits = 0;
        this.isGrabbed = false;
        this.isThrown = false;
        this.global.FinishThrow();
    }

    void ResetTile(GameObject tile, Renderer renderer, MeshRenderer meshRenderer)
    {
        // Set tile to empty tile
        tile.tag = "EmptyTileTag";
        meshRenderer.material = null;
        renderer.material.color = Color.white;
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        GameObject tile = collision.gameObject;
        Renderer renderer = tile.GetComponent<Renderer>();
        MeshRenderer meshRenderer = tile.GetComponent<MeshRenderer>();
        string wallName = tile.transform.parent.name;

        // Reset when ball hits black hole tile
        if (collider.CompareTag("BlackHoleTileTag"))
        {
            this.global.accumulatedScore -= 50;
            this.ResetBall();
        }
        // Point tiles
        else if (collider.CompareTag("10PtTileTag"))
        {
            this.global.accumulatedScore += 10;
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        } 
        else if (collider.CompareTag("20PtTileTag"))
        {
            this.global.accumulatedScore += 20;
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        else if (collider.CompareTag("30PtTileTag"))
        {
            this.global.accumulatedScore += 30;
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        
        // Force tiles
        else if (collider.CompareTag("LeftForceTileTag"))
        {
            if (wallName == "FrontWall" || wallName == "TopWall" || wallName == "BottomWall")
            {
                this.rb.velocity += new Vector3(-forceAmt, 0, 0);
            }
            else if (wallName == "BackWall")
            {
                this.rb.velocity += new Vector3(forceAmt, 0, 0);
            }
            else if (wallName == "LeftWall")
            {
                this.rb.velocity += new Vector3(0, 0, -forceAmt);
            }
            else if (wallName == "RightWall")
            {
                this.rb.velocity += new Vector3(0, 0, forceAmt);
            }
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        else if (collider.CompareTag("RightForceTileTag"))
        {
            if (wallName == "FrontWall" ||wallName == "TopWall" || wallName == "BottomWall")
            {
                this.rb.velocity += new Vector3(forceAmt, 0, 0);
            }
            else if (wallName == "BackWall")
            {
                this.rb.velocity += new Vector3(-forceAmt, 0, 0);
            }
            else if (wallName == "LeftWall")
            {
                this.rb.velocity += new Vector3(0, 0, forceAmt);
            }
            else if (wallName == "RightWall")
            {
                this.rb.velocity += new Vector3(0, 0, -forceAmt);
            }
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        else if (collider.CompareTag("UpForceTileTag"))
        {
            if (wallName == "FrontWall" || wallName == "BackWall" || wallName == "LeftWall" || wallName == "RightWall")
            {
                this.rb.velocity += new Vector3(0, forceAmt, 0);
            }
            else if (wallName == "TopWall")
            {
                this.rb.velocity += new Vector3(0, 0, -forceAmt);
            }
            else if (wallName == "BottomWall")
            {
                this.rb.velocity += new Vector3(0, 0, forceAmt);
            }
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        else if (collider.CompareTag("DownForceTileTag"))
        {
            if (wallName == "FrontWall" || wallName == "BackWall" || wallName == "LeftWall" || wallName == "RightWall")
            {
                this.rb.velocity += new Vector3(0, -forceAmt, 0);
            }
            else if (wallName == "TopWall")
            {
                this.rb.velocity += new Vector3(0, 0, forceAmt);
            }
            else if (wallName == "BottomWall")
            {
                this.rb.velocity += new Vector3(0, 0, -forceAmt);
            }
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        
        // Multiplier tiles
        else if (collider.CompareTag("2xMultiplier"))
        {
            this.global.multiplier = 2;
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
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

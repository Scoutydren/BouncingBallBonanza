using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class BallScript : MonoBehaviour
{
    public AudioClip collisionSound;
    public AudioClip slapSound;
    public AudioClip coinSound;
    public GameObject plus10UIPrefab;
    public GameObject plus20UIPrefab;
    public GameObject plus30UIPrefab;

    private GlobalScript global;
    private GameObject canvas;
    private GameObject camera;
    private Rigidbody rb;
    private Interactable interactable;
    private Throwable throwable;
    private int hitThreshold;
    private int numHits;

    private bool isGrabbed;
    private bool isThrown;

    private float forceAmt;

    void Awake()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.canvas = GameObject.Find("Canvas");
        this.camera = GameObject.Find("VRCamera");
        this.interactable = GetComponent<Interactable>();
        this.throwable = GetComponent<Throwable>();
        this.rb = GetComponent<Rigidbody>();

        this.numHits = 0;
        this.hitThreshold = 50;

        this.isGrabbed = false;
        this.isThrown = false;

        this.forceAmt = 2f;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetBall()
    {
        // Reset ball in front of player camera
        this.rb.velocity = new Vector3(0, 0, 0);
        this.rb.angularVelocity = new Vector3(0, 0, 0);
        this.transform.position = GameObject.Find("VRCamera").transform.position + new Vector3(0, 0, 0.5f);

        this.numHits = 0;
        this.isGrabbed = false;
        this.isThrown = false;
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

        OnCollisionSound(collider);

        // Reset when ball hits black hole tile
        if (collider.CompareTag("BlackHoleTileTag"))
        {
            this.global.score -= 50;
            this.ResetBall();
        }
        // Point tiles
        else if (collider.CompareTag("10PtTileTag"))
        {
            // Spawn + 10 points
            this.SpawnPointUI(plus10UIPrefab, collider);

            this.global.score += 10;
            this.numHits += 1;
            this.global.numPointTiles -= 1;
            this.ResetTile(tile, renderer, meshRenderer);
        } 
        else if (collider.CompareTag("20PtTileTag"))
        {
            // Spawn + 20 points
            this.SpawnPointUI(plus20UIPrefab, collider);

            this.global.score += 20;
            this.numHits += 1;
            this.global.numPointTiles -= 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        else if (collider.CompareTag("30PtTileTag"))
        {
            // Spawn + 30 points
            this.SpawnPointUI(plus30UIPrefab, collider);

            this.global.score += 30;
            this.numHits += 1;
            this.global.numPointTiles -= 1;
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

        // Throw tiles
        else if (collider.CompareTag("ThrowTileTag"))
        {
            this.global.IncrThrow();
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
            // depreicated because of timer
            // this.ResetBall();
        }
    }

    private void OnCollisionSound(Collider collider)
    {
        if (collider.CompareTag("Untagged") || collider.CompareTag("PlayerTag"))
        {
            AudioSource.PlayClipAtPoint(this.slapSound, this.gameObject.transform.position, 0.7f);
        }
        else if (collider.CompareTag("BlackHoleTileTag"))
        {
            // Add black hole sound effect
        }
        else
        {
            AudioSource.PlayClipAtPoint(this.collisionSound, this.gameObject.transform.position);
            if (collider.CompareTag("10PtTileTag") || collider.CompareTag("20PtTileTag") ||
                collider.CompareTag("30PtTileTag"))
            {
                AudioSource.PlayClipAtPoint(this.coinSound, this.gameObject.transform.position);
            }
        }
    }

    private void SpawnPointUI(GameObject prefab, Collider collider)
    {
        Vector3 interpPos = 0.75f * this.transform.position + 0.15f * collider.transform.position + 0.10f * this.camera.transform.position;

        GameObject ui = GameObject.Instantiate(prefab, interpPos, Quaternion.identity);
        ui.transform.parent = this.canvas.transform;
    }

    public void RemoveThrowability()
    {
        this.interactable.highlightOnHover = false;
        GameObject.Destroy(this.throwable);
    }

    public void AddThrowability()
    {
        this.throwable = this.gameObject.AddComponent<Throwable>();
        this.throwable.onDetachFromHand = new UnityEvent();
        this.throwable.onDetachFromHand.AddListener(this.global.DecrThrow);
        this.interactable.highlightOnHover = true;
    }
}

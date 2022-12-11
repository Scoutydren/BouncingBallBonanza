using System;
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
    public AudioClip errorSound;
    public AudioClip hourglassSound;
    public AudioClip snowflakeSound;
    public AudioClip flameSound;

    public GameObject plus10UIPrefab;
    public GameObject plus20UIPrefab;
    public GameObject plus30UIPrefab;
    public GameObject badTileUIPrefab;

    private GlobalScript global;
    private TimedEventScript timedEventScript;
    private GameObject canvas;
    private GameObject camera;
    private Rigidbody rb;
    private Interactable interactable;
    private Throwable throwable;

    private AudioSource levelAudio;
    private AudioSource tutorialAudio;

    private int hitThreshold;
    private int numHits;

    private float forceAmt;
    private float constantSpeed;

    void Awake()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.timedEventScript = GetComponent<TimedEventScript>();
        this.canvas = GameObject.Find("Canvas");
        this.camera = GameObject.Find("VRCamera");
        this.interactable = GetComponent<Interactable>();
        this.throwable = GetComponent<Throwable>();
        this.rb = GetComponent<Rigidbody>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        this.levelAudio = audioSources[0];
        this.tutorialAudio = audioSources[1];

        this.numHits = 0;
        this.hitThreshold = 50;

        this.forceAmt = 2f;
        this.constantSpeed = 1f;

        // Ignore collision between ball and player's head
        //Physics.IgnoreLayerCollision(6, 7);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Constant velocity
        this.rb.velocity = this.constantSpeed * rb.velocity.normalized * this.timedEventScript.speedMultiplier;
    }

    public void IncrSpeed(float speed)
    {
        this.constantSpeed += speed;
    }


    public void ResetBall()
    {
        // Reset ball in front of player camera
        this.rb.velocity = new Vector3(0, 0, 0);
        this.rb.angularVelocity = new Vector3(0, 0, 0);
        try
        {
            this.transform.position = GameObject.Find("VRCamera").transform.TransformPoint(new Vector3(0, 0, 0.5f));
        }
        catch (Exception e)
        {
            Debug.Log("Exception in BallScript.cs ResetBall()");
        }

        this.numHits = 0;
    }

    void ResetTile(GameObject tile, Renderer renderer, MeshRenderer meshRenderer)
    {
        // Set tile to empty tile
        tile.tag = "EmptyTileTag";
        meshRenderer.material = null;
        if (this.global.level <= 3)
        {
            renderer.material.color = Color.white;
        }
        else
        {
            renderer.material.color = new Color(0, 1, 1);
        }
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
            this.SpawnPointUI(badTileUIPrefab, collider, false);

            this.global.score -= 50;
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        // Point tiles
        else if (collider.CompareTag("10PtTileTag"))
        {
            // Spawn + 10 points
            this.SpawnPointUI(plus10UIPrefab, collider, this.global.multiplier > 1);

            this.global.score += 10 * this.global.multiplier;
            this.global.multiplier = 1;
            this.numHits += 1;
            this.global.numPointTiles -= 1;
            this.ResetTile(tile, renderer, meshRenderer);
        } 
        else if (collider.CompareTag("20PtTileTag"))
        {
            // Spawn + 20 points
            this.SpawnPointUI(plus20UIPrefab, collider, this.global.multiplier > 1);

            this.global.score += 20 * this.global.multiplier;
            this.global.multiplier = 1;
            this.numHits += 1;
            this.global.numPointTiles -= 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }
        else if (collider.CompareTag("30PtTileTag"))
        {
            // Spawn + 30 points
            this.SpawnPointUI(plus30UIPrefab, collider, this.global.multiplier > 1);

            this.global.score += 30 * this.global.multiplier;
            this.global.multiplier = 1;
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

        // Hourglass tiles
        else if (collider.CompareTag("HourglassTileTag"))
        {
            this.global.timer += this.global.maxTimer / 6;
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }

        // Snowflake tiles
        else if (collider.CompareTag("SnowflakeTileTag"))
        {
            this.timedEventScript.AddEffect(TimedEventScript.Effect.FREEZE);
            this.numHits += 1;
            this.ResetTile(tile, renderer, meshRenderer);
        }

        // Flame tiles
        else if (collider.CompareTag("FlameTileTag"))
        {
            this.timedEventScript.AddEffect(TimedEventScript.Effect.SPEED);
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
        if (collider.CompareTag("Untagged") || collider.CompareTag("PlayerHandTag"))
        {
            AudioSource.PlayClipAtPoint(this.slapSound, this.gameObject.transform.position, 0.7f);
        }
        else if (collider.CompareTag("BlackHoleTileTag"))
        {
            AudioSource.PlayClipAtPoint(this.errorSound, this.gameObject.transform.position);
        }
        else if (collider.CompareTag("HourglassTileTag"))
        {
            AudioSource.PlayClipAtPoint(this.hourglassSound, this.gameObject.transform.position);
        }
        else if (collider.CompareTag("SnowflakeTileTag"))
        {
            AudioSource.PlayClipAtPoint(this.snowflakeSound, this.gameObject.transform.position);
        }
        else if (collider.CompareTag("FlameTileTag"))
        {
            AudioSource.PlayClipAtPoint(this.flameSound, this.gameObject.transform.position);
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

    private void SpawnPointUI(GameObject prefab, Collider collider, bool willMultiply)
    {
        Vector3 interpPos = 0.75f * this.transform.position + 0.15f * collider.transform.position + 0.10f * this.camera.transform.position;

        GameObject ui = GameObject.Instantiate(prefab, interpPos, Quaternion.identity);
        ui.transform.parent = this.canvas.transform;
        if (willMultiply)
        {
            ui.GetComponent<PointUIScript>().wasMultiplied = true;
        }
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

    public void PlayActualMusic()
    {
        this.tutorialAudio.Stop();
        this.levelAudio.Play();
    }
}

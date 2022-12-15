using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GlobalScript : MonoBehaviour
{
    // Global variables
    public float maxTimer;
    public float timer;
    public bool freezeTimer;

    public int score;
    public int level;
    public int multiplier;
    public int throws;
    
    public int numPointTiles; // Number of point tiles in world
    public int randomizeThreshold; // How many throws until the board randomizes

    private GameObject canvas;
    private BallScript ballScript;
    private TileRandomizerScript randomizer;
    private AudioSource tickAudio;
    private AudioSource failAudio;
    private AudioSource completeAudio;

    // For level transitions
    private float outroTimer;
    private float maxOutroTimer;

    public GameObject newLevelIntroPrefab;
    public float newLevelIntroTimer;
    public float maxNewLevelIntroTimer;
    private bool awaitingNewLevelIntroTimer;

    public GameObject countdownIntroPrefab;
    public float countdownIntroTimer;
    public float maxCountdownIntroTimer;
    private bool awaitingCountdownIntroTimer;

    private bool levelBegun;

    void Awake()
    {
        this.canvas = GameObject.Find("Canvas");
        this.ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
        this.randomizer = GetComponent<TileRandomizerScript>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        this.tickAudio = audioSources[0];
        this.failAudio = audioSources[1];
        this.completeAudio = audioSources[2];

        this.maxTimer = 50;
        this.freezeTimer = false;
        this.score = 0;
        this.level = 0; // We call advance level at start
        this.multiplier = 0;
        this.throws = 0;
        this.numPointTiles = 1;
        this.randomizeThreshold = 1;

        PlayerPrefs.SetInt("currScore", 0);

        // Scale the room
        // Note: this assumes the player is already standing upright when gameplay scene is loaded
        float headPosition = GameObject.Find("VRCamera").transform.position[1];
        GameObject.Find("CubeWorld").transform.localScale = headPosition / 2f * new Vector3(1f, 1f, 1f);

        this.maxOutroTimer = 4;
        this.maxNewLevelIntroTimer = 4;
        this.awaitingNewLevelIntroTimer = false;
        this.maxCountdownIntroTimer = 3;
        this.awaitingCountdownIntroTimer = false;
        this.levelBegun = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (throws == 0)
        {
            this.ballScript.RemoveThrowability();
        }

        // Moves to next level and resets any needed variables
        PlayIntro();
    }

    // Update is called once per frame
    void Update()
    {
        // Outro timer
        if (this.outroTimer > 0)
        {
            this.outroTimer -= Time.deltaTime;
        }

        // New level intro timer
        if (this.newLevelIntroTimer > 0)
        {
            this.newLevelIntroTimer -= Time.deltaTime;
        } else if (this.newLevelIntroTimer <= 0 && this.awaitingNewLevelIntroTimer)
        {
            this.awaitingNewLevelIntroTimer = false;
            this.newLevelIntroTimer = 0;
            this.DisplayCountdownIntro();
        }

        // Countdown intro timer
        if (this.countdownIntroTimer > 0)
        {
            this.countdownIntroTimer -= Time.deltaTime;
        }
        else if (this.countdownIntroTimer <= 0 && this.awaitingCountdownIntroTimer)
        {
            this.awaitingCountdownIntroTimer = false;
            this.countdownIntroTimer = 0;
            this.AdvanceLevel();
        }

        if (this.levelBegun)
        {
            // Level timer
            if (!this.freezeTimer)
            {
                timer -= Time.deltaTime;
            }
            if (this.numPointTiles <= 0)
            {
                // Level complete, give bonus points and go to next level
                this.levelBegun = false;
                this.score += 100;
                this.completeAudio.Play();
                PlayIntro();
            }
            else if (this.timer > 0.2 * this.maxTimer && this.tickAudio.isPlaying)
            {
                this.tickAudio.Stop();
            }
            else if (this.timer > 0 && this.timer <= 0.2 * this.maxTimer)
            {
                if (!tickAudio.isPlaying) this.tickAudio.Play();
            }
            else if (this.timer <= 0)
            {
                this.failAudio.Play();
                PlayIntro();
            }
        }
        
    }

    void PlayOutro()
    {
        /// Do level outro

        // Make ball temporarily disappear
        this.ballScript.gameObject.transform.position = new Vector3(1000, 1000, 1000);

        // Display "Level complete and score" for 3 seconds


        // Dim all tiles
    }

    void PlayIntro()
    {
        /// Do level intro

        // Stop ticking if it exists
        this.tickAudio.Stop();

        // Handle game logic
        if (this.level == 1)
        {
            // Speed increases a little after lvl 1
            this.ballScript.IncrSpeed(0.15f);
        }
        if (this.level == 2)
        {
            // Speed increases a little after lvl 2
            this.ballScript.IncrSpeed(0.25f);

            // Music changes after lvl 2 (last tutorial level)
            this.ballScript.PlayActualMusic();
            this.maxTimer = 90;
        }
        else if (this.level >= 3)
        {
            this.ballScript.IncrSpeed(0.08f);
        }

        // Reset nums
        this.level += 1;
        this.multiplier = 1;
        this.throws = 0;

        this.newLevelIntroTimer = this.maxNewLevelIntroTimer;
        this.awaitingNewLevelIntroTimer = true;

        // Display new level intro
        GameObject ui = GameObject.Instantiate(this.newLevelIntroPrefab);
        ui.transform.parent = this.canvas.transform;
    }

    void DisplayCountdownIntro()
    {
        /// Display countdown, randomize and undim asynchronously (countdown > undim)

        this.countdownIntroTimer = this.maxCountdownIntroTimer;
        this.awaitingCountdownIntroTimer = true;

        // Display countdown intro
        GameObject ui = GameObject.Instantiate(this.countdownIntroPrefab);
        ui.transform.parent = this.canvas.transform;

        // Randomize tiles
        this.randomizer.RandomizeTiles();
    }

    void AdvanceLevel()
    {
        // If Level 0, skip this
        // Freeze time
        // Delete ball
        // Display score for 3 seconds
        // Dim all tiles

        // After 3 seconds
        // Randomize tiles & intermediate stuff

        // Display new level
        // Undim all tiles
        // Countdown
        // Spawn ball

        // Spawn ball
        this.ballScript.ResetBall();

        // Set time
        this.timer = this.maxTimer;

        // Unfreeze time
        this.levelBegun = true;
    }

    public void IncrThrow()
    {
        this.throws += 1;
        if (this.throws == 1)
        {
            this.ballScript.AddThrowability();
        }
    }

    public void DecrThrow()
    {
        this.throws -= 1;
        if (this.throws == 0)
        {
            this.ballScript.RemoveThrowability();
        }
    }

    public void SetHighScore()
    {
        if (this.score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", this.score);
        }
    }

    public void SetCurrScore()
    {
        PlayerPrefs.SetInt("currScore", this.score);
        this.SetHighScore();
    }
}

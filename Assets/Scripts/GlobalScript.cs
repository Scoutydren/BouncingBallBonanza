using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GlobalScript : MonoBehaviour
{
    // Global variables
    public float maxTimer;
    public float timer;

    public int score;
    public int accumulatedScore;
    public int level;
    public int multiplier;
    public int throws;
    
    public int numPointTiles; // Number of point tiles in world
    public int randomizeThreshold; // How many throws until the board randomizes

    private BallScript ballScript;
    private TileRandomizerScript randomizer;
    private AudioSource tickAudio;
    private AudioSource failAudio;
    private AudioSource completeAudio;

    void Awake()
    {
        this.ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
        this.randomizer = GetComponent<TileRandomizerScript>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        this.tickAudio = audioSources[0];
        this.failAudio = audioSources[1];
        this.completeAudio = audioSources[2];

        this.maxTimer = 60;
        this.score = 0;
        this.accumulatedScore = 0;
        this.level = 0; // We call advance level at start
        this.multiplier = 0;
        this.throws = 0;
        this.numPointTiles = 1;
        this.randomizeThreshold = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (throws == 0)
        {
            this.ballScript.RemoveThrowability();
        }

        // Moves to next level and resets any needed variables
        AdvanceLevel();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (this.numPointTiles <= 0)
        {
            // Level complete, give bonus points and go to next level
            this.score += 100;
            this.completeAudio.Play();
            AdvanceLevel();
        }
        else if (this.timer > 0 && this.timer <= 0.2 * this.maxTimer)
        {
            if (!tickAudio.isPlaying) this.tickAudio.Play();
        }
        else if (this.timer <= 0)
        {
            this.failAudio.Play();
            AdvanceLevel();
        }
    }

    void AdvanceLevel()
    {
        this.tickAudio.Stop();

        if (this.level == 1)
        {
            // Speed increases a little after lvl 2
            this.ballScript.IncrSpeed(0.15f);
        }
        if (this.level == 2)
        {
            // Speed increases a little after lvl 2
            this.ballScript.IncrSpeed(0.25f);
        }
        else if (this.level == 3)
        {
            // Music changes after lvl 3 (last tutorial level)
            this.ballScript.PlayActualMusic();
        }

        this.timer = this.maxTimer;
        this.level += 1;
        this.multiplier = 1;
        this.throws = 0;

        // TODO accumulated score func from multiplier
        this.ballScript.ResetBall();
        //this.randomizer.RandomizeTiles(this.level, this.randomizeThreshold);
        this.randomizer.RandomizeTiles();
    }

    // depreicated
    /*public void FinishThrow()
    {        
        if (numThrows % randomizeThreshold == 0)
        {
            randomizer.RandomizeTiles(this.numThrows, this.randomizeThreshold);
        }
    }*/

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
}

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
    
    public int numPointTiles; // Number of point tiles in world
    public int randomizeThreshold; // How many throws until the board randomizes

    private BallScript ballScript;
    private TileRandomizerScript randomizer;

    void Awake()
    {
        this.ballScript = GameObject.Find("Ball").GetComponent<BallScript>();
        this.randomizer = GetComponent<TileRandomizerScript>();

        this.maxTimer = 60;
        this.score = 0;
        this.accumulatedScore = 0;
        this.level = 0;
        this.multiplier = 0;
        this.numPointTiles = 1;
        this.randomizeThreshold = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
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
            AdvanceLevel();
            // TODO add sound effect
        }
        else if (this.timer <= 0)
        {
            AdvanceLevel();
            // TODO add timer countdown sound effect when in range 0-10
        }
    }

    void AdvanceLevel()
    {
        this.timer = this.maxTimer;
        this.level += 1;
        this.multiplier = 1;

        // TODO accumulated score func
        this.ballScript.ResetBall();
        this.randomizer.RandomizeTiles(this.level, this.randomizeThreshold);
    }

    // depreicated
    /*public void FinishThrow()
    {        
        if (numThrows % randomizeThreshold == 0)
        {
            randomizer.RandomizeTiles(this.numThrows, this.randomizeThreshold);
        }
    }*/
}

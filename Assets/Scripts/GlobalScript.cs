using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    // Global variables
    public int score;
    public int accumulatedScore;
    public int currThrow;
    public int multiplier;
    
    public int randomizeThreshold; // How many throws until the board randomizes

    public TileRandomizerScript randomizer;

    // Start is called before the first frame update
    void Start()
    {
        this.score = 0;
        this.accumulatedScore = 0;
        this.currThrow = 1;
        this.multiplier = 1;
        this.randomizeThreshold = 1;

        this.randomizer = GetComponent<TileRandomizerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishThrow()
    {
        this.score += multiplier * accumulatedScore;
        this.accumulatedScore = 0;
        this.currThrow += 1;
        this.multiplier = 1;
        
        if (currThrow - 1 % randomizeThreshold == 0)
        {
            randomizer.RandomizeTiles(this.currThrow, this.randomizeThreshold);
        }
    }
}

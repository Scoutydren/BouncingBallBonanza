using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Controls;

public class ScoreTextScript : MonoBehaviour
{
    private GlobalScript global;
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        Math.Round(0.5d, MidpointRounding.AwayFromZero);
        this.scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        FormatScore();
    }

    private void FormatScore()
    {
        if (this.global.multiplier > 1)
        {
            this.scoreText.text = "Score: " + this.global.score.ToString() + " + " + this.global.accumulatedScore + " x " + this.global.multiplier;
        }
        else if (this.global.accumulatedScore > 0)
        {
            this.scoreText.text = "Score: " + this.global.score.ToString() + " + " + this.global.accumulatedScore;
        }
        else
        {
            this.scoreText.text = "Score: " + this.global.score.ToString();
        }
    }
}

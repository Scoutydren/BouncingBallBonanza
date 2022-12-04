using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextScript : MonoBehaviour
{
    private GlobalScript global;
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        FormatScore();
    }

    private void FormatScore()
    {
        if (global.numHits == 0)
        {
            this.scoreText.text = "Score: " + this.global.score.ToString();
        }
        else
        {
            this.scoreText.text = "Score: " + this.global.score.ToString() + " + " + this.global.numHits;
        }
    }
}

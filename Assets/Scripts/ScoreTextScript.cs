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
        this.scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        FormatScore();
    }

    private void FormatScore()
    {
        if (this.global.levelBegun)
        {
            this.scoreText.text = "Score: " + this.global.score.ToString();
        }
        else
        {
            this.scoreText.text = "";
        }

        try
        {
            // Keep in front of player
            this.transform.position = GameObject.Find("VRCamera").transform.TransformPoint(new Vector3(0.3f, -0.35f, 0.5f));
            this.transform.rotation = GameObject.Find("VRCamera").transform.rotation;
        }
        catch (Exception e) {}
        
    }
}

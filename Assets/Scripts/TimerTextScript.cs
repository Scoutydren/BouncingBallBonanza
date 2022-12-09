using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerTextScript : MonoBehaviour
{
    private GlobalScript global;
    private TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        this.timerText.text = "Timer: " + this.global.timer.ToString("0.0");
        if (this.global.timer > 0.6 * this.global.maxTimer)
        {
            this.timerText.color = Color.green;
        }
        else if (this.global.timer > 0.2 * this.global.maxTimer)
        {
            this.timerText.color = Color.yellow;
        }
        else if (Math.Round(0.2 * this.global.maxTimer - this.global.timer, MidpointRounding.AwayFromZero) % 2 == 1)
        {
            this.timerText.color = Color.red;
        }
        else
        {
            this.timerText.color = Color.yellow;
        }
    }
}

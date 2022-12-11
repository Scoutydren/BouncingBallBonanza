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
        if (this.global.freezeTimer)
        {
            this.timerText.color = new Color(0.5f, 0.94f, 1);
        }
        else
        {
            if (this.global.timer > 0.6 * this.global.maxTimer)
            {
                this.timerText.color = Color.green;
            }
            else if (this.global.timer > 0.2 * this.global.maxTimer)
            {
                this.timerText.color = new Color(1, 0.6f, 0);
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

        try
            {
                // Keep in front of player
                this.transform.position = GameObject.Find("VRCamera").transform.TransformPoint(new Vector3(0.3f, -0.57f, 0.5f));
                this.transform.rotation = GameObject.Find("VRCamera").transform.rotation;
            }
            catch (Exception e) {}
    }
}

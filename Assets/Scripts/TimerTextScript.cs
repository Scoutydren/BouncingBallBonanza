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
    }
}

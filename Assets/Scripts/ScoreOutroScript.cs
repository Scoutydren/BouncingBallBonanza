using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreOutroScript : MonoBehaviour
{
    public Vector3 pos;

    private GlobalScript global;
    private TextMeshProUGUI levelText;

    // % of duration when fadeIn/fadeOut
    private float fadeIn = 0.75f;
    private float fadeOut = 0.25f;

    private Vector4 color;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.levelText = GetComponent<TextMeshProUGUI>();

        this.pos = new Vector3(0.40f, -0.53f, 0.35f);
        color = this.levelText.color;
        this.levelText.color = new Color(color[0], color[1], color[2], 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.global.outroTimer / this.global.maxOutroTimer >= fadeIn)
        {
            float dimPercent = (this.global.maxOutroTimer - this.global.outroTimer) / ((1 - fadeIn) * this.global.maxOutroTimer);
            this.levelText.color = new Color(color[0], color[1], color[2], dimPercent);
        } else if (this.global.outroTimer / this.global.maxOutroTimer <= fadeOut && this.global.outroTimer > 0)
        {
            float dimPercent = this.global.outroTimer / (fadeOut * this.global.maxOutroTimer);
            this.levelText.color = new Color(color[0], color[1], color[2], dimPercent);
        } else if (this.global.outroTimer <= 0)
        {
            Destroy(this.gameObject);
        }

        this.levelText.text = "Score: " + this.global.score;

        try
        {
            // Keep in front of player
            this.transform.position =
                GameObject.Find("VRCamera").transform.TransformPoint(pos);
            this.transform.rotation = GameObject.Find("VRCamera").transform.rotation;
        }
        catch (Exception)
        {
        }
    }
}

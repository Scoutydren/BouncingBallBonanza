using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewLevelIntroScript : MonoBehaviour
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

        this.pos = new Vector3(0.43f, -0.47f, 0.35f);
        color = this.levelText.color;
        this.levelText.color = new Color(color[0], color[1], color[2], 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.global.newLevelIntroTimer / this.global.maxNewLevelIntroTimer >= fadeIn)
        {
            float dimPercent = (this.global.maxNewLevelIntroTimer - this.global.newLevelIntroTimer) / ((1 - fadeIn) * this.global.maxNewLevelIntroTimer);
            this.levelText.color = new Color(color[0], color[1], color[2], dimPercent);
        } else if (this.global.newLevelIntroTimer / this.global.maxNewLevelIntroTimer <= fadeOut && this.global.newLevelIntroTimer > 0)
        {
            float dimPercent = this.global.newLevelIntroTimer / (fadeOut * this.global.maxNewLevelIntroTimer);
            this.levelText.color = new Color(color[0], color[1], color[2], dimPercent);
        } else if (this.global.newLevelIntroTimer <= 0)
        {
            Destroy(this.gameObject);
        }

        this.levelText.text = "Level " + this.global.level.ToString();

        try
        {
            // Keep in front of player
            this.transform.position =
                GameObject.Find("VRCamera").transform.TransformPoint(pos);
            this.transform.rotation = GameObject.Find("VRCamera").transform.rotation;
        }
        catch (Exception)
        {
            Debug.Log("Exception in NewlevelIntroScript.cs");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTextScript : MonoBehaviour
{
    private GlobalScript global;
    private TextMeshProUGUI leveltext;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.leveltext = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        this.leveltext.text = "Level: " + this.global.level.ToString();

        try
        {
            // Keep in front of player
            this.transform.position =
                GameObject.Find("VRCamera").transform.TransformPoint(new Vector3(0.3f, -0.3f, 0.5f));
            this.transform.rotation = GameObject.Find("VRCamera").transform.rotation;
        }
        catch (Exception e)
        {
        }
    }
}

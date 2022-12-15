using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownIntroScript : MonoBehaviour
{
    public Vector3 pos;

    private GlobalScript global;
    private TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.levelText = GetComponent<TextMeshProUGUI>();

        this.pos = new Vector3(0.53f, -0.47f, 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.global.countdownIntroTimer <= 0)
        {
            Destroy(this.gameObject);
        }

        this.levelText.text = Math.Ceiling(this.global.countdownIntroTimer).ToString();

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

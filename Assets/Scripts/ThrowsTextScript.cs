using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowsTextScript : MonoBehaviour
{
    private GlobalScript global;
    private TextMeshProUGUI throwsText;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.throwsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        this.throwsText.text = "Throws: " + this.global.throws;

        try
        {
            // Keep in front of player
            this.transform.position =
                GameObject.Find("VRCamera").transform.TransformPoint(new Vector3(0.3f, -0.4f, 0.5f));
            this.transform.rotation = GameObject.Find("VRCamera").transform.rotation;
        }
        catch (Exception e)
        {
            
        }
    }
}

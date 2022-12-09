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
    }
}

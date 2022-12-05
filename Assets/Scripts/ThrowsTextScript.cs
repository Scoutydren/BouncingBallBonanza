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
        this.throwsText.text = "Throws: " + this.global.numThrows.ToString();
    }
}

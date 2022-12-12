using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CurrScoreTextScript : MonoBehaviour
{
    private TextMeshProUGUI currScoreText;

    // Start is called before the first frame update
    void Start()
    {
        this.currScoreText = GetComponent<TextMeshProUGUI>();
        this.currScoreText.text = "Curr: " + PlayerPrefs.GetInt("currScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTextScript : MonoBehaviour
{
    private TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        this.highScoreText = GetComponent<TextMeshProUGUI>();
        this.highScoreText.text = "Best: " + PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {}
}

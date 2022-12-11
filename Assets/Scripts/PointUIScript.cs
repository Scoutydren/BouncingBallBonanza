using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointUIScript : MonoBehaviour
{
    private GlobalScript global;
    private TextMeshProUGUI text;
    private GameObject camera;
    private float duration;
    private float timer;

    public bool wasMultiplied;
    public string defaultText;
    
    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
        this.text = GetComponent<TextMeshProUGUI>();
        this.camera = GameObject.Find("VRCamera");
        this.duration = 3;
        this.timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.wasMultiplied)
        {
            this.text.text = defaultText + " x 2";
        }
        else
        {
            this.text.text = defaultText;
        }
        this.transform.rotation = this.camera.transform.rotation;
        if (timer > duration)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}

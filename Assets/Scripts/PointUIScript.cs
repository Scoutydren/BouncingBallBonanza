using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUIScript : MonoBehaviour
{
    private GameObject camera;
    private float duration;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        this.camera = GameObject.Find("VRCamera");
        this.duration = 3;
        this.timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

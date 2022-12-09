using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUIScript : MonoBehaviour
{
    private float duration;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        this.duration = 3;
        this.timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

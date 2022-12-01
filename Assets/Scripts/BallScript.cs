using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    GlobalScript global;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<GlobalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("10PtTileTag")) {
            global.score += 10;
        } else if (collider.CompareTag("20PtTileTag")) {
            global.score += 20;
        } else if (collider.CompareTag("30PtTileTag")) {
            global.score += 30;
        }
    }
}

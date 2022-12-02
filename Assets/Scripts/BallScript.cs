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
        if (collider.CompareTag("PlayerTag")) {
            // Set speed to 0
            Debug.Log("Collide with player");
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        } else if (collider.CompareTag("BackWallTag"))
        {
            // Reset ball
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            this.transform.position = GameObject.Find("RightHand").transform.position + new Vector3(0, 0, .3f); // Does not spawn at correct position
        }
        else if (collider.CompareTag("10PtTileTag"))
        {
            global.score += 10;
        } else if (collider.CompareTag("20PtTileTag"))
        {
            global.score += 20;
        } else if (collider.CompareTag("30PtTileTag"))
        {
            global.score += 30;
        }
    }
}

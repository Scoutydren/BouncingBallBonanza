using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: not srue how to keep vr player in same z position
        // Set player position to be at back wall
        this.transform.position = new Vector3(
            this.transform.position[0],
            this.transform.position[1],
            -2f
        );

        Debug.Log(this.transform.position);
    }
}

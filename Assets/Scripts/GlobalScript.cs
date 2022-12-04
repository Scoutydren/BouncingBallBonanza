using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    // Global variables
    public int score;
    public int numHits;
    public int currThrow;

    // Start is called before the first frame update
    void Start()
    {
        this.score = 0;
        this.numHits = 0;
        this.currThrow = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NegateTracking : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 ballPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.startPosition = new Vector3(0f, 1.2f, -4.8f);
        this.ballPosition = new Vector3(0.6f, 1.2f, -4.8f);
    }

    // Update is called once per frame
    void Update()
    {
        // Only allow movement on the x axis
        Vector3 currPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);
        transform.position = new Vector3(
            currPosition[0],
            -currPosition[1],
            -currPosition[2]
        ) + startPosition;

        //transform.position = ballPosition - currPosition;
    }
}

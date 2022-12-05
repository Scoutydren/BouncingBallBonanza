using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NegateTracking : MonoBehaviour
{
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.startPosition = new Vector3(-0.07f, 0.5f, -2.3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = -InputTracking.GetLocalPosition(XRNode.CenterEye) + startPosition;
        //transform.position = new Vector3(0, 0, 0);
        //transform.rotation = Quaternion.Inverse(InputTracking.GetLocalRotation(XRNode.CenterEye));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBallScript : MonoBehaviour
{
    private int threshold;

    // Start is called before the first frame update
    void Start()
    {
        this.threshold = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(this.transform.position[0]) > threshold || Math.Abs(this.transform.position[1]) > threshold || Math.Abs(this.transform.position[2]) > threshold)
        {
            // Reset position
            this.transform.position = new Vector3(0, 1f, 0.5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        Debug.Log(collider.tag);
        if (collider.CompareTag("NewGameButtonTag"))
        {
            GameObject.Destroy(this);
            SceneManager.LoadScene("GameplayScene");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        Debug.Log(collider.tag);
        if (collider.CompareTag("NewGameButtonTag"))
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
}

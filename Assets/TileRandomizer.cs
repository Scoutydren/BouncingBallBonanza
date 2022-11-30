using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] walls = new string[2] { "frontWall", "bottomWall" };

        foreach (string wallStr in walls)
        {
            GameObject wall = GameObject.Find(wallStr);
            int numTiles = wall.transform.childCount;

            for (int i = 0; i < numTiles; i++)
            {
                Transform tileTransform = wall.transform.GetChild(i);
                GameObject tile = tileTransform.gameObject;
                Renderer renderer = tile.GetComponent<Renderer>();

                int colorNum = Random.Range(0, 2);
                Color color = new Color(250, 255, 255);
                if (colorNum == 0)
                {
                    colorNum = Random.Range(0, 3);
                    if (colorNum == 0) color = Color.red;
                    if (colorNum == 1) color = Color.green;
                    if (colorNum == 2) color = Color.blue;
                }
                renderer.material.color = color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] walls = new string[5] { "FrontWall", "LeftWall", "RightWall", "TopWall", "BottomWall" };

        foreach (string wallStr in walls)
        {
            GameObject wall = GameObject.Find(wallStr);
            int numTiles = wall.transform.childCount;

            for (int i = 0; i < numTiles; i++)
            {
                Transform tileTransform = wall.transform.GetChild(i);
                GameObject tile = tileTransform.gameObject;
                Renderer renderer = tile.GetComponent<Renderer>();

                int tileType = Random.Range(0, 6);
                Color color = new Color(1, 1, 1);
                if (tileType < 3)
                {
                    int colorNum = Random.Range(0, 3);
                    if (colorNum == 0)
                    {
                        color = Color.red;
                        tile.tag = "10PtTileTag";
                    }
                    if (colorNum == 1)
                    {
                        color = Color.green;
                        tile.tag = "20PtTileTag";
                    }
                    if (colorNum == 2)
                    {
                        color = Color.blue;
                        tile.tag = "30PtTileTag";
                    }
                }
                else if (tileType == 3)
                {
                    tileType = Random.Range(0, 4);
                    if (tileType == 0)
                    {
                        tile.tag = "LeftForceTileTag";
                        tile.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Force");
                        Debug.Log("pikachu");
                    }
                    else if (tileType == 1)
                    {
                        tile.tag = "RightForceTileTag";
                    }
                    else if (tileType == 2)
                    {
                        tile.tag = "UpForceTileTag";
                    }
                    else
                    {
                        tile.tag = "DownForceTileTag";
                    }
                    color = new Color(.2f, .3f, .4f);
                }
                else
                {
                    tile.tag = "EmptyTileTag";
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

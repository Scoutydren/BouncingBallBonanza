using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RandomizeTiles(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeTiles(int currThrow, int randomizeThreshold)
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

                int tileType = Random.Range(0, 7);
                Color color = new Color(1, 1, 1);
                if (tileType < 3)
                {
                    int colorNum = Random.Range(0, 3);
                    if (colorNum == 0)
                    {
                        // color = Color.red;
                        tile.tag = "10PtTileTag";
                        tile.GetComponent<MeshRenderer>().material = Resources.Load<Material>("10Points");
                    }
                    if (colorNum == 1)
                    {
                        // color = Color.green;
                        tile.tag = "20PtTileTag";
                        tile.GetComponent<MeshRenderer>().material = Resources.Load<Material>("20Points");
                    }
                    if (colorNum == 2)
                    {
                        // color = Color.blue;
                        tile.tag = "30PtTileTag";
                        tile.GetComponent<MeshRenderer>().material = Resources.Load<Material>("30Points");
                    }
                }
                else if (tileType == 3)
                {
                    tileType = Random.Range(0, 4);
                    if (tileType == 0)
                    {
                        tile.tag = "LeftForceTileTag";
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
                else if (tileType == 4)
                {
                    tile.tag = "2xMultiplier";
                    tile.GetComponent<MeshRenderer>().material = Resources.Load<Material>("2xMultiplier");
                }
                else
                {
                    tile.tag = "EmptyTileTag";
                    tile.GetComponent<MeshRenderer>().material = null;
                }
                renderer.material.color = color;
            }
        }
    }
}

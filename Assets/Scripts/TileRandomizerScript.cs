using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RandomizeTiles(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeTiles(int numThrows, int randomizeThreshold)
    {
        string[] walls = new string[6] { "FrontWall", "BackWall", "LeftWall", "RightWall", "TopWall", "BottomWall" };

        foreach (string wallStr in walls)
        {
            GameObject wall = GameObject.Find(wallStr);
            int numTiles = wall.transform.childCount;

            for (int i = 0; i < numTiles; i++)
            {
                Transform tileTransform = wall.transform.GetChild(i);
                GameObject tile = tileTransform.gameObject;
                Renderer renderer = tile.GetComponent<Renderer>();
                MeshRenderer meshRenderer = tile.GetComponent<MeshRenderer>();

                int tileType = Random.Range(0, 100);
                Color color = Color.white;
                if (tileType < 35)
                {
                    int colorNum = Random.Range(0, 3);
                    if (colorNum == 0)
                    {
                        // color = Color.red;
                        tile.tag = "10PtTileTag";
                        meshRenderer.material = Resources.Load<Material>("10Points");
                    }
                    if (colorNum == 1)
                    {
                        // color = Color.green;
                        tile.tag = "20PtTileTag";
                        meshRenderer.material = Resources.Load<Material>("20Points");
                    }
                    if (colorNum == 2)
                    {
                        // color = Color.blue;
                        tile.tag = "30PtTileTag";
                        meshRenderer.material = Resources.Load<Material>("30Points");
                    }
                }
                else if (tileType < 45)
                {
                    tileType = Random.Range(0, 4);
                    if (tileType == 0)
                    {
                        tile.tag = "LeftForceTileTag";
                        meshRenderer.material = Resources.Load<Material>("LeftForce");
                    }
                    else if (tileType == 1)
                    {
                        tile.tag = "RightForceTileTag";
                        meshRenderer.material = Resources.Load<Material>("RightForce");
                    }
                    else if (tileType == 2)
                    {
                        tile.tag = "UpForceTileTag";
                        meshRenderer.material = Resources.Load<Material>("UpForce");
                    }
                    else
                    {
                        tile.tag = "DownForceTileTag";
                        meshRenderer.material = Resources.Load<Material>("DownForce");
                    }
                }
                else if (tileType < 50)
                {
                    tile.tag = "2xMultiplier";
                    meshRenderer.material = Resources.Load<Material>("2xMultiplier");
                }
                else if (tileType < 60 + 2 * numThrows) // Make more difficult by increasing portion of black holes
                {
                    tile.tag = "BlackHoleTileTag";
                    color = Color.black;
                }
                else
                {
                    tile.tag = "EmptyTileTag";
                    meshRenderer.material = null;
                }
                renderer.material.color = color;
            }
        }
    }
}

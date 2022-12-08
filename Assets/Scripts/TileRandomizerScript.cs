using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RandomizeTiles(4, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeTiles(int difficulty, int randomizeThreshold)
    {
        string[] walls = new string[6] { "FrontWall", "BackWall", "LeftWall", "RightWall", "TopWall", "BottomWall" };

        // First level should only have empty and point tiles
        float pointPercentage = 0.3f;
        float forcePercentage = 0f;
        float multiplierPercentage = 0f;
        float deathPercentage = 0f;

        if (difficulty == 1)
        {
            // Second level introduces force tiles
            forcePercentage = 0.05f;
        }
        else if (difficulty == 2)
        {
            // Third level introduces multiplier tiles
            forcePercentage = 0.05f;
            multiplierPercentage = 0.03f;
        }
        else if (difficulty == 3)
        {
            // Fourth level introduces death tiles
            forcePercentage = 0.05f;
            multiplierPercentage = 0.03f;
            deathPercentage = 0.02f;
        }
        else if (difficulty > 3)
        {
            forcePercentage = 0.05f + (difficulty - 2) * 0.01f;
            multiplierPercentage = 0.03f;
            deathPercentage = 0.02f + (difficulty - 2) * 0.02f;
        }

        Debug.Log(pointPercentage);
        Debug.Log(forcePercentage);
        Debug.Log(multiplierPercentage);
        Debug.Log(deathPercentage);


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

                float rand = Random.Range(0f, 1f);
                Color color = Color.white;
                if (rand < pointPercentage)
                {
                    rand = Random.Range(0f, 1f);
                    if (rand < 1f / 3f)
                    {
                        tile.tag = "10PtTileTag";
                        meshRenderer.material = Resources.Load<Material>("10Points");
                    }
                    else if (rand < 2f / 3f)
                    {
                        tile.tag = "20PtTileTag";
                        meshRenderer.material = Resources.Load<Material>("20Points");
                    }
                    else
                    {
                        tile.tag = "30PtTileTag";
                        meshRenderer.material = Resources.Load<Material>("30Points");
                    }
                }
                else if (rand < pointPercentage + forcePercentage)
                {
                    rand = Random.Range(0f, 1f);
                    if (rand < 0.25f)
                    {
                        tile.tag = "LeftForceTileTag";
                        meshRenderer.material = Resources.Load<Material>("LeftForce");
                    }
                    else if (rand < 0.5f)
                    {
                        tile.tag = "RightForceTileTag";
                        meshRenderer.material = Resources.Load<Material>("RightForce");
                    }
                    else if (rand < 0.75f)
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
                else if (rand < pointPercentage + forcePercentage + multiplierPercentage)
                {
                    tile.tag = "2xMultiplier";
                    meshRenderer.material = Resources.Load<Material>("2xMultiplier");
                }
                else if (rand < pointPercentage + forcePercentage + multiplierPercentage + deathPercentage)
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

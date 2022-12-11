using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizerScript : MonoBehaviour
{
    // Shuffle array
    public GlobalScript global;

    // Start is called before the first frame update
    void Start()
    {
        this.global = GameObject.Find("Global").GetComponent<GlobalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Randomizes list of tiles per face
    private List<int> RandomizeNums(int num)
    {
        // Initialize array
        List<int> randomizedList = new List<int>();
        for (int i = 0; i < num; i++)
        {
            randomizedList.Add(i);
        }

        randomizedList = randomizedList.OrderBy(x => Random.value).ToList();
        return randomizedList;
    }

    private List<int> RandomizeNums(int num, int keep)
    {
        // Initialize array
        List<int> randomizedList = new List<int>();
        for (int i = 0; i < num; i++)
        {
            randomizedList.Add(i);
        }

        randomizedList = randomizedList.OrderBy(x => Random.value).ToList();
        randomizedList.RemoveRange(0, num - keep);
        return randomizedList;
    }

    public void RandomizeTiles()
    {
        if (this.global.level == 1)
        {
            RandomizeLevel1();
        }
        else if (this.global.level == 2)
        {
            RandomizeLevel2();
        }
        else if (this.global.level == 3)
        {
            RandomizeLevel3();
        }
        else
        {
            RandomizeTiles(this.global.level, 1);
        }
    }

    public void RandomizeLevel1()
    {
        // Pick 1 wall and generate 1 tile
        string[] walls = new string[6] { "FrontWall", "BackWall", "LeftWall", "RightWall", "TopWall", "BottomWall" };
        int wallNum = Random.Range(0, 6);

        foreach (string wallStr in walls)
        {
            GameObject wall = GameObject.Find(wallStr);

            int numTiles = wall.transform.childCount;

            List<int> randomizedList = RandomizeNums(numTiles);

            for (int i = 0; i < randomizedList.Count(); i++)
            {
                Transform tileTransform = wall.transform.GetChild(randomizedList[i]);
                GameObject tile = tileTransform.gameObject;
                Renderer renderer = tile.GetComponent<Renderer>();
                MeshRenderer meshRenderer = tile.GetComponent<MeshRenderer>();

                Color color = Color.white;
                
                if (wallStr == walls[wallNum] && i == 0)
                {
                    tile.tag = "10PtTileTag";
                    meshRenderer.material = Resources.Load<Material>("10Points");
                }
                else
                {
                    tile.tag = "EmptyTileTag";
                    meshRenderer.material = null;
                }
                renderer.material.color = color;
            }

            this.global.numPointTiles = 1;
        }
    }

    public void RandomizeLevel2()
    {
        // Pick 2 walls and generate 1 tile
        string[] walls = new string[6] { "FrontWall", "BackWall", "LeftWall", "RightWall", "TopWall", "BottomWall" };
        List<int> randomizedWalls = RandomizeNums(6, 2);

        for (int j = 0; j < walls.Length; j++)
        {
            GameObject wall = GameObject.Find(walls[j]);

            int numTiles = wall.transform.childCount;

            List<int> randomizedList = RandomizeNums(numTiles);

            for (int i = 0; i < randomizedList.Count(); i++)
            {
                Transform tileTransform = wall.transform.GetChild(randomizedList[i]);
                GameObject tile = tileTransform.gameObject;
                Renderer renderer = tile.GetComponent<Renderer>();
                MeshRenderer meshRenderer = tile.GetComponent<MeshRenderer>();

                Color color = Color.white;
                if (j == randomizedWalls[0] && (i == 0))
                {
                    tile.tag = "FlameTileTag";
                    meshRenderer.material = Resources.Load<Material>("flame");
                }
                else if (j == randomizedWalls[1] && (i == 0))
                {
                    tile.tag = "SnowflakeTileTag";
                    meshRenderer.material = Resources.Load<Material>("snowflake");
                }
                else
                {
                    tile.tag = "EmptyTileTag";
                    meshRenderer.material = null;
                }
                renderer.material.color = color;
            }

            this.global.numPointTiles = 3;
        }
    }

    public void RandomizeLevel3()
    {
        // Pick 3 walls and 3 tiles are 10 points, pick 1 wall, 1 is 20 points
        string[] walls = new string[6] { "FrontWall", "BackWall", "LeftWall", "RightWall", "TopWall", "BottomWall" };
        HashSet<int> randomizedWalls = new HashSet<int>(RandomizeNums(6, 3));
        int random20 = Random.Range(0, 6);

        for (int j = 0; j < walls.Length; j++)
        {
            GameObject wall = GameObject.Find(walls[j]);

            int numTiles = wall.transform.childCount;

            List<int> randomizedList = RandomizeNums(numTiles);

            for (int i = 0; i < randomizedList.Count(); i++)
            {
                Transform tileTransform = wall.transform.GetChild(randomizedList[i]);
                GameObject tile = tileTransform.gameObject;
                Renderer renderer = tile.GetComponent<Renderer>();
                MeshRenderer meshRenderer = tile.GetComponent<MeshRenderer>();

                Color color = Color.white;

                if (randomizedWalls.Contains(j) && i == 0)
                {
                    tile.tag = "10PtTileTag";
                    meshRenderer.material = Resources.Load<Material>("10Points");
                }
                else if (j == random20 && i == numTiles - 1)
                {
                    tile.tag = "20PtTileTag";
                    meshRenderer.material = Resources.Load<Material>("20Points");
                }
                else
                {
                    tile.tag = "EmptyTileTag";
                    meshRenderer.material = null;
                }
                renderer.material.color = color;
            }

            this.global.numPointTiles = 4;
        }
    }

    // Will be depricated soon
    public void RandomizeTiles(int difficulty, int randomizeThreshold)
    {
        string[] walls = new string[6] { "FrontWall", "BackWall", "LeftWall", "RightWall", "TopWall", "BottomWall" };

        // First level should only have empty and point tiles
        float pointPercentage = 0.05f;
        float forcePercentage = 0f;
        float multiplierPercentage = 0f;
        float deathPercentage = 0f;
        float throwPercentage = 0.05f;

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

        int numPointTiles = 0;

        foreach (string wallStr in walls)
        {
            GameObject wall = GameObject.Find(wallStr);
            int numTiles = wall.transform.childCount;

            List<int> randomizedList = RandomizeNums(numTiles);

            for (int i = 0; i < numTiles; i++)
            {
                Transform tileTransform = wall.transform.GetChild(i);
                GameObject tile = tileTransform.gameObject;
                Renderer renderer = tile.GetComponent<Renderer>();
                MeshRenderer meshRenderer = tile.GetComponent<MeshRenderer>();

                Color color = Color.white;
                if (randomizedList[i] < pointPercentage * numTiles)
                {
                    // Count number of point tiles
                    numPointTiles += 1;

                    float rand = Random.Range(0f, 1f);
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
                else if (randomizedList[i] < (pointPercentage + forcePercentage) * numTiles)
                {
                    float rand = Random.Range(0f, 1f);
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
                else if (randomizedList[i] < (pointPercentage + forcePercentage + multiplierPercentage) * numTiles)
                {
                    tile.tag = "2xMultiplier";
                    meshRenderer.material = Resources.Load<Material>("2xMultiplier");
                }
                else if (randomizedList[i] < (pointPercentage + forcePercentage + multiplierPercentage + deathPercentage) * numTiles)
                {
                    tile.tag = "BlackHoleTileTag";
                    color = Color.black;
                }
                else if (randomizedList[i] < (pointPercentage + forcePercentage + multiplierPercentage + deathPercentage + throwPercentage) * numTiles)
                {
                    tile.tag = "ThrowTileTag";
                    meshRenderer.material = Resources.Load<Material>("ThrowTile");
                }
                else
                {
                    tile.tag = "EmptyTileTag";
                    color = new Color(0, 32, 32);
                    meshRenderer.material = null;
                }
                renderer.material.color = color;
            }
        }

        this.global.numPointTiles = numPointTiles;
    }
}

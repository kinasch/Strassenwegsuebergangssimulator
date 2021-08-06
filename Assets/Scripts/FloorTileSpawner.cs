using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorTileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject floorTilePrefab;
    [SerializeField] private GameObject[] parents;
    [SerializeField] private Transform mainCam;
    [SerializeField] private int seed;

    private List<float> streetRows = new List<float>();
    private Dictionary<float, GameObject[]> tiles = new Dictionary<float, GameObject[]>();
    private int rows = 0;
    private bool startUpDone = false;
    
    void Start()
    {
        rows = 0;
        // Set the seed before anything else.
        Random.InitState(seed);
        // First, spawn all the tiles and hide it with the loading screen.
        SpawnTiles();
        // Second, "remove" the loading screen.
        loadingScreen.gameObject.SetActive(false);
        startUpDone = true;
    }

    private void FixedUpdate()
    {
        if (rows < (Math.Round(mainCam.position.x) + 20f) && startUpDone)
        {
            SpawnRandomTiles(rows-8.5f);
        }
        
        /*TileGarbageCollector(mainCam.position.x-12);*/
    }

    private void SpawnTiles()
    {
        // Test generation to fill the field for the 2D camera.
        for (float i = -8.5f; i <= 20-0.5f; i++)
        {
            if (i < -6)
            {
                SpawnRandomTiles(i, 0.2f);
            }
            else
            {
                SpawnRandomTiles(i);
            }
        }
    }

    private void SpawnRandomTiles(float xRow, float randomizer = 10f)
    {
        List<GameObject> newObjectsList = new List<GameObject>();
        randomizer = randomizer > 1 ? Random.value : randomizer;
        for (float j = -5.5f; j <= 5.5f; j++)
        {
            if (randomizer < 0.7f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[0].transform);
                newObjectsList.Add(newObj);
            }
            else if (randomizer > 0.7f && randomizer < 0.9f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[1].transform);
                newObj.GetComponent<SpriteRenderer>().color = Color.black;
                if (j > 5)
                {
                    streetRows.Add(xRow);
                }
                newObjectsList.Add(newObj);
            }
            else if (randomizer > 0.9f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[2].transform);
                newObj.GetComponent<SpriteRenderer>().color = Color.red;
                newObjectsList.Add(newObj);
            }
        }
        tiles.Add(xRow, newObjectsList.ToArray());
        rows++;
    }

    private void TileGarbageCollector(float xPos)
    {
        var list = tiles[xPos];
        foreach (var gO in list)
        {
            Destroy(gO);
        }

        tiles.Remove(xPos);
    }
}

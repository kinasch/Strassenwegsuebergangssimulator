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
    private int rows = 0;
    
    void Start()
    {
        rows = 0;
        // Set the seed before anything else.
        Random.InitState(seed);
        // First, spawn all the tiles and hide it with the loading screen.
        SpawnTiles();
        // Second, "remove" the loading screen.
        loadingScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (false/*rows < mainCam.position.x + 12.1f*/)
        {
            SpawnRandomTiles(mainCam.position.x+12.6f);
        }
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
        randomizer = randomizer > 1 ? Random.value : randomizer;
        for (float j = -5.5f; j <= 5.5f; j++)
        {
            if (randomizer < 0.7f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[0].transform);
            }
            else if (randomizer > 0.7f && randomizer < 0.9f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[1].transform);
                newObj.GetComponent<SpriteRenderer>().color = Color.black;
                if(j > 5) streetRows.Add(xRow);
            }
            else if (randomizer > 0.9f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[2].transform);
                newObj.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        rows++;
    }
}

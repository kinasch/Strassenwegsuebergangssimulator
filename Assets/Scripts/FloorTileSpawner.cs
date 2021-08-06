using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject floorTilePrefab;
    [SerializeField] private GameObject[] parents;
    [SerializeField] private float endX;
    [SerializeField] private int seed;
    
    void Start()
    {
        // Set the seed before anything else.
        Random.InitState(seed);
        // First, spawn all the tiles and hide it with the loading screen.
        SpawnTiles();
        // Second, "remove" the loading screen.
        loadingScreen.gameObject.SetActive(false);
    }

    private void SpawnTiles()
    {
        // Test generation to fill the field for the 2D camera.
        for (float i = -9.5f; i <= 20-0.5f; i++)
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
        for (float j = -4.5f; j <= 4.5f; j++)
        {
            if (randomizer < 0.7f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[0].transform);
            }
            else if (randomizer > 0.7f && randomizer < 0.9f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[1].transform);
                newObj.GetComponent<SpriteRenderer>().color = Color.black;
            }
            else if (randomizer > 0.9f)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(xRow, j, 0), new Quaternion(), parents[2].transform);
                newObj.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    public float GetEndX()
    {
        return endX;
    }
}

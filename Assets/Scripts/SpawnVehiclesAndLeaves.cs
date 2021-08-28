using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVehiclesAndLeaves : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject vehiclePrefab, vehicleParent, leafPrefab, leafParent;

    private List<float> rowsWithVehicles = new List<float>(), rowsWithLeaves = new List<float>();
    private FloorTileSpawner floorTileSpawner;
    private GameManaging gameManaging;
    private Dictionary<float, GameObject[]> stuff = new Dictionary<float, GameObject[]>();

    private void Start()
    {
        floorTileSpawner = gameManager.GetComponent<FloorTileSpawner>();
        gameManaging = gameManager.GetComponent<GameManaging>();
    }

    private void FixedUpdate()
    {
        foreach (var streetRow in floorTileSpawner.streetRows)
        {
            if (!rowsWithVehicles.Contains(streetRow))
            {
                StartCoroutine(Spawn(streetRow, UnityEngine.Random.Range(0.05f, 0.12f), UnityEngine.Random.Range(1, 4), true));
                rowsWithVehicles.Add(streetRow);
            }
        }
        foreach (var leavesRow in floorTileSpawner.waterRows)
        {
            if (!rowsWithLeaves.Contains(leavesRow))
            {
                StartCoroutine(Spawn(leavesRow, UnityEngine.Random.Range(0.01f, 0.03f), UnityEngine.Random.Range(1, 4), false));
                rowsWithLeaves.Add(leavesRow);
            }
        }
        
        GarbageCollector(Mathf.Round(floorTileSpawner.mainCam.position.x)-12.5f);
    }
    
    IEnumerator Spawn(float xRow, float speed, int amount, bool vehicle)
    {
        GameObject tempGameObject = null;
        List<GameObject> spawnedStuff = new List<GameObject>();

        int sign = UnityEngine.Random.value < 0.5f ? -1 : 1;
        int rotation = sign == 1 ? 180 : 0;
        // float waitTime = 0;
        
        for (int i = 0; i < amount; i++)
        {
            // Create distance between the different vehicles or leaves
            // fixed distance
            if (tempGameObject != null)
            {
                // Should the tempGameObject contain any GameObject,
                // then check the objects absolute y position until it reaches below a set value.
                // After that is reached, leave the while and create new objects.
                while(Mathf.Abs(tempGameObject.transform.position.y) > 4.2f)
                {
                    yield return null;
                }
            }
            
            GameObject obj = null;
            if (vehicle)
            {
                obj = Instantiate(vehiclePrefab, new Vector3(xRow, sign * 5.5f, -0.09f), Quaternion.Euler(0, 0, rotation), vehicleParent.transform);
            }
            else
            {
                obj = Instantiate(leafPrefab, new Vector3(xRow, sign * 5.5f, -0.09f), Quaternion.Euler(0, 0, rotation), leafParent.transform);
            }
            
            // Speed and the GameManager are things that deviate from the original prefab, thus need updates.
            obj.GetComponent<EntityBehaviour>().speed = speed;
            obj.GetComponent<EntityBehaviour>().gameManager = gameManaging;
            spawnedStuff.Add(obj);

            // Save the object for the creation of distance (see beneath for loop, l.1 of scope)
            tempGameObject = obj;
        }

        stuff.Add(xRow, spawnedStuff.ToArray());

        yield return null;
    }
    
    private void GarbageCollector(float xPos)
    {
        if (stuff.ContainsKey(xPos))
        {
            var list = stuff[xPos];
            foreach (var gO in list)
            {
                Destroy(gO);
            }

            stuff.Remove(xPos);
        }
    }
}

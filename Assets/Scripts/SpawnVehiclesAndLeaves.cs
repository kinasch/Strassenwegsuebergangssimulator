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
                StartCoroutine(Spawn(leavesRow, UnityEngine.Random.Range(0.05f, 0.12f), UnityEngine.Random.Range(1, 4), false));
                rowsWithLeaves.Add(leavesRow);
            }
        }
        
        GarbageCollector(Mathf.Round(floorTileSpawner.mainCam.position.x)-12.5f);
    }
    
    IEnumerator Spawn(float xRow, float speed, int amount, bool vehicle)
    {
        List<GameObject> spawnedStuff = new List<GameObject>();
        Mathf.Clamp(amount, 1, 3);

        int sign = UnityEngine.Random.value < 0.5f ? -1 : 1;
        int rotation = sign == 1 ? 180 : 0;
        
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = null;
            if (vehicle)
            {
                obj = Instantiate(vehiclePrefab, new Vector3(xRow, sign * 5.5f, -0.09f), Quaternion.Euler(0, 0, rotation), vehicleParent.transform);
            }
            else
            {
                obj = Instantiate(leafPrefab, new Vector3(xRow, sign * 5.5f, -0.09f), Quaternion.Euler(0, 0, rotation), leafParent.transform);
            }
            
            obj.GetComponent<VehicleAndLeafBehaviour>().speed = speed;
            obj.GetComponent<VehicleAndLeafBehaviour>().gameManager = gameManaging;
            spawnedStuff.Add(obj);
            yield return new WaitForSeconds(0.3f-speed);
        }
        
        stuff.Add(xRow, spawnedStuff.ToArray());
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

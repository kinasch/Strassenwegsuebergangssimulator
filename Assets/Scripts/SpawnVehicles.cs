using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVehicles : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject vehiclePrefab, vehicleParent;

    private List<float> rowsWithVehicles = new List<float>();
    private FloorTileSpawner floorTileSpawner;
    private GameManaging gameManaging;
    private Dictionary<float, GameObject[]> vehicles = new Dictionary<float, GameObject[]>();

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
                StartCoroutine(Spawn(streetRow, UnityEngine.Random.Range(0.05f, 0.12f), UnityEngine.Random.Range(1, 4)));
                rowsWithVehicles.Add(streetRow);
            }
        }
        
        VehicleGarbageCollector(Mathf.Round(floorTileSpawner.mainCam.position.x)-12.5f);
    }
    
    IEnumerator Spawn(float xRow, float speed, int amount)
    {
        List<GameObject> spawnedVehicles = new List<GameObject>();
        Mathf.Clamp(amount, 1, 3);

        int sign = UnityEngine.Random.value < 0.5f ? -1 : 1;
        int rotation = sign == 1 ? 180 : 0;
        
        for (int i = 0; i < amount; i++)
        {
            var obj = Instantiate(vehiclePrefab, new Vector3(xRow, sign*5.5f, -0.1f), Quaternion.Euler(0, 0, rotation), vehicleParent.transform);
            obj.GetComponent<VehicleBehaviour>().speed = speed;
            obj.GetComponent<VehicleBehaviour>().gameManager = gameManaging;
            spawnedVehicles.Add(obj);
            yield return new WaitForSeconds(0.3f-speed);
        }
        
        vehicles.Add(xRow, spawnedVehicles.ToArray());
    }
    
    private void VehicleGarbageCollector(float xPos)
    {
        if (vehicles.ContainsKey(xPos))
        {
            var list = vehicles[xPos];
            foreach (var gO in list)
            {
                Destroy(gO);
            }

            vehicles.Remove(xPos);
        }
    }
}

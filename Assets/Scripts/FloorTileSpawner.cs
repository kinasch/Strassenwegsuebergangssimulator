using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileSpawner : MonoBehaviour
{
    [SerializeField] private Canvas can;
    [SerializeField] private GameObject floorTilePrefab;
    [SerializeField] private GameObject grassTilesParent;
    
    void Start()
    {
        SpawnTiles();
        can.gameObject.SetActive(false);
    }

    private void SpawnTiles()
    {
        for (float i = -9.5f; i <= 9.5f; i++)
        {
            for (float j = -4.5f; j <= 4.5f; j++)
            {
                var newObj = Instantiate(floorTilePrefab, new Vector3(i, j, 0), new Quaternion() ,grassTilesParent.transform);
                if (i > -2 && i < 2)
                {
                    newObj.GetComponent<SpriteRenderer>().color = Color.black;
                }
            }
        }
    }
}

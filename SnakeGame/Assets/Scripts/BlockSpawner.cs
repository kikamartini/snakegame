using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Blocks")]
    [Tooltip ("The amount of each block type in the list defines how much of each will spawn")] 
    [SerializeField] List<GameObject> blocksType;

    [Header("Spawn Interval")]
    [SerializeField] float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 10f;
    float spawnTime; 
    float controlSpawnTime;
    Vector3 spawnPosition;

    int currentBlockIndex;

    [Header("Spawn Area")]
    [SerializeField] float xSafeZone;
    [SerializeField] float ySafeZone;
    float yTopFixedSafeZone = 6.5f;
    Vector2 minScreen;
    Vector2 maxScreen;
    float minXScreen;
    float maxXScreen;
    float minYScreen;
    float maxYScreen;

    //cached reference
    Camera mainCamera;





    // Start is called before the first frame update
    void Start()
    {
        CacheReferences();
        SetScreenBoundaries();
        SetVariableSpawnTime();
    }

    private void CacheReferences()
    {
        mainCamera = FindObjectOfType<Camera>();
  
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBlock();
    }

    private void SpawnBlock()
    {
        if (blocksType.Count == 0) { return; }

        currentBlockIndex = Random.Range(0, blocksType.Count);

        if (controlSpawnTime <= 0)
        {
            SetVariableSpawnPosition();
            Instantiate(blocksType[currentBlockIndex], spawnPosition, Quaternion.identity);
            SetVariableSpawnTime();
        }
        else
        {
            controlSpawnTime -= Time.deltaTime;
        }
    }

    private void SetVariableSpawnPosition()
    {
        float xPos = Random.Range(minXScreen, maxXScreen);
        float yPos = Random.Range(minYScreen, maxYScreen);
        spawnPosition = new Vector3(xPos, yPos, 0);
    }

    private void SetVariableSpawnTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        controlSpawnTime = spawnTime;
    }

    private void SetScreenBoundaries()
    {
        minScreen = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));
        maxScreen = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));

        minXScreen = minScreen.x + xSafeZone;
        maxXScreen = maxScreen.x - xSafeZone;

        minYScreen = minScreen.y + ySafeZone;
        maxYScreen = maxScreen.y - yTopFixedSafeZone;
    }
}

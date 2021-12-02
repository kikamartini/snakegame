using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Head")]
    [SerializeField] EnemyController enemyPrefab;

    [Header("Spawn Interval After Death")]
    [SerializeField] float timeToSpawn = 1f;
    
    [Header("Spawn Area")]
    [SerializeField] float xSafeZone = 2f;
    [SerializeField] float ySafeZone = 2f;
    float yTopFixedSafeZone = 6.5f;

    Vector2 minScreen;
    Vector2 maxScreen;
    float minXScreen;
    float maxXScreen;
    float minYScreen;
    float maxYScreen;
    Vector3 spawnPosition;

    //cached reference
    Camera mainCamera;
    PlayerMovement player;
    bool isPlayerAlive;



    // Start is called before the first frame update
    void Start()
    {
        CacheReferences();
        SetScreenBoundaries();
 
    }

    private void CacheReferences()
    {
        mainCamera = FindObjectOfType<Camera>();
        player = FindObjectOfType<PlayerMovement>();

        if (player = null)
        {
            isPlayerAlive = false;
        }
        else
        {
            isPlayerAlive = true;
        }

    }


    public void EnemyKilled()
    {
        StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        yield return new WaitForSeconds(timeToSpawn);
        SetVariableSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }



    private void SetVariableSpawnPosition()
    {
        float xPos = Random.Range(minXScreen, maxXScreen);
        float yPos = Random.Range(minYScreen, maxYScreen);
        spawnPosition = new Vector3(xPos, yPos, 0);
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


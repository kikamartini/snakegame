using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BodyController : MonoBehaviour
{

    [Header("Snake Body")]
    [SerializeField] GameObject bodyPrefab;
    [SerializeField] int initialBodySize = 5;
    [Tooltip("Debug")] [SerializeField] List<GameObject> bodys;
   
    //cached reference
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRd;
    PlayerMovement myPlayerMov;
    LevelController levelController;

    //hierarchy organization and single collider
    GameObject bodysParent;
    CompositeCollider2D parentCompositeCollider;


    // Start is called before the first frame update
    void Start()
    {
        SetBodysParentAndSingleCollider();
        CacheReferences();
 
        bodys.Add(gameObject); //set the head as first object in bodys

        SpawnInitialBody();

    }

    private void SpawnInitialBody()
    {
        for (int i = 0; i < initialBodySize; i++)
        {
            SpawnBody();
        }
    }

    private void CacheReferences()
    {
        myRd = GetComponent<Rigidbody2D>();
        myPlayerMov = GetComponent<PlayerMovement>();
        levelController = FindObjectOfType<LevelController>();

    }

    private void SetBodysParentAndSingleCollider()
    {
        if (bodysParent == null)
        {
            bodysParent = new GameObject("Bodys"); ;
            bodysParent.transform.position = new Vector2(0, 0);

            //set single collider
            bodysParent.AddComponent<Rigidbody2D>();
            bodysParent.GetComponent<Rigidbody2D>().gravityScale = 0;
            bodysParent.AddComponent<CompositeCollider2D>();
            parentCompositeCollider = bodysParent.GetComponent<CompositeCollider2D>();
            parentCompositeCollider.isTrigger = true;
        }
    }

 
    private void FixedUpdate()
    {

        UpdateBodysPosition();
        myPlayerMov.Move();
        myPlayerMov.CheckSnakeHeadPos();
        parentCompositeCollider.GenerateGeometry();

    }

    private void UpdateBodysPosition()
    {
        for (int i = (bodys.Count - 1); i > 0; i--)
        {
            bodys[i].transform.position = bodys[i - 1].transform.position;
        }
    }

    public void SpawnBody()
    {
        GameObject previousBody = bodys[bodys.Count - 1];
        GameObject spawnedBody = Instantiate(bodyPrefab, previousBody.transform.position, Quaternion.identity);
        spawnedBody.transform.parent = bodysParent.transform;
        spawnedBody.tag = "Body";
        bodys.Add(spawnedBody);
    }


    public void DestroySnake()
    {
        Invoke("Destroy", 0.2f);
    }

    private void Destroy()
    {    
        Destroy(bodysParent);
        Destroy(gameObject);
        levelController.HandleGameOver();
    }
}

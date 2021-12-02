using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Snake Movement")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float minTimeChangeDirection = 2f;
    [SerializeField] float maxTimeChangeDirection = 4f;
    float timeToChangeDirection;
    Vector2[] directions = new Vector2[4];
    Vector2 direction;
    float[] angles = new float[4];
    float angle;
    int directionIndex;
    bool moveAISnake;

    [Header("Enemy Snake Body")]
    [SerializeField] GameObject bodyPrefab;
    [SerializeField] int initialBodySize = 5;
    [Tooltip ("Debug")] [SerializeField] List<GameObject> bodys;

  
    //hierarchy organization and single collider
    GameObject bodysEnemyParent;
    CompositeCollider2D parentCompositeCollider;


    //screen boundaries
    Vector2 minScreen;
    Vector2 maxScreen;
    float minXScreen;
    float maxXScreen;
    float minYScreen;
    float maxYScreen;
    float paddleX;
    float paddleY;
    float flipPosX;
    float flipPosY;


    //cached references
    Rigidbody2D myRd;
    Camera mainCamera;
    SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

        SetBodysParentAndSingleCollider();
        CacheReferences();
        SetScreenBoundaries();
        InitializeDirection();

        bodys.Add(gameObject); //set the head as first object in bodys
        SpawnInitialBody();

        moveAISnake = true;
        StartCoroutine(ChangeDirection());


    }

    private void FixedUpdate()
    {
        UpdateBodysPosition();
        Move();
        CheckSnakeHeadPos();
    }

    private void SetBodysParentAndSingleCollider()
    {
        if (bodysEnemyParent == null)
        {
            bodysEnemyParent = new GameObject("EnemyBodys"); ;
            bodysEnemyParent.transform.position = new Vector2(0, 0);

            //set single collider
            bodysEnemyParent.AddComponent<Rigidbody2D>();
            bodysEnemyParent.GetComponent<Rigidbody2D>().gravityScale = 0;
            bodysEnemyParent.AddComponent<CompositeCollider2D>();
            parentCompositeCollider = bodysEnemyParent.GetComponent<CompositeCollider2D>();
            parentCompositeCollider.isTrigger = true;
        }
    }

    private void SpawnInitialBody()
    {
        for (int i = 0; i < initialBodySize; i++)
        {
            SpawnBody();
        }
    }

    //sets the only possibles directions the snake can move
    private void InitializeDirection()
    {
        directions[0] = Vector2.down;
        angles[0] = -90;

        directions[1] = Vector2.up;
        angles[1] = 90;

        directions[2] = Vector2.left;
        angles[2] = 180;

        directions[3] = Vector2.right;
        angles[3] = 0;
    }

    IEnumerator ChangeDirection()
    {
        while (moveAISnake)
        {
            int previousDirectionIndex = directionIndex;
            timeToChangeDirection = Random.Range(minTimeChangeDirection, maxTimeChangeDirection);
            yield return new WaitForSeconds(timeToChangeDirection);

            if (previousDirectionIndex == 0 || previousDirectionIndex == 1)
            {
                directionIndex = Random.Range(2, 3 + 1);
            }
            else if (previousDirectionIndex == 2 || previousDirectionIndex == 3)
            {
                directionIndex = Random.Range(0, 1 + 1);
            }

            direction = directions[directionIndex];
            angle = angles[directionIndex];
        }
    }

    private void CacheReferences()
    {
        myRd = GetComponent<Rigidbody2D>();
        mainCamera = FindObjectOfType<Camera>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    


    public void Move()
    {
        var xDeltaPos = direction.x * moveSpeed * Time.deltaTime;
        var yDeltaPos = direction.y * moveSpeed * Time.deltaTime;

        var xPos = transform.position.x + xDeltaPos;
        var yPos = transform.position.y + yDeltaPos;

        gameObject.transform.position = new Vector2(xPos, yPos);
        myRd.rotation = angle;
    }




    private void SetScreenBoundaries()
    {
        minScreen = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));
        maxScreen = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));

        minXScreen = minScreen.x;
        maxXScreen = maxScreen.x;

        minYScreen = minScreen.y;
        maxYScreen = maxScreen.y;

        //tamanho do sprite
        paddleX = mySpriteRenderer.sprite.bounds.size.x / 2;
        paddleY = mySpriteRenderer.sprite.bounds.size.y / 2;

        //posição (absoluta) da cobra nas bordas da tela, quando passa de um lado para o outro
        flipPosX = Mathf.Abs(maxXScreen) - paddleX;
        flipPosY = Mathf.Abs(maxYScreen) - paddleY;

    }

    public void CheckSnakeHeadPos()
    {
        if ((myRd.position.x >= maxXScreen) || (myRd.position.x <= minXScreen))
        {
            float posX = Mathf.Sign(myRd.position.x) * flipPosX;
            transform.position = new Vector2(-posX, myRd.position.y);
        }

        if ((myRd.position.y >= maxYScreen) || (myRd.position.y <= minYScreen))
        {
            float posY = Mathf.Sign(myRd.position.y) * flipPosY;
            transform.position = new Vector2(myRd.position.x, -posY);
        }
    }

    public void IncreaseMoveSpeed(float extraSpeed)
    {
        moveSpeed += extraSpeed;
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
        spawnedBody.tag = "Enemy";
        spawnedBody.GetComponent<SpriteRenderer>().color = mySpriteRenderer.color;
        spawnedBody.transform.parent = bodysEnemyParent.transform;
        bodys.Add(spawnedBody);
    }


    public void DestroySnake()
    {
        Destroy(bodysEnemyParent);
        Destroy(gameObject);
    }

}

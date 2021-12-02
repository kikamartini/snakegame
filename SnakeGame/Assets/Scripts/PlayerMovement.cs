using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    [Header("Snake Movement")]
    [SerializeField] float moveSpeed = 2f;
    [Tooltip("Apenas em linha reta e valor 1")] [SerializeField] Vector2 initialDirection = Vector2.right;
    Vector2 direction;
    float angle;


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
        CacheReferences();
        SetScreenBoundaries();
        direction = initialDirection;
    }

    private void CacheReferences()
    {
        myRd = GetComponent<Rigidbody2D>();
        mainCamera = FindObjectOfType<Camera>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (direction != Vector2.down)
            {
                direction = Vector2.up;
                angle = 90;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direction != Vector2.up)
            {
                direction = Vector2.down;
                angle = -90;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction != Vector2.right)
            {
                direction = Vector2.left;
                angle = 180;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction != Vector2.left)
            {
                direction = Vector2.right;
                angle = 0;
            }
        }
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
        flipPosX = Mathf.Abs(maxXScreen)  - paddleX;
        flipPosY = Mathf.Abs(maxYScreen) - paddleY;

    }

    //"flips" the snake when it reaches the borders of the camera
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


}

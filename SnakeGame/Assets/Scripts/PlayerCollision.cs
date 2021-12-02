using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    int pickedBlocksBattering;

    BlockEngine collidingBlockEngine;
    BlockBattering collidingBlockBattering;
    EnemyController enemy;

    //cached reference
    BodyController myBodyController;
    PlayerMovement myMovement;
    LevelController levelController;
    EnemySpawner enemySpawner;

    void Start()
    {
        myBodyController = GetComponent<BodyController>();
        myMovement = GetComponent<PlayerMovement>();
        levelController = FindObjectOfType<LevelController>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        pickedBlocksBattering = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BlockEngine")
        {
            collidingBlockEngine = other.GetComponent<BlockEngine>();
            myMovement.IncreaseMoveSpeed(collidingBlockEngine.GetSpeedIncrement());
            myBodyController.SpawnBody();
            levelController.AddToScore(collidingBlockEngine.GetPoints());
            Destroy(other.gameObject);
        }

        else if (other.tag == "BlockBattering")
        {
            collidingBlockBattering = other.GetComponent<BlockBattering>();
            pickedBlocksBattering++;
            levelController.AddToScore(collidingBlockBattering.GetPoints());
            levelController.AddBatteringBlock();

            Destroy(other.gameObject);
        }

        else 
        {
            if (pickedBlocksBattering == 0)
            {
                myBodyController.DestroySnake();
            }

            else
            {
                if (other.tag == "Enemy")
                {
                    enemy = other.GetComponent<EnemyController>();
                    enemy.DestroySnake();
                    enemySpawner.EnemyKilled();
                }

                pickedBlocksBattering--;
                levelController.RemoveBatteringBlock();

            }
        }
    }



}

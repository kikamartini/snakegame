using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlockPicker : MonoBehaviour
{
    BlockEngine collidingBlockEngine;

    //cached reference
    EnemyController myController;

    void Start()
    {
        myController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BlockEngine")
        {
            collidingBlockEngine = other.GetComponent<BlockEngine>();
            myController.IncreaseMoveSpeed(collidingBlockEngine.GetSpeedIncrement());
            myController.SpawnBody();
         
            Destroy(other.gameObject);
        }

        if (other.tag == "BlockBattering")
        {
            //the enemy has no buffs from battering block, just takes it from the player
            Destroy(other.gameObject);
        }

    }




}

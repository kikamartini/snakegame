using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEngine : MonoBehaviour
{

    [SerializeField] float speedIncrease = 0.5f;
    [SerializeField] float points = 100;


    public float GetSpeedIncrement()
    {
        return speedIncrease;
    }

    public float GetPoints()
    {
        return points;
    }
    
}

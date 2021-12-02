using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI batteringBlockDisplay;

    
    float playerScore;
    float playerBatteringBlocks;

    //cached reference
    LevelController levelController;
    
    public void AddToScore(float score)
    {
        playerScore += score;
    }

    public void AddBatteringBlock()
    {
        playerBatteringBlocks++;
    }

    public void RemoveBatteringBlock()
    {
        playerBatteringBlocks--;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        playerScore = 0;
        playerBatteringBlocks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "SCORE: " + playerScore.ToString("0000000");
        batteringBlockDisplay.text = "x " + playerBatteringBlocks.ToString("000");

    }
}

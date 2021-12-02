using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{

    [Header("Game Over Screen")]
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TextMeshProUGUI finalScoreText;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenuCanvas;

    [Header("Gameplay UI")]
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI batteringBlockDisplay;


    float playerScore;
    float playerBatteringBlocks;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        playerScore = 0;
        playerBatteringBlocks = 0;
    }

    private void Update()
    {
        scoreDisplay.text = "SCORE: " + playerScore.ToString("0000000");
        batteringBlockDisplay.text = "x " + playerBatteringBlocks.ToString("000");

        HandlePause();

    }

    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1;
    }


    public void HandleGameOver()
    {
        gameOverCanvas.SetActive(true);
        finalScoreText.text = "SCORE: " + playerScore.ToString("000000");
        Time.timeScale = 0;
    }

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

}

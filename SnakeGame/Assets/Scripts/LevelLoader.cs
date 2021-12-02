using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    int currentSceneIndex;



    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
  
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }


    public void QuitGame()
    {
        Application.Quit();
    }


   

}

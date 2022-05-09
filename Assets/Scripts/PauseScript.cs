using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    private void Start()
    {
        GameIsPaused = false;
        Resume();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Was Pushed");
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }

    }
    public void Resume()
    {
        Debug.Log("Resume Was Pushed");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() {
        Debug.Log("Quit Was Pushed");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
   
}
// Pause Script made from help of https://www.youtube.com/watch?v=JivuXdrIHK0 
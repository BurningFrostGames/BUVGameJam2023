using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenu;

    public GameObject objects_ToDeactivate;
    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                StopGame();
            }
        }
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    private void StopGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
        GamePaused = true;
        objects_ToDeactivate.SetActive(false);
        Debug.Log("Ojects deactivated");
    }
    
}

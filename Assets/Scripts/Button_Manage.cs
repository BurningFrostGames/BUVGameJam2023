using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Manage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start_Game()
    {
        //SceneManager.LoadScene(sceneBuildIndex: +1);
        Debug.Log("Load Game");
        SceneManager.LoadScene("Dn test map");
    }

    public void options_Menu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Options");
    }

    public void quit_Game()
    {
        Application.Quit();
        Debug.Log("quit gane");
    }
}

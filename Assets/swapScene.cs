using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class swapScene : MonoBehaviour
{
    
    public void swapToHub()
    {
        SceneManager.LoadScene("Hub");
    }

    public void swapToMenu()
    {
        SceneManager.LoadScene("main menu");
    }

    public void quitGame()
    {
        Application.Quit(); 
    }
}

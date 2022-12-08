using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public void playGame()
    {
        SceneManager.LoadScene("Castle Level");
    }

    public void miniGame()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void controls()
    {
        SceneManager.LoadScene("Controls Scene");
    }

    public void menu()
    {
        Debug.Log("menu");
        SceneManager.LoadScene("Start Scene");

    }

    public void leaderboards()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void quit()
    {
        Application.Quit();
    }
   
    // Start is called before the first frame update

    
}   

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsObject;
    [SerializeField] private GameObject menuObject;

    public void StartButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f; // Set time scale back to 1 to resume the game
    }

    public void Credits()
    {
        creditsObject.SetActive(true);
        menuObject.SetActive(false);
    }
    public void MainMenu()
    {
        creditsObject.SetActive(false);
        menuObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

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

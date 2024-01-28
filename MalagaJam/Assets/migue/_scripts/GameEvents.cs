using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



// Event system for the game. Any object can subscribe to an action By calling GameEvents.instance.$action_name += $method_to_call
// To add a new event: create a new action and a new trigger for that action (for example: OnTimeRunOut and TimeRunOout())
public class GameEvents : MonoBehaviour
{
    // Singleton
    public static GameEvents instance;
    private void Awake() { instance = this; }


    // Define actions
    public event Action OnTimeRunOut;

    public void TimeRunOut()
    {
        if (OnTimeRunOut != null) OnTimeRunOut();
        SceneManager.LoadScene(0);
    }
}

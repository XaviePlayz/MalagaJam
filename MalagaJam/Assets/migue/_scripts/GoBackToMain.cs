using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoBackToMainMenu());
    }

    // Rotate a gameobject eulerAngles degrees in a certain amount of time (duration). If notice = true, then trigger the TimeRunOut event
    IEnumerator GoBackToMainMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Use: Create an object with the following structure:
// Clock
// ----HourRotation
// --------Hour
// ----MinuteRotation
// --------Minute
// Assign this script to the Clock object and change the total time for the clock, after which it runs out
// When the time runs out, it triggers and event and everything subscribed to the event is also called
public class ClockManager : MonoBehaviour
{

    private GameObject hourHandle;
    private GameObject minuteHandle;
    public float totalTimeSeconds = 30f;
    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    // Rotate a gameobject eulerAngles degrees in a certain amount of time (duration). If notice = true, then trigger the TimeRunOut event
    IEnumerator rotateObject(GameObject gameObjectToMove, Vector3 eulerAngles, float duration, bool notice)
    {
        Vector3 newRot = gameObjectToMove.transform.eulerAngles + eulerAngles;

        Vector3 currentRot = gameObjectToMove.transform.eulerAngles;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.eulerAngles = Vector3.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        if (notice)
        {
            Debug.Log("Call the time finsihed event");
            GameEvents.instance.TimeRunOut();
        }

    }

    void Start() 
    {
        hourHandle = getChildGameObject(this.gameObject, "HourRotation");
        minuteHandle = getChildGameObject(this.gameObject, "MinuteRotation");

        StartCoroutine(rotateObject(hourHandle, new Vector3(0, 0, -360), totalTimeSeconds, true));
        StartCoroutine(rotateObject(minuteHandle, new Vector3(0, 0, -4320), totalTimeSeconds, false));

    }

}

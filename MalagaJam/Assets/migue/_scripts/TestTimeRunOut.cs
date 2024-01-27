using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Test script to test the triggering for the TimeRunOut event. Destroys the object after a certain amount of time
public class TestTimeRunOut : MonoBehaviour
{
    
    void OnDisable() {
        GameEvents.instance.OnTimeRunOut -= deleteThis;

    }

    private void Start() {
        GameEvents.instance.OnTimeRunOut += deleteThis;
    }

    private void deleteThis()
    {
        Destroy(this.gameObject);
    }
}

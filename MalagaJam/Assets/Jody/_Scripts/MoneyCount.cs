using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    private int MoneyAmount;
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("10")) 
        { 
            MoneyAmount+=10;
            Debug.Log("+10, total " + MoneyAmount); 
        }         
        
        if (other.gameObject.CompareTag("20")) 
        { 
            MoneyAmount+=20;
            Debug.Log("+20, total " + MoneyAmount); 
        } 
    }
}

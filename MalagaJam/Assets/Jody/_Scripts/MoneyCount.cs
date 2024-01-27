using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    private int TotalMoney;
    
    List<GameObject> Coins25 = new List<GameObject>();
    List<GameObject> Coins100 = new List<GameObject>();
    List<GameObject> Coins500 = new List<GameObject>();
    List<GameObject> Bills1000 = new List<GameObject>();

    void Start()
    {
        CountMoney();
    }

    private void CountMoney()
    {
        foreach(GameObject Coin25 in GameObject.FindGameObjectsWithTag("25")) 
        {
			Coins25.Add(Coin25);
		}

        foreach(GameObject Coin100 in GameObject.FindGameObjectsWithTag("100")) 
        {
			Coins100.Add(Coin100);
		}

        foreach(GameObject Coin500 in GameObject.FindGameObjectsWithTag("500")) 
        {
			Coins500.Add(Coin500);
		}

        foreach(GameObject Bill1000 in GameObject.FindGameObjectsWithTag("1000")) 
        {
			Bills1000.Add(Bill1000);
		}
        TotalMoney = (Coins25.Count * 25) + (Coins100.Count * 100) + (Coins500.Count * 500) + (Bills1000.Count * 1000);
        Debug.Log(TotalMoney);
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("25")) 
        { 
            TotalMoney+=25;
            Debug.Log("+25, total " + TotalMoney); 
        }         
        
        else if (other.gameObject.CompareTag("100")) 
        { 
            TotalMoney+=100;
            Debug.Log("+100, total " + TotalMoney); 
        }          
        
        else if (other.gameObject.CompareTag("500")) 
        { 
            TotalMoney+=500;
            Debug.Log("+500, total " + TotalMoney); 
        }          
        
        else if (other.gameObject.CompareTag("1000")) 
        { 
            TotalMoney+=1000;
            Debug.Log("+1000, total " + TotalMoney); 
        } 
    }
}

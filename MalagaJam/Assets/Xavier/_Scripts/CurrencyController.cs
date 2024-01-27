using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    private int TotalMoney;
    
    List<GameObject> Coins25 = new List<GameObject>();
    public Sprite placed25;
    public Sprite unplaced25;
    List<GameObject> Coins100 = new List<GameObject>();
    public Sprite placed100;
    public Sprite unplaced100;
    List<GameObject> Coins500 = new List<GameObject>();
    public Sprite placed500;
    public Sprite unplaced500;
    List<GameObject> Bills1000 = new List<GameObject>();
    public Sprite placed1000;
    public Sprite unplaced1000;

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
            other.GetComponent<SpriteRenderer>().sprite = placed25;
        }         
        
        else if (other.gameObject.CompareTag("100")) 
        { 
            TotalMoney+=100;
            Debug.Log("+100, total " + TotalMoney); 
            other.GetComponent<SpriteRenderer>().sprite = placed100;
        }          
        
        else if (other.gameObject.CompareTag("500")) 
        { 
            TotalMoney+=500;
            Debug.Log("+500, total " + TotalMoney); 
            other.GetComponent<SpriteRenderer>().sprite = placed500;
        }          
        
        else if (other.gameObject.CompareTag("1000")) 
        { 
            TotalMoney+=1000;
            Debug.Log("+1000, total " + TotalMoney); 
            other.GetComponent<SpriteRenderer>().sprite = placed1000;
        } 
    }
    
    public void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("25")) 
        { 
            other.GetComponent<SpriteRenderer>().sprite = unplaced25;
        }         
        
        else if (other.gameObject.CompareTag("100")) 
        { 
            other.GetComponent<SpriteRenderer>().sprite = unplaced100;
        }          
        
        else if (other.gameObject.CompareTag("500")) 
        { 
            other.GetComponent<SpriteRenderer>().sprite = unplaced500;
        }          
        
        else if (other.gameObject.CompareTag("1000")) 
        { 
            other.GetComponent<SpriteRenderer>().sprite = unplaced1000;
        } 
    }
}

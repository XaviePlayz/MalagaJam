using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController_Dutch : MonoBehaviour
{

    #region Singleton

    private static CurrencyController_Dutch _instance;
    public static CurrencyController_Dutch Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CurrencyController_Dutch>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(CurrencyController_Dutch).Name;
                    _instance = obj.AddComponent<CurrencyController_Dutch>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public float TotalMoney;
    
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
    public int totalAdultTickets;
    public int totalChildrentickets;

    public List<GameObject> coinsInCounter = new List<GameObject>();


    void Start()
    {
        CountMoney();
    }

    private void CountMoney()
    {
        foreach(GameObject Coin25 in GameObject.FindGameObjectsWithTag("€0.50")) 
        {
			Coins25.Add(Coin25);
		}

        foreach(GameObject Coin100 in GameObject.FindGameObjectsWithTag("€1.00")) 
        {
			Coins100.Add(Coin100);
		}

        foreach(GameObject Coin500 in GameObject.FindGameObjectsWithTag("€5.00")) 
        {
			Coins500.Add(Coin500);
		}

        foreach(GameObject Bill1000 in GameObject.FindGameObjectsWithTag("€10.00")) 
        {
			Bills1000.Add(Bill1000);
		}
        TotalMoney = (Coins25.Count * 0.50f) + (Coins100.Count * 1) + (Coins500.Count * 5) + (Bills1000.Count * 10);

        // substract the total amount of money that is in the change box at start:
        TotalMoney -= 93;
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("€0.50")) 
        { 
            TotalMoney+=0.50f;
            Debug.Log("+€0.50, total " + TotalMoney); 
            other.GetComponent<SpriteRenderer>().sprite = placed25;
            coinsInCounter.Add(other.gameObject);
        }         
        
        else if (other.gameObject.CompareTag("€1.00")) 
        { 
            TotalMoney+=1;
            Debug.Log("+€1.00, total " + TotalMoney); 
            other.GetComponent<SpriteRenderer>().sprite = placed100;
            coinsInCounter.Add(other.gameObject);
        }

        else if (other.gameObject.CompareTag("€5.00")) 
        { 
            TotalMoney+=5;
            Debug.Log("+€5.00, total " + TotalMoney); 
            other.GetComponent<SpriteRenderer>().sprite = placed500;
            coinsInCounter.Add(other.gameObject);
        }

        else if (other.gameObject.CompareTag("€10.00")) 
        { 
            TotalMoney+=10;
            Debug.Log("+€10.00, total " + TotalMoney); 
            other.GetComponent<SpriteRenderer>().sprite = placed1000;
            coinsInCounter.Add(other.gameObject);
        }

        if (other.gameObject.CompareTag("PlacedAdultTickets"))
        {
            totalAdultTickets +=1;
        }
        else if (other.gameObject.CompareTag("PlacedChildrenTickets"))
        {
            totalChildrentickets +=1;
        }
    }
    
    public void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("€0.50")) 
        {
            TotalMoney -= 0.50f;
            Debug.Log("-€0.50, total " + TotalMoney);
            other.GetComponent<SpriteRenderer>().sprite = unplaced25;
            coinsInCounter.Remove(other.gameObject);
        }

        else if (other.gameObject.CompareTag("€1.00")) 
        {
            TotalMoney -= 1;
            Debug.Log("-€1.00, total " + TotalMoney);
            other.GetComponent<SpriteRenderer>().sprite = unplaced100;
            coinsInCounter.Remove(other.gameObject);
        }

        else if (other.gameObject.CompareTag("€5.00")) 
        {
            TotalMoney -= 5;
            Debug.Log("-€5.00, total " + TotalMoney);
            other.GetComponent<SpriteRenderer>().sprite = unplaced500;
            coinsInCounter.Remove(other.gameObject);
        }

        else if (other.gameObject.CompareTag("€10.00")) 
        {
            TotalMoney -= 10;
            Debug.Log("-€10.00, total " + TotalMoney);
            other.GetComponent<SpriteRenderer>().sprite = unplaced1000;
            coinsInCounter.Remove(other.gameObject);
        }
        if (other.gameObject.CompareTag("AdultTickets"))
        {
            totalAdultTickets -= 1;
        }
        else if (other.gameObject.CompareTag("ChildrenTickets"))
        {
            totalChildrentickets -= 1;
        }
    }
}

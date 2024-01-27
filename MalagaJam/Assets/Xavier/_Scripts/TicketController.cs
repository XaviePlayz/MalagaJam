using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketController : MonoBehaviour
{

    #region Singleton

    private static TicketController _instance;
    public static TicketController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TicketController>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(TicketController).Name;
                    _instance = obj.AddComponent<TicketController>();
                }
            }
            return _instance;
        }
    }

    #endregion

    [Header("NPC")]
    private CharacterController characterController;
    private CurrencyController currencyController;

    public GameObject Coin25;
    public GameObject Coin100;
    public GameObject Coin500;
    public GameObject Bill1000;

    // the range of X
    [Header("X Spawn Range")]
    public float xMin;
    public float xMax;

    // the range of y
    [Header("Y Spawn Range")]
    public float yMin;
    public float yMax;

    private int pricePerAdult = 250;
    private int pricePerChild = 150;

    [SerializeField] private int totalTicketPrice;
    [SerializeField] private int receivedMoney;
    void Start()
    {
        characterController = FindObjectOfType<CharacterController>();
        currencyController = FindObjectOfType<CurrencyController>();
    }

    void Update()
    {
        if (characterController.currentNPC != null)
        {
            totalTicketPrice = (pricePerAdult * characterController.amountOfAdults) + (pricePerChild * characterController.amountOfChildren);
        }
    }  

    public IEnumerator ReceiveMoney()
    {
        List<GameObject> givenMoneyList = new List<GameObject>();
        yield return new WaitForSeconds(2);
        while (receivedMoney < totalTicketPrice)
        {
            int randomCoin25Amount = Random.Range(0, 4);
            Debug.Log(randomCoin25Amount);
            int randomCoin100Amount = Random.Range(0, 3);
            Debug.Log(randomCoin100Amount);
            int randomCoin500Amount = Random.Range(0, 2);
            Debug.Log(randomCoin500Amount);
            int randomBill1000Amount = Random.Range(0, 1);
            Debug.Log(randomBill1000Amount);

            for (int i = 0; i < randomCoin25Amount; i++)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                Instantiate(Coin25, pos, Quaternion.identity);
                receivedMoney += 25;
            }
            for (int i = 0; i < randomCoin100Amount; i++)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                Instantiate(Coin100, pos, Quaternion.identity);
                receivedMoney += 100;
            }
            for (int i = 0; i < randomCoin500Amount; i++)
            {
                if (totalTicketPrice > 500)
                {
                    Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                    Instantiate(Coin500, pos, Quaternion.identity);
                    receivedMoney += 500;
                }
                else
                {
                    randomCoin500Amount = 0;
                }
            }
            for (int i = 0; i < randomBill1000Amount; i++)
            {
                if (totalTicketPrice > 1000)
                {
                    Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                    Instantiate(Bill1000, pos, Quaternion.identity);
                    receivedMoney += 1000;
                }
                else
                {
                    randomBill1000Amount = 0;
                }
            }
        }
    }
}

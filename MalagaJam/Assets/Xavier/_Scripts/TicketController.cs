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

    public void ReceiveMoney()
    {
        while (receivedMoney <= totalTicketPrice)
        {
            int randomCoin25Amount = Random.Range(0, 2);
            int randomCoin100Amount = Random.Range(0, 4);
            int randomCoin500Amount = Random.Range(0, 10);
            int randomBill1000Amount = Random.Range(0, 5);



            for (int i = 0; i < randomCoin25Amount; i++)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
                Instantiate(Coin25, pos, Quaternion.identity);
            }
            for (int i = 0; i < randomCoin100Amount; i++)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
                Instantiate(Coin100, pos, Quaternion.identity);
            }
            for (int i = 0; i < randomCoin500Amount; i++)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
                Instantiate(Coin500, pos, Quaternion.identity);
            }
            for (int i = 0; i < randomBill1000Amount; i++)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
                Instantiate(Bill1000, pos, Quaternion.identity);
            }
            receivedMoney = (randomCoin25Amount * 25) + (randomCoin100Amount * 100) + (randomCoin500Amount * 500) + (randomBill1000Amount * 1000);
        }
    }
}

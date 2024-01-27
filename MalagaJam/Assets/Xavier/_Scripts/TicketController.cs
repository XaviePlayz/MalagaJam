using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinGiveWrapper
{
    public int coinAmount25;
    public int coinAmount100;
    public int coinAmount500;
    public int coinAmount1000;

    public CoinGiveWrapper(int coinAmount25, int coinAmount100, int coinAmount500, int coinAmount1000)
    {
        this.coinAmount25 = coinAmount25;
        this.coinAmount100 = coinAmount1000;
        this.coinAmount500 = coinAmount500;
        this.coinAmount1000 = coinAmount1000;
    }
}

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

    // Parallax layer for the coins
    [Header("Parllax Layer Transform")]
    public Transform pLayer;


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


    public CoinGiveWrapper CalculateCoinNew(int amount)
    {
        int coinAmount25 = 0;
        int coinAmount100= 0;
        int coinAmount500= 0;
        int coinAmount1000= 0;

        if (amount > 1000) {
            coinAmount1000 = amount % 1000;
            Debug.Log("1000 coins");
            Debug.Log(coinAmount1000);
            amount -= coinAmount1000 * 1000;
        }

        if (amount > 500)
        {
            coinAmount500 = amount % 500;
            Debug.Log("500 coins");
            Debug.Log(coinAmount500);
            amount -= coinAmount500 * 500;
        }

        if (amount > 100)
        {
            coinAmount100 = amount % 100;
            amount -= coinAmount100 * 100;
        }
        if (amount > 25)
        {
            coinAmount25 = amount % 25;
            amount -= coinAmount25 * 25;
        }
        if (amount > 0)
        {
            coinAmount25 += 1;
        }
        CoinGiveWrapper cw = new CoinGiveWrapper(coinAmount25, coinAmount100, coinAmount500, coinAmount1000);

        return cw;
    }

    public CoinGiveWrapper CalculateCoinOld(int amount)
    {
        int randomCoin25Amount = Random.Range(0, 3);
        int randomCoin100Amount = Random.Range(0, 3);
        int randomCoin500Amount = Random.Range(0, 3);
        int randomBill1000Amount = Random.Range(0, 2);

        CoinGiveWrapper cw = new CoinGiveWrapper(randomCoin25Amount, randomCoin100Amount, randomCoin500Amount, randomBill1000Amount);

        return cw;

    }

    public IEnumerator ReceiveMoney()
    {
        yield return new WaitForSeconds(2);

        while (receivedMoney < totalTicketPrice)
        {

            CoinGiveWrapper cw = CalculateCoinNew(totalTicketPrice);
            int coinAmount25 = cw.coinAmount25;
            int coinAmount100 = cw.coinAmount1000;
            int coinAmount500 = cw.coinAmount500;
            int coinAmount1000 = cw.coinAmount1000;

            for (int i = 0; i < coinAmount25; i++)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                var coin = Instantiate(Coin25, pos, Quaternion.identity);
                coin.transform.parent = pLayer;
                receivedMoney += 25;
            }
            for (int i = 0; i < coinAmount100; i++)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                var coin = Instantiate(Coin100, pos, Quaternion.identity);
                coin.transform.parent = pLayer;
                receivedMoney += 100;
            }
            for (int i = 0; i < coinAmount500; i++)
            {
                if (totalTicketPrice > 500)
                {
                    Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                    var coin = Instantiate(Coin500, pos, Quaternion.identity);
                    coin.transform.parent = pLayer;
                    receivedMoney += 500;
                }
                else
                {
                    coinAmount500 = 0;
                }
            }
            for (int i = 0; i < coinAmount1000; i++)
            {
                if (totalTicketPrice > 1000)
                {
                    Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                    var coin = Instantiate(Bill1000, pos, Quaternion.identity);
                    coin.transform.parent = pLayer;
                    receivedMoney += 1000;
                }
                else
                {
                    coinAmount1000 = 0;
                }
            }
        }
    }
}

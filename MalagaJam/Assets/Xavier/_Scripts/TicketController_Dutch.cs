using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGiveWrapper_Dutch
{
    public float coinAmount50Cents;
    public float coinAmount1Euro;
    public float coinAmount5Euro;
    public float coinAmount10Euro;

    public CoinGiveWrapper_Dutch(float coinAmount50Cents, float coinAmount1Euro, float coinAmount5Euro, float coinAmount10Euro)
    {
        this.coinAmount50Cents = coinAmount50Cents;
        this.coinAmount1Euro = coinAmount1Euro;
        this.coinAmount5Euro = coinAmount5Euro;
        this.coinAmount10Euro = coinAmount10Euro;
    }
}

public class TicketController_Dutch : MonoBehaviour
{
    private static TicketController_Dutch _instance;
    public static TicketController_Dutch Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TicketController_Dutch>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(TicketController_Dutch).Name;
                    _instance = obj.AddComponent<TicketController_Dutch>();
                }
            }
            return _instance;
        }
    }

    private CharacterController_Dutch characterController;
    private CurrencyController_Dutch currencyController;

    public GameObject Coin50Cents;
    public GameObject Coin1Euro;
    public GameObject Coin5Euro;
    public GameObject Coin10Euro;

    public float xMin;
    public float xMax;

    public Transform pLayer;

    public float yMin;
    public float yMax;

    private float pricePerAdult = 7.00f;
    private float pricePerChild = 3.50f;

    public ParallaxManager pm;

    [SerializeField] public float totalTicketPrice;
    [SerializeField] public float receivedMoney;

    void Start()
    {
        characterController = FindObjectOfType<CharacterController_Dutch>();
        currencyController = FindObjectOfType<CurrencyController_Dutch>();
    }

    void Update()
    {
        if (characterController.currentNPC != null)
        {
            totalTicketPrice = (pricePerAdult * characterController.amountOfAdults) + (pricePerChild * characterController.amountOfChildren);
        }
    }

    public CoinGiveWrapper_Dutch CalculateCoinNew(float amount)
    {
        float coinAmount50Cents = 0;
        float coinAmount1Euro = 0;
        float coinAmount5Euro = 0;
        float coinAmount10Euro = 0;

        if (amount > 10)
        {
            coinAmount10Euro = amount / 10;
            amount -= coinAmount10Euro * 10;
        }

        if (amount > 5)
        {
            coinAmount5Euro = amount / 5;
            amount -= coinAmount5Euro * 5;
        }

        if (amount > 1)
        {
            coinAmount1Euro = amount / 1;
            amount -= coinAmount1Euro * 1;
        }

        if (amount > 0.50f)
        {
            coinAmount50Cents = amount / 0.50f;
            amount -= coinAmount50Cents * 0.50f;
        }

        if (amount > 0)
        {
            coinAmount50Cents += 1;
        }

        CoinGiveWrapper_Dutch cw = new CoinGiveWrapper_Dutch(coinAmount50Cents, coinAmount1Euro, coinAmount5Euro, coinAmount10Euro);

        return cw;
    }

    public CoinGiveWrapper_Dutch CalculateCoinOld(float amount)
    {
        float randomCoin50CentsAmount = Random.Range(0, 3);
        float randomCoin1EuroAmount = Random.Range(0, 3);
        float randomCoin5EuroAmount = Random.Range(0, 3);
        float randomCoin10EuroAmount = Random.Range(0, 2);

        CoinGiveWrapper_Dutch cw = new CoinGiveWrapper_Dutch(randomCoin50CentsAmount, randomCoin1EuroAmount, randomCoin5EuroAmount, randomCoin10EuroAmount);

        return cw;
    }

    public IEnumerator ReceiveMoney()
    {
        yield return new WaitForSeconds(2);

        float rCoinCalc = Random.Range(0f, 1f);

        if (rCoinCalc < 0.15f)
        {
            CoinGiveWrapper_Dutch cw = CalculateCoinNew(totalTicketPrice);
            float coinAmount50Cents = cw.coinAmount50Cents;
            float coinAmount1Euro = cw.coinAmount1Euro;
            float coinAmount5Euro = cw.coinAmount5Euro;
            float coinAmount10Euro = cw.coinAmount10Euro;

            for (int i = 0; i < coinAmount50Cents; i++)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax) + pm.frontlayerParallax.x, Random.Range(yMin, yMax) + pm.frontlayerParallax.y, -5);
                var coin = Instantiate(Coin50Cents, pos, Quaternion.identity);
                coin.transform.parent = pLayer;
                receivedMoney += 0.50f;
            }

            for (int i = 0; i < coinAmount1Euro; i++)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax) + pm.frontlayerParallax.x, Random.Range(yMin, yMax) + pm.frontlayerParallax.y, -5);
                var coin = Instantiate(Coin1Euro, pos, Quaternion.identity);
                coin.transform.parent = pLayer;
                receivedMoney += 1.0f;
            }

            for (int i = 0; i < coinAmount5Euro; i++)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax) + pm.frontlayerParallax.x, Random.Range(yMin, yMax) + pm.frontlayerParallax.y, -5);
                var coin = Instantiate(Coin5Euro, pos, Quaternion.identity);
                coin.transform.parent = pLayer;
                receivedMoney += 5.0f;
            }

            for (int i = 0; i < coinAmount10Euro; i++)
            {
                Vector3 pos = new Vector3(Random.Range(xMin, xMax) + pm.frontlayerParallax.x, Random.Range(yMin, yMax) + pm.frontlayerParallax.y, -5);
                var coin = Instantiate(Coin10Euro, pos, Quaternion.identity);
                coin.transform.parent = pLayer;
                receivedMoney += 10.0f;
            }
        }
        else
        {
            while (receivedMoney < totalTicketPrice)
            {
                CoinGiveWrapper_Dutch cw = CalculateCoinOld(totalTicketPrice);
                float coinAmount50Cents = cw.coinAmount50Cents;
                float coinAmount1Euro = cw.coinAmount1Euro;
                float coinAmount5Euro = cw.coinAmount5Euro;
                float coinAmount10Euro = cw.coinAmount10Euro;

                for (int i = 0; i < coinAmount50Cents; i++)
                {
                    Vector3 pos = new Vector3(Random.Range(xMin, xMax) + pm.frontlayerParallax.x, Random.Range(yMin, yMax) + pm.frontlayerParallax.y, -5);
                    var coin = Instantiate(Coin50Cents, pos, Quaternion.identity);
                    coin.transform.parent = pLayer;
                    receivedMoney += 0.50f;
                }

                for (int i = 0; i < coinAmount1Euro; i++)
                {
                    Vector3 pos = new Vector3(Random.Range(xMin, xMax) + pm.frontlayerParallax.x, Random.Range(yMin, yMax) + pm.frontlayerParallax.y, -5);
                    var coin = Instantiate(Coin1Euro, pos, Quaternion.identity);
                    coin.transform.parent = pLayer;
                    receivedMoney += 1.0f;
                }

                for (int i = 0; i < coinAmount5Euro; i++)
                {
                    if (totalTicketPrice > 5)
                    {
                        Vector3 pos = new Vector3(Random.Range(xMin, xMax) + pm.frontlayerParallax.x, Random.Range(yMin, yMax) + pm.frontlayerParallax.y, -5);
                        var coin = Instantiate(Coin5Euro, pos, Quaternion.identity);
                        coin.transform.parent = pLayer;
                        receivedMoney += 5.0f;
                    }
                    else
                    {
                        coinAmount5Euro = 0;
                    }
                }

                for (int i = 0; i < coinAmount10Euro; i++)
                {
                    if (totalTicketPrice > 10)
                    {
                        Vector3 pos = new Vector3(Random.Range(xMin, xMax) + pm.frontlayerParallax.x, Random.Range(yMin, yMax) + pm.frontlayerParallax.y, -5);
                        var coin = Instantiate(Coin10Euro, pos, Quaternion.identity);
                        coin.transform.parent = pLayer;
                        receivedMoney += 10.0f;
                    }
                    else
                    {
                        coinAmount10Euro = 0;
                    }
                }
            }
        }
    }
}
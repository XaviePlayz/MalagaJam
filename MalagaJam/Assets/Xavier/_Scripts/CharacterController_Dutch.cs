using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class CharacterController_Dutch : MonoBehaviour
{

    #region Singleton

    private static CharacterController_Dutch _instance;
    public static CharacterController_Dutch Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CharacterController_Dutch>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(CharacterController_Dutch).Name;
                    _instance = obj.AddComponent<CharacterController_Dutch>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public GameObject currentNPC;
    public bool isHelping;
    public GameObject destroyPreviousNPC;
    public GameObject bell;
    public GameObject bellPart;

    public GameObject[] allAvailableNPCs;

    [Header("Animator")]
    private Animator anim;
    private string leavingAnimationTrigger = "Leaving";

    [Header("TextBubble")]
    [SerializeField] private GameObject adults;
    [SerializeField] private GameObject children;
    public int amountOfAdults;
    public int amountOfChildren;

    [Header("ParallaxLayer")]
    [SerializeField] private Transform pLayer;

    public GameObject popUp;
    public bool isTutorial;
    public bool tutorialTextCompleted;
    public GameObject[] tutorialTexts;
    private int currentTutorialText = 1;
    private int previousTutorialText = 0;
    public GameObject pauseMenu, optionsMenu;

    [Header("Clients")]
    public bool outOfTime;
    public int givenTooMuchMoneyClients;
    public int perfectClients;
    public int madClients;
    public int notGivenEnoughTicketsClients;
    public TextMeshProUGUI givenTooMuchMoneyClientsText;
    public TextMeshProUGUI perfectClientsText;
    public TextMeshProUGUI madClientsText;
    public TextMeshProUGUI notGivenEnoughTicketsClientsText;

    public int clientSatisfactionScore;
    public TextMeshProUGUI clientSatisfactionScoreText;

    void Start()
    {
        clientSatisfactionScoreText.text = clientSatisfactionScore.ToString("D6");
        givenTooMuchMoneyClients = 0;
        perfectClients = 0;
        madClients = 0;
        notGivenEnoughTicketsClients = 0;
        popUp = GameObject.Find("PopUp");

        EventTrigger evTrig = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry clickEvent = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerClick
        };
        clickEvent.callback.AddListener(CorrectedCurrency);
        evTrig.triggers.Add(clickEvent);
    }

    void Update()
    {
        if (isTutorial && Input.GetKeyDown(KeyCode.Mouse0) && !pauseMenu.activeSelf && !optionsMenu.activeSelf)
        {
            Time.timeScale = 1f; // Set time scale back to 1 to resume the game
            if (currentTutorialText < tutorialTexts.Length)
            {
                tutorialTexts[currentTutorialText].SetActive(true);
            }
            if (previousTutorialText < tutorialTexts.Length)
            {
                tutorialTexts[previousTutorialText].SetActive(false);
            }

            if (currentTutorialText == tutorialTexts.Length)
            {
                TicketController_Dutch.Instance.gameStarted = true;
            }
            previousTutorialText++;
            currentTutorialText++;
        }
        if (isTutorial && Input.GetKeyDown(KeyCode.Mouse0) && tutorialTexts[1].activeSelf)
        {
            SendNextCharacter();
        }
    }

    public void SendNextCharacter()
    {
        currentNPC = allAvailableNPCs[Random.Range(0, allAvailableNPCs.Length)];
        isHelping = true;
        var player = Instantiate(currentNPC);
        player.transform.parent = pLayer;

        destroyPreviousNPC = GameObject.FindGameObjectWithTag("NPC");
        adults = GameObject.FindGameObjectWithTag("Adults");
        children = GameObject.FindGameObjectWithTag("Children");
        anim = GameObject.FindAnyObjectByType<Animator>();

        if (isTutorial)
        {
            amountOfAdults = 2;
            amountOfChildren = 1;
        }
        else
        {
            amountOfAdults = Random.Range(1, 5);
            amountOfChildren = Random.Range(1, 5);
        }

        if (adults != null && children != null)
        {
            adults.GetComponent<TextMeshProUGUI>().text = "Volwassenen: " + amountOfAdults;
            children.GetComponent<TextMeshProUGUI>().text = "Kinderen: " + amountOfChildren;
        }

        StartCoroutine(TicketController_Dutch.Instance.ReceiveMoney());
    }

    public int CheckTicketsLogic()
    {
        var moneyOnSurface = CurrencyController_Dutch.Instance.TotalMoney;
        var ticketPrice = TicketController_Dutch.Instance.totalTicketPrice;
        var receivedMoney = TicketController_Dutch.Instance.receivedMoney;

        var moneyShouldBeLeft = receivedMoney - ticketPrice;

        var adultTickets = CurrencyController_Dutch.Instance.totalAdultTickets;
        var childrenTickets = CurrencyController_Dutch.Instance.totalChildrentickets;

        if (adultTickets == amountOfAdults && childrenTickets == amountOfChildren) { 
            if (moneyOnSurface > moneyShouldBeLeft) {
                Debug.Log($"You left �{moneyOnSurface - moneyShouldBeLeft} more than the actual ticketprices");
                givenTooMuchMoneyClients++;
                if (clientSatisfactionScore > 0)
                {
                    clientSatisfactionScore += Random.Range(3, 8);
                }
                StartCoroutine(PopUpReaction(3));
            }
            else if (moneyOnSurface == moneyShouldBeLeft)
            {
                Debug.Log("Perfect");
                perfectClients++;

                clientSatisfactionScore += Random.Range(121, 164);
                StartCoroutine(PopUpReaction(2));

            }
            else if (moneyOnSurface < moneyShouldBeLeft)
            {
                Debug.Log($"You gave �{moneyOnSurface - moneyShouldBeLeft} less change than the actual ticketprices");
                Debug.Log("ClientIsMad");
                clientSatisfactionScore -= Random.Range(23, 34);
                madClients++;
                StartCoroutine(PopUpReaction(1));
            }
        }
        else
        {
            Debug.Log("NotEnoughTickets");
            notGivenEnoughTicketsClients++;
            clientSatisfactionScore -= Random.Range(12, 17);
            StartCoroutine(PopUpReaction(0));
        }

        return -2;

    }

    IEnumerator PopUpReaction(int reactionNumber)
    {
        if (clientSatisfactionScore < 0)
        {
            clientSatisfactionScore = 0;
        }
        clientSatisfactionScoreText.text = clientSatisfactionScore.ToString("D6");

        if (!outOfTime)
        {
            popUp.GetComponent<Transform>().GetChild(reactionNumber).gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            popUp.GetComponent<Transform>().GetChild(reactionNumber).gameObject.SetActive(false);
        }
    }
    public void CorrectedCurrency(BaseEventData eventData)
    {
        if (isHelping)
        {
            StartCoroutine(PressedBell());
            CheckTicketsLogic();
            foreach (GameObject placedTickets in GameObject.FindGameObjectsWithTag("PlacedAdultTickets"))
            {
                Destroy(placedTickets);
            }
            foreach (GameObject placedTickets in GameObject.FindGameObjectsWithTag("PlacedChildrenTickets"))
            {
                Destroy(placedTickets);
            }

            List<GameObject> temporary = new List<GameObject>(CurrencyController_Dutch.Instance.coinsInCounter);
            foreach (GameObject coin in temporary)
            {
                Destroy(coin);
            }
            CurrencyController_Dutch.Instance.totalAdultTickets = 0;
            CurrencyController_Dutch.Instance.totalChildrentickets = 0;
            TicketController_Dutch.Instance.receivedMoney = 0;
            StartCoroutine(CustomerLeaves());
        }       
    }
    public void LastCustomerLeaves()
    {
        if (isHelping)
        {
            CheckTicketsLogic();
            foreach (GameObject placedTickets in GameObject.FindGameObjectsWithTag("PlacedAdultTickets"))
            {
                Destroy(placedTickets);
            }
            foreach (GameObject placedTickets in GameObject.FindGameObjectsWithTag("PlacedChildrenTickets"))
            {
                Destroy(placedTickets);
            }

            List<GameObject> temporary = new List<GameObject>(CurrencyController_Dutch.Instance.coinsInCounter);
            foreach (GameObject coin in temporary)
            {
                Destroy(coin);
            }
            CurrencyController_Dutch.Instance.totalAdultTickets = 0;
            CurrencyController_Dutch.Instance.totalChildrentickets = 0;
            TicketController_Dutch.Instance.receivedMoney = 0;
            StartCoroutine(CustomerLeaves());
        }
    }

    public IEnumerator PressedBell()
    {
        yield return new WaitForSeconds(0.01f);
        bellPart.transform.localPosition = new Vector3 (0, 0, 0);
        yield return new WaitForSeconds(0.35f);
        bellPart.transform.localPosition = new Vector3(0, 0.32f, 0);
    }

    public IEnumerator CustomerLeaves()
    {
        isHelping = false;
        anim.SetTrigger(leavingAnimationTrigger);
        yield return new WaitForSeconds(3);
      
        yield return new WaitForSeconds(1);
        Destroy(destroyPreviousNPC);

        yield return new WaitForSeconds(1);
        destroyPreviousNPC = null;

        if (!outOfTime)
        {
            SendNextCharacter();
        }
    }
}
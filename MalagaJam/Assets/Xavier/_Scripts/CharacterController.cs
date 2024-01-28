using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    public GameObject currentNPC;
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

    void Start()
    {
        SendNextCharacter();

        EventTrigger evTrig = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry clickEvent = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerClick
        };
        clickEvent.callback.AddListener(CorrectedCurrency);
        evTrig.triggers.Add(clickEvent);
    }


    private void SendNextCharacter()
    {
        currentNPC = allAvailableNPCs[Random.Range(0, allAvailableNPCs.Length)];

        var player = Instantiate(currentNPC);
        player.transform.parent = pLayer;

        destroyPreviousNPC = GameObject.FindGameObjectWithTag("NPC");
        adults = GameObject.FindGameObjectWithTag("Adults");
        children = GameObject.FindGameObjectWithTag("Children");
        anim = GameObject.FindAnyObjectByType<Animator>();

        amountOfAdults = Random.Range(1, 4);
        amountOfChildren = Random.Range(1, 4);

        if (adults != null && children != null)
        {
            adults.GetComponent<TextMeshProUGUI>().text = "Adults: " + amountOfAdults;
            children.GetComponent<TextMeshProUGUI>().text = "Kids: " + amountOfChildren;
        }

        StartCoroutine(TicketController.Instance.ReceiveMoney());
    }

    public int CheckTicketsLogic()
    {
        var moneyOnSurface = CurrencyController.Instance.TotalMoney;
        var ticketPrice = TicketController.Instance.totalTicketPrice;
        var receivedMoney = TicketController.Instance.receivedMoney;

        var moneyShouldBeLeft = receivedMoney - ticketPrice;

        var adultTickets = CurrencyController.Instance.totalAdultTickets;
        var childrenTickets = CurrencyController.Instance.totalChildrentickets;
        //Debug.Log("adults " + adultTickets + "--" + "childrens " + childrenTickets + "--" + "adults amount " + adultTickets + "--" + "childrens amount" + childrenTickets + "--");

        if (adultTickets == amountOfAdults && childrenTickets == amountOfChildren) { 
            if (moneyOnSurface > moneyShouldBeLeft) {
                Debug.Log("You left more money than needed");
                return 2;
            }
            else if (moneyOnSurface == moneyShouldBeLeft)
            {
                Debug.Log("Perfect");
                return 1;
            }
            else if (moneyOnSurface < moneyShouldBeLeft)
            {
                Debug.Log("ClientIsMad");
                return 0;
            }
        }
        else
        {
            Debug.Log("NotEnoughTickets");
            return -1;
        }

        return -2;

    }

    public void CorrectedCurrency(BaseEventData eventData)
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

        List<GameObject> temporary = new List<GameObject>(CurrencyController.Instance.coinsInCounter);
        foreach (GameObject coin in temporary)
        {
            Destroy(coin);
        }
        CurrencyController.Instance.totalAdultTickets = 0;
        CurrencyController.Instance.totalChildrentickets = 0;
        TicketController.Instance.receivedMoney = 0;
        StartCoroutine(CustomerLeaves());
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
        anim.SetTrigger(leavingAnimationTrigger);
        yield return new WaitForSeconds(3);
      
        yield return new WaitForSeconds(1);
        Destroy(destroyPreviousNPC);
        destroyPreviousNPC = null;

        yield return new WaitForSeconds(1);
        SendNextCharacter();     
    }
}
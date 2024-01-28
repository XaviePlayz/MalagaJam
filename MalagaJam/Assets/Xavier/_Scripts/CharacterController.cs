using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{



    public GameObject currentNPC;
    public GameObject destroyPreviousNPC;

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

    void Start()
    {
        SendNextCharacter();
        popUp = GameObject.Find("PopUp");
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
                StartCoroutine(PopUpReaction(3));
            }
            else if (moneyOnSurface == moneyShouldBeLeft)
            {
                Debug.Log("Perfect");
                StartCoroutine(PopUpReaction(2));

            }
            else if (moneyOnSurface < moneyShouldBeLeft)
            {
                Debug.Log("ClientIsMad");
                StartCoroutine(PopUpReaction(1));
            }
        }
        else
        {
            Debug.Log("NotEnoughTickets");
            StartCoroutine(PopUpReaction(0));
        }

        return -2;

    }

    IEnumerator PopUpReaction(int reactionNumber)
    {

        popUp.GetComponent<Transform>().GetChild(reactionNumber).gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        popUp.GetComponent<Transform>().GetChild(reactionNumber).gameObject.SetActive(false);


    }


    public void CorrectedCurrency()
    {
        Debug.Log("buttonpress");
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
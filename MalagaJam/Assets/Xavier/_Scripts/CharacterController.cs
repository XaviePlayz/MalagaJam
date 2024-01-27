using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

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

    void Start()
    {
        SendNextCharacter();
    }


    private void SendNextCharacter()
    {
        currentNPC = allAvailableNPCs[Random.Range(0, allAvailableNPCs.Length)];

        Instantiate(currentNPC);

        destroyPreviousNPC = GameObject.FindGameObjectWithTag("NPC");
        adults = GameObject.FindGameObjectWithTag("Adults");
        children = GameObject.FindGameObjectWithTag("Children");
        anim = GameObject.FindAnyObjectByType<Animator>();

        amountOfAdults = Random.Range(1, 4);
        amountOfChildren = Random.Range(1, 4);

        if (adults != null && children != null)
        {
            adults.GetComponent<TextMeshProUGUI>().text = "Adults: " + amountOfAdults;
            children.GetComponent<TextMeshProUGUI>().text = "Children: " + amountOfChildren;
        }
    }

    public void CorrectedCurrency()
    {
        foreach (GameObject draggedTickets in GameObject.FindGameObjectsWithTag("PlacedTicket"))
        {
            Destroy(draggedTickets);
        }
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
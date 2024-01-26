using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject currentNPC;
    public GameObject[] allAvailableNPCs; 
    public GameObject canvas;

    [Header("TextBubble")]
    [SerializeField] private GameObject adults;
    [SerializeField] private GameObject children;
    public int amountOfAdults;
    public int amountOfChildren;

    void Start()
    {
        CorrectedCurrency();
    }


    private void SendNextCharacter()
    {
        Instantiate(currentNPC);
        adults = GameObject.FindGameObjectWithTag("Adults");
        children = GameObject.FindGameObjectWithTag("Children");

        amountOfAdults = Random.Range(1, 4);
        amountOfChildren = Random.Range(1, 4);

        adults.GetComponent<TextMeshProUGUI>().text = "Adults: " + amountOfAdults;
        children.GetComponent<TextMeshProUGUI>().text = "Children: " + amountOfChildren;
    }

    public void CorrectedCurrency()
    {
        currentNPC = allAvailableNPCs[Random.Range(0, allAvailableNPCs.Length)];
        SendNextCharacter();
    }
}
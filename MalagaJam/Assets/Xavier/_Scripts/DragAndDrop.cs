using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool canMove;
    bool dragging;
    Collider2D col;

    public GameObject ticketInventory;
    public GameObject ticket;
    [SerializeField] private GameObject draggingTicket;
    private Vector3 offset = new Vector3(0, 0, -5);

    // Parallax layer for the Tickets
    [Header("Parllax Layer Transform")]
    public Transform pLayer;

    public Sprite PlacedAdultTickets;
    public Sprite PlacedChildrenTickets;
    public Sprite AdultTickets;
    public Sprite ChildrenTickets;
    void Start()
    {
        col = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;

    }
     // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
            if (canMove)
            {
                dragging = true;
            }
           

        }
        if (dragging)
        {
            if (ticketInventory != null)
            {
                if (draggingTicket == null && gameObject.CompareTag("AdultTickets"))
                {
                    var instantiatedTicket = Instantiate(ticket, ticketInventory.transform.position, Quaternion.identity);
                    instantiatedTicket.transform.parent = pLayer;
                    draggingTicket = instantiatedTicket;
                }
                else if (draggingTicket == null && gameObject.CompareTag("ChildrenTickets"))
                {
                    var instantiatedTicket = Instantiate(ticket, ticketInventory.transform.position, Quaternion.identity);
                    instantiatedTicket.transform.parent = pLayer;
                    draggingTicket = instantiatedTicket;
                }
                draggingTicket.transform.position = mousePos - offset;
            }
            else
            {
                this.transform.position = mousePos - offset;
            }

        }
        else
        {
            draggingTicket = null;
        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Counter") && gameObject.CompareTag("AdultTickets"))
        {
            ticket.tag = "PlacedAdultTickets";
            ticket.GetComponent<SpriteRenderer>().sprite = PlacedAdultTickets;

        }
        else if (other.gameObject.CompareTag("Counter") && gameObject.CompareTag("ChildrenTickets"))
        {
            ticket.tag = "PlacedChildrenTickets";
            ticket.GetComponent<SpriteRenderer>().sprite = PlacedChildrenTickets;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Counter") && gameObject.CompareTag("PlacedAdultTickets"))
        {
            ticket.tag = "AdultTickets";
            ticket.GetComponent<SpriteRenderer>().sprite = AdultTickets;
        }
        else if (other.gameObject.CompareTag("Counter") && gameObject.CompareTag("PlacedChildrenTickets"))
        {
            ticket.tag = "ChildrenTickets";
            ticket.GetComponent<SpriteRenderer>().sprite = ChildrenTickets;
        }
    }
}
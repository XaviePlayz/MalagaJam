using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool canMove;
    bool dragging;
    Collider2D col;

    public GameObject ticketInventory;
    private GameObject draggingTicket;
    private Vector3 offset = new Vector3(0, 0, -5);
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
                    Instantiate(ticketInventory);
                    draggingTicket = ticketInventory;
                    draggingTicket.tag = "PlacedAdultTickets";
                }
                else if (draggingTicket == null && gameObject.CompareTag("ChildrenTickets"))
                {
                    Instantiate(ticketInventory);
                    draggingTicket = ticketInventory;
                    draggingTicket.tag = "PlacedChildrenTickets";
                }
                draggingTicket.transform.position = mousePos - offset;
            }
            else
            {
                this.transform.position = mousePos - offset;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }
}

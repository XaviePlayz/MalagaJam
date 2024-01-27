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
    void Start()
    {
        col = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;

    }
     // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
                if (draggingTicket == null)
                {
                    Instantiate(ticketInventory);
                    draggingTicket = ticketInventory;
                    draggingTicket.tag = "PlacedTicket";
                }
                draggingTicket.transform.position = mousePos;
            }
            else
            {
                this.transform.position = mousePos;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool canMove;
    bool dragging;
    Collider2D col;

    public GameObject ticket;
    public GameObject draggingTicket;
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
            if (draggingTicket == null)
            {
                Instantiate(ticket);
                draggingTicket = ticket;
                draggingTicket.tag = "PlacedTicket";
            }
            ticket.transform.position = mousePos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }
}

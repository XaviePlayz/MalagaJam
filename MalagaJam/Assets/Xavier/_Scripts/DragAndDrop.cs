using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool canMove;
    bool dragging;
    Collider2D col;

    [Header("Booleans")]
    public bool onCounter;
    public static bool isDragging;
    public static bool IsAnyObjectBeingDragged = false;
    private bool isUsingController = false;

    [Header("Ticket Manager")]
    public GameObject ticketInventory;
    public GameObject ticket;
    [SerializeField] private GameObject draggingTicket;
    private Vector3 offset = new Vector3(0, 0, 5);

    // Parallax layer for the Tickets
    [Header("Parllax Layer Transform")]
    public Transform pLayer;

    [Header("Ticket Sprites")]
    public Sprite PlacedAdultTickets;
    public Sprite PlacedChildrenTickets;
    public Sprite AdultTickets;
    public Sprite ChildrenTickets;

    [Header("What Currency?")]
    public bool _50Cents;
    public bool _1Euro;
    public bool _5Euros;
    public bool _10Euros;

    private int defaultOrderInLayer;
    private int newOrderInLayer;

    [Header("Hover-Able Sprites")]
    public Sprite Hover50;
    public Sprite HoverPlaced50;
    public Sprite Hover100;
    public Sprite HoverPlaced100;
    public Sprite Hover500;
    public Sprite HoverPlaced500;
    public Sprite Hover1000;
    public Sprite HoverPlaced1000;

    Vector3 mousePos;
    Vector3 controllerCursorPos;

    void Start()
    {
        col = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;
        defaultOrderInLayer = GetComponent<SpriteRenderer>().sortingOrder;
        newOrderInLayer = 9;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Ensure the Z position is set correctly for the overlap check
        mousePos.z = 0f;

        // Get the world position of the controller cursor
        controllerCursorPos = Camera.main.ScreenToWorldPoint(ControllerCursor.Instance.cursorRectTransform.position);
        controllerCursorPos.z = 0f; // Set the Z position for the overlap check

        // Check for overlap with either the mouse or the controller cursor
        // Update is called once per frame
        if (col == Physics2D.OverlapPoint(mousePos) && !CharacterController_Dutch.Instance.outOfTime && TicketController_Dutch.Instance.gameStarted || col == Physics2D.OverlapPoint(controllerCursorPos) && !CharacterController_Dutch.Instance.outOfTime && TicketController_Dutch.Instance.gameStarted)
        {
            if (!isDragging)
            {
                canMove = true;
                GetComponent<SpriteRenderer>().sortingOrder = newOrderInLayer;
                if (_50Cents)
                {
                    if (!onCounter)
                    {
                        GetComponent<SpriteRenderer>().sprite = Hover50;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = HoverPlaced50;
                    }
                }
                if (_1Euro)
                {
                    if (!onCounter)
                    {
                        GetComponent<SpriteRenderer>().sprite = Hover100;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = HoverPlaced100;
                    }
                }
                if (_5Euros)
                {
                    if (!onCounter)
                    {
                        GetComponent<SpriteRenderer>().sprite = Hover500;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = HoverPlaced500;
                    }
                }
                if (_10Euros)
                {
                    if (!onCounter)
                    {
                        GetComponent<SpriteRenderer>().sprite = Hover1000;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = HoverPlaced1000;
                    }
                }
            }
        }
        else
        {
            if (!isDragging)
            {
                canMove = false;
                GetComponent<SpriteRenderer>().sortingOrder = defaultOrderInLayer;

                if (_50Cents)
                {
                    if (!onCounter)
                    {
                        GetComponent<SpriteRenderer>().sprite = CurrencyController_Dutch.Instance.unplaced50;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = CurrencyController_Dutch.Instance.placed50;
                    }
                }
                if (_1Euro)
                {
                    if (!onCounter)
                    {
                        GetComponent<SpriteRenderer>().sprite = CurrencyController_Dutch.Instance.unplaced100;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = CurrencyController_Dutch.Instance.placed100;
                    }
                }
                if (_5Euros)
                {
                    if (!onCounter)
                    {
                        GetComponent<SpriteRenderer>().sprite = CurrencyController_Dutch.Instance.unplaced500;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = CurrencyController_Dutch.Instance.placed500;
                    }
                }
                if (_10Euros)
                {
                    if (!onCounter)
                    {
                        GetComponent<SpriteRenderer>().sprite = CurrencyController_Dutch.Instance.unplaced1000;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = CurrencyController_Dutch.Instance.placed1000;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            isUsingController = false;
            PickUp();
        }

        if (Input.GetButtonDown("ControllerInteract"))
        {
            isUsingController = true;
            PickUp();
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

                if (isUsingController)
                {
                    draggingTicket.transform.position = controllerCursorPos - offset;
                }
                else
                {
                    draggingTicket.transform.position = mousePos - offset;
                }
            }
            else
            {
                if (isUsingController)
                {
                    this.transform.position = controllerCursorPos - offset;
                }
                else
                {
                    this.transform.position = mousePos - offset;
                }
            }

        }
        else
        {
            draggingTicket = null;

            if (IsAnyObjectBeingDragged)
            {
                IsAnyObjectBeingDragged = false;
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("ControllerInteract"))
        {
            canMove = false;
            dragging = false;
            isDragging = false;

            if (IsAnyObjectBeingDragged)
            {
                IsAnyObjectBeingDragged = false;
            }
        }
    }
    void PickUp()
    {
        if (!IsAnyObjectBeingDragged && IsHighestSortingOrder(this.gameObject))
        {
            if (col == Physics2D.OverlapPoint(mousePos) && !CharacterController_Dutch.Instance.outOfTime && TicketController_Dutch.Instance.gameStarted || col == Physics2D.OverlapPoint(controllerCursorPos) && !CharacterController_Dutch.Instance.outOfTime && TicketController_Dutch.Instance.gameStarted)
            {
                canMove = true;
                IsAnyObjectBeingDragged = true;
            }
            else
            {
                canMove = false;
            }

            if (canMove)
            {
                dragging = true;
                isDragging = true;
            }
        }
    }

    bool IsHighestSortingOrder(GameObject gameObject)
    {
        // Get the SpriteRenderer component of this object
        SpriteRenderer thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Find all objects that could be dragged
        DragAndDrop[] allDraggableObjects = FindObjectsOfType<DragAndDrop>();
        int highestSortingOrder = -1;
        GameObject highestOrderObject = null;

        foreach (var draggable in allDraggableObjects)
        {
            SpriteRenderer spriteRenderer = draggable.GetComponent<SpriteRenderer>();
            if (spriteRenderer.sortingOrder > highestSortingOrder)
            {
                highestSortingOrder = spriteRenderer.sortingOrder;
                highestOrderObject = draggable.gameObject;
            }
        }

        // Return true if this object has the highest sorting order
        return gameObject == highestOrderObject;
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
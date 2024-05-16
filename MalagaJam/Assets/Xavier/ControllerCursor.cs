using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerCursor : MonoBehaviour
{
    #region Singleton

    private static ControllerCursor _instance;
    public static ControllerCursor Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ControllerCursor>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(ControllerCursor).Name;
                    _instance = obj.AddComponent<ControllerCursor>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public GameObject cursor;
    public RectTransform cursorRectTransform;
    public Canvas canvas;
    public float cursorSpeed = 100f;

    public Vector2 cursorPosition;

    private float lastRealTime;
    public bool isControllerConnected = false;
    public bool controllerCurrentlyConnected = false;

    void Awake()
    {
        // Initialize lastRealTime
        lastRealTime = Time.realtimeSinceStartup;
    }


    void Start()
    {
        if (cursor != null)
        {
            cursor.SetActive(false);
            cursorRectTransform.pivot = new Vector2(0.1429948f, 0.8577762f);

            // Initialize the cursor position
            cursorPosition = cursorRectTransform.anchoredPosition;
        }

        isControllerConnected = false;
        controllerCurrentlyConnected = false;
    }

    void Update()
    {
        string[] joystickNames = Input.GetJoystickNames();

        // Check if any joystick is currently connected
        foreach (var name in joystickNames)
        {
            if (!string.IsNullOrEmpty(name))
            {
                controllerCurrentlyConnected = true;
                break;
            }
        }

        // Failsafe check for controller reconnection through input movement
        // This check now only considers joystick axis values, not keyboard inputs
        if (!controllerCurrentlyConnected)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                controllerCurrentlyConnected = true;
            }
        }

        // If a controller is connected, handle cursor movement
        if (controllerCurrentlyConnected)
        {
            if (cursor != null && !isControllerConnected)
            {
                cursor.SetActive(true);
                Debug.Log("Controller is connected.");
            }
            isControllerConnected = true;

            if (cursor != null)
            {
                // Calculate the real delta time
                float deltaRealTime = Time.realtimeSinceStartup - lastRealTime;
                lastRealTime = Time.realtimeSinceStartup;

                // Get input from the controller's axes
                Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                cursorPosition += input * deltaRealTime * cursorSpeed;

                // Convert the cursor position to canvas space
                Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
                cursorPosition.x = Mathf.Clamp(cursorPosition.x, -canvasSize.x / 2, canvasSize.x / 2);
                cursorPosition.y = Mathf.Clamp(cursorPosition.y, -canvasSize.y / 2, canvasSize.y / 2);

                cursorRectTransform.anchoredPosition = cursorPosition;
            }
        }
        else
        {
            if (cursor != null && isControllerConnected)
            {
                cursor.SetActive(false);
                Debug.Log("Controller is disconnected.");
            }
            isControllerConnected = false;
        }
    }
}
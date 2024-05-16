using UnityEngine;

public class JoystickMouseController : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public RectTransform cursorRectTransform;

    void Update()
    {
        // Check if any joystick is connected and if the left joystick is being moved
        if (Input.GetJoystickNames().Length > 0 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            // Get input from the top-left joystick
            float moveX = Input.GetAxis("Horizontal") * sensitivity;
            float moveY = Input.GetAxis("Vertical") * sensitivity;

            // Calculate new position based on joystick input
            Vector3 newMousePosition = new Vector3(moveX, moveY, 0f) + cursorRectTransform.position;

            // Move the cursor RectTransform to the new position
            cursorRectTransform.position = newMousePosition;

            // Simulate mouse click with a joystick button, for example, the "Fire1" button
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Clicked");
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layerTrans;
    public float parallaxScale;

    public ParallaxLayer(Transform layerTrans, float parallaxScale)
    {
        this.layerTrans = layerTrans;
        this.parallaxScale = parallaxScale;
    }
}

public class ParallaxManager : MonoBehaviour
{
    public List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();
    private List<Vector3> layerStartPos = new List<Vector3>();
    public Vector2 frontlayerParallax;
    public ControllerCursor controllerCursor; // Reference to the ControllerCursor script
    private Vector2 lastInputPosition;
    public float smoothTime = 2f; // Time taken to smooth the movement
    private Vector2 velocity = Vector2.zero; // Used by SmoothDamp

    void Start()
    {
        foreach (var pLayer in parallaxLayers)
        {
            layerStartPos.Add(pLayer.layerTrans.localPosition);
        }
        // Initialize last input position to the screen center
        lastInputPosition = new Vector2(0.5f, 0.5f); // Normalized screen coordinates
    }

    void Update()
    {
        Vector2 inputPos = GetInputPosition();
        lastInputPosition = inputPos; // Always update last input position with the current position

        // Use SmoothDamp for smooth transition
        Vector2 displacement = Vector2.SmoothDamp(lastInputPosition, inputPos, ref velocity, smoothTime);

        int count = 0;
        foreach (var pLayer in parallaxLayers)
        {
            Vector3 newLocalPos = layerStartPos[count] + new Vector3(displacement.x * pLayer.parallaxScale, displacement.y * pLayer.parallaxScale, 0);
            pLayer.layerTrans.localPosition = newLocalPos;

            if (count == 1)
            {
                frontlayerParallax = new Vector2(newLocalPos.x, newLocalPos.y);
            }
            count++;
        }
    }

    Vector2 GetInputPosition()
    {
        // If the controller cursor is active, use its position; otherwise, use the mouse position
        if (ControllerCursor.Instance.cursor.activeInHierarchy)
        {
            Vector2 cursorScreenPosition = RectTransformUtility.WorldToScreenPoint(null, controllerCursor.cursorRectTransform.position);
            return new Vector2(-cursorScreenPosition.x / Screen.width, -cursorScreenPosition.y / Screen.height);
        }
        else
        {
            // Normalize the mouse position to be within 0 and 1
            return new Vector2(-Input.mousePosition.x / Screen.width, -Input.mousePosition.y / Screen.height);
        }
    }
}
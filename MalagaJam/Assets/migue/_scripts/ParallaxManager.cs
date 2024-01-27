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


// Use: In the editor, create empty objects (layers) and assign each object that you want to be in that layer as a child
// Create an empty Gameobject, called for example ParallaxManager, and assign this script
// After that, assign the Layers to the parallaxLayers list and assign also the parallaxScale for each layer (bigger = more displacement
public class ParallaxManager : MonoBehaviour
{
    // Define layer array
    public List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();
    private List<Transform> layerStartPos = new List<Transform>(); 

    // Start is called before the first frame update
    void Start()
    {
        foreach (var pLayer in parallaxLayers)
        {
            layerStartPos.Add(pLayer.layerTrans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var screenPoint = Input.mousePosition;
        float halfWidth = Screen.width / 2f;
        float halfHeight = Screen.height / 2f;
        Vector2 displacement = new Vector2((screenPoint.x - halfWidth)/halfWidth , (screenPoint.y - halfHeight) / halfHeight);  
        // Move on the contrary direction to the mouse displacement
        displacement *= -1f;
        

        int count = 0;
        foreach (var pLayer in parallaxLayers)
        {
            pLayer.layerTrans.localPosition = layerStartPos[0].localPosition + new Vector3(displacement.x * pLayer.parallaxScale, displacement.y * pLayer.parallaxScale, 0);
            count = count + 1;
        }
    }
}

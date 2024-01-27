using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [Serializable]
    public struct Message {
        public Sprite Avatar;
        public string Name;
        public string Review;
    }

    [SerializeField] Message[] allMessages;

    void Start()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        int N = allMessages.Length;
        for (int i = 0; i < N; i++) {
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(0).GetComponent<RectTransform>().GetComponent<Image>().sprite = allMessages[i].Avatar;
            g.transform.GetChild(1).GetComponent<TMP_Text>().text = allMessages[i].Name;
            g.transform.GetChild(2).GetComponent<TMP_Text>().text = allMessages[i].Review;
        }

        Destroy(buttonTemplate);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}

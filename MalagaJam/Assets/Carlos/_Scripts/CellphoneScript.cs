using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


using Random = UnityEngine.Random;
public class CellPhoneScript : MonoBehaviour
{

    // Singleton
    public static CellPhoneScript instance;
    private void Awake() { instance = this; }


    [Serializable]
    public struct Message {
        public Sprite Avatar;
        public string Name;
        public string Review;

        public Message(Sprite Avatar,  string Name, string Review)
        {
            this.Avatar = Avatar;  
            this.Name = Name;  
            this.Review = Review;
        }
    }

    public TextAsset textAssetData;
    public GameObject buttonTemplate;

    private List<Message> messageList = new List<Message>();
    private List<GameObject> messageGOList = new List<GameObject>();
    public float minTime;
    public float maxTime;
    private int messageCount = 0;

    private GameObject PopNewMessage(Message msg)
    {
        GameObject g;

        g = Instantiate(buttonTemplate, transform);
        g.transform.GetChild(0).GetComponent<RectTransform>().GetComponent<Image>().sprite = msg.Avatar;
        g.transform.GetChild(1).GetComponent<TMP_Text>().text = msg.Name;
        g.transform.GetChild(2).GetComponent<TMP_Text>().text = msg.Review;

        return g;
    }

    public void NextMessage()
    {
        if (messageList.Count > messageCount)
        {
            if (messageGOList.Count == 4) {
                Destroy(messageGOList[0]);
                messageGOList.RemoveAt(0);
            }
            messageGOList.Add(PopNewMessage(messageList[messageCount]));

            messageCount++;
        }
        else
        {
            StopCoroutine(TriggerNextMessagePeriodically());
        }
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 3 - 1;

        for (int i = 0; i < tableSize; i++)
        {
            string avatarName = data[3 * (i + 1)];
            string name = data[3 * (i + 1) + 1];
            string review = data[3 * (i + 1) + 2];
            Sprite avatar = Resources.Load<Sprite>("Sprites/Avatars/" + avatarName);

            messageList.Add(new Message(avatar, name, review));
        }

    }

    IEnumerator TriggerNextMessagePeriodically()
    {
        NextMessage();
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        StartCoroutine(TriggerNextMessagePeriodically());
    }


    void Start()
    {
        ReadCSV();
        StartCoroutine(TriggerNextMessagePeriodically());
    }




}

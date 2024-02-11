using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using Random = UnityEngine.Random;
public class CellPhoneScript : MonoBehaviour, IPointerClickHandler
{

    // Singleton
    public static CellPhoneScript instance;
    private void Awake() { instance = this; }


    [Serializable]
    public struct Message {
        public Sprite Avatar;
        public string Name;
        public string Review;
        public int starNumber;

        public Message(Sprite Avatar,  string Name, string Review, int starNumber)
        {
            this.Avatar = Avatar;  
            this.Name = Name;  
            this.Review = Review;
            this.starNumber = starNumber;
        }
    }

    public TextAsset textAssetData;
    public GameObject buttonTemplate;
    public float movingTime;
    private List<Message> messageList = new List<Message>();
    private List<GameObject> messageGOList = new List<GameObject>();
    public float minTime;
    public float maxTime;
    private int messageCount = 0;

    private float yPosUp = 20f;
    private float yPosDown = -220f;
    private bool up = false;
    private Coroutine moveCoroutine;

    private GameObject PopNewMessage(Message msg)
    {
        GameObject g;

        g = Instantiate(buttonTemplate, transform);
        g.transform.GetChild(0).GetComponent<RectTransform>().GetComponent<Image>().sprite = msg.Avatar;
        g.transform.GetChild(0).gameObject.SetActive(false);
        g.transform.GetChild(1).GetComponent<TMP_Text>().text = msg.Name;
        g.transform.GetChild(2).GetComponent<TMP_Text>().text = msg.Review;
        // enable only the corresponding star
        int starNumber = msg.starNumber;
        g.transform.GetChild(3 + starNumber).gameObject.SetActive(true);

        return g;
    }

    public void NextMessage()
    {
        if (messageList.Count > messageCount)
        {
            StartCoroutine(Tremble());
            if (messageGOList.Count == 3) {
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

        int tableSize = data.Length / 4 - 1;

        for (int i = 0; i < tableSize; i++)
        {

            string avatarName = data[4 * (i + 1)];
            string name = data[4 * (i + 1) + 1];
            string review = data[4 * (i + 1) + 2];
            int starReview = int.Parse(data[4 * (i + 1) + 3]);
            Sprite avatar = Resources.Load<Sprite>("Sprites/Avatars/" + avatarName);
            messageList.Add(new Message(avatar, name, review, starReview));
        }

    }

    IEnumerator TriggerNextMessagePeriodically()
    {
        NextMessage();
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        StartCoroutine(TriggerNextMessagePeriodically());
    }


    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        // Stop transition if it is already moving
        float time = 0;
        RectTransform rt = this.GetComponent<RectTransform>();
        Vector3 startPosition = rt.anchoredPosition;
        while (time < duration)
        {
            rt.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        rt.anchoredPosition = targetPosition;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeCellPos();
    }

    public void ChangeCellPos()
    {
        if (up == true)
        {
            MoveCellphoneDown();
        }
        else
        {
            MoveCellphoneUp();
        }
    }


    void Start()
    {
        ReadCSV();
        StartCoroutine(TriggerNextMessagePeriodically());
    }

    private void MoveCellphoneUp()
    {
        up = true;
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(LerpPosition(new Vector3(-20f, yPosUp,0f), movingTime));
    }
    private void MoveCellphoneDown()
    {
        up = false;
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(LerpPosition(new Vector3(-20f, yPosDown, 0f), movingTime));
    }

    IEnumerator Tremble()
    {
        RectTransform rt = this.GetComponent<RectTransform>();
        Vector3 startPosition = rt.anchoredPosition;
        for (int i = 0; i < 10; i++)
        {
            rt.anchoredPosition += new Vector2(3f, 0);
            yield return new WaitForSeconds(0.03f);
            rt.anchoredPosition -= new Vector2(3f, 0);
            yield return new WaitForSeconds(0.03f);
        }
    }

}

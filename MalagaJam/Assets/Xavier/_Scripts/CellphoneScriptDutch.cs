using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using Random = UnityEngine.Random;
public class CellPhoneScriptDutch : MonoBehaviour, IPointerClickHandler
{
    #region Singleton

    private static CellPhoneScriptDutch _instance;
    public static CellPhoneScriptDutch Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CellPhoneScriptDutch>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(CellPhoneScriptDutch).Name;
                    _instance = obj.AddComponent<CellPhoneScriptDutch>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public GameObject phone, optionsMenu;
    public GameObject buttonTemplate;
    public float movingTime;
    public float minTime;
    public float maxTime;

    private float yPosUp = 20f;
    private float yPosDown = -220f;
    private bool up = false;
    private Coroutine moveCoroutine;

    public GameObject settingsButton;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeCellPos();
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeCellPos();
    }

    public void ChangeCellPos()
    {
        if (up == true && !optionsMenu.activeSelf)
        {
            MoveCellphoneDown();
        }
        else
        {
            MoveCellphoneUp();
        }
    }

    private void MoveCellphoneUp()
    {
        up = true;

        if (ControllerCursor.Instance.isControllerConnected)
        {
            if (ControllerCursor.Instance.cursor != null)
            {
                ControllerCursor.Instance.cursor.GetComponent<Image>().enabled = false;
                EventSystem.current.SetSelectedGameObject(settingsButton);
            }
        }
        if (!ControllerCursor.Instance.isControllerConnected)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(LerpPosition(new Vector3(-20f, yPosUp,0f), movingTime));
    }
    public void MoveCellphoneDown()
    {
        if (!optionsMenu.activeSelf)
        {
            up = false;

            if (ControllerCursor.Instance.isControllerConnected)
            {
                if (ControllerCursor.Instance.cursor != null)
                {
                    ControllerCursor.Instance.cursor.GetComponent<Image>().enabled = true;
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
            if (!ControllerCursor.Instance.isControllerConnected)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(LerpPosition(new Vector3(-20f, yPosDown, 0f), movingTime));
        }       
    }
}

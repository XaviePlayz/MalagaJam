using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMoveCellphone : MonoBehaviour
{

    Button button;
    void OnEnable()
    {
        button = this.GetComponent<Button> ();
        //Register Button Events
        button.onClick.AddListener(() => CellPhoneScript.instance.ChangeCellPos());
    }

    void OnDisable()
    {
        //Un-Register Button Events
        button.onClick.RemoveAllListeners();

    }
}

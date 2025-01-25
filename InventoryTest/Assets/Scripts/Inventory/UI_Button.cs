using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerClickHandler 
{
    public Action ClickFunc = null;
    public Action MouseRightClickFunc = null;



    public void OnPointerClick (PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ClickFunc != null) ClickFunc(); 
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (MouseRightClickFunc != null) MouseRightClickFunc(); 
        }
    }
   
}

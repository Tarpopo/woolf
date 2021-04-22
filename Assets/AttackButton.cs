using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour,IPointerDownHandler
{
    public delegate  void button(int numb);
    public event button _buttonDown;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        print("down");
        _buttonDown?.Invoke(0);
    }

    private void OnDisable()
    {
        _buttonDown = null;
    }
}

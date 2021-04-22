using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBlock : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private Player _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _player._blockTrigger = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _player._blockTrigger = false;
    }
    
}

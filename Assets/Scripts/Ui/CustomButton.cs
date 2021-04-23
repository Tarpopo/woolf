using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[RequireComponent(typeof(SpriteRenderer))]
public class CustomButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Sprite _down;
    [SerializeField] private Sprite _up;
    private SpriteRenderer _spriteRenderer;
    public UnityEvent ButtonDown;
    public UnityEvent ButtonUp;
    public bool IsButtonActive { get; private set; }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsButtonActive = true;
        _spriteRenderer.sprite = _down;
        ButtonDown?.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _spriteRenderer.sprite = _up;
        IsButtonActive = false;
        ButtonUp?.Invoke();
    }
    private void OnDisable()
    {
        ButtonDown = null;
    }
}

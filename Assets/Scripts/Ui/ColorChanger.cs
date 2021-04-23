using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger<T> where T: Graphic
{
    private T _component;
    private Color _baseColor;
    private bool _isBaseColor;

    public ColorChanger(T component)
    {
        _component = component;
    }

    public void SetBaseColor(Color color)
    {
        _baseColor = color;
    }

    public void ChangeColor(Color color,bool withBaseColor=false)
    {
        if (withBaseColor)
        {
            _component.color = _isBaseColor ? _baseColor: color;
            _isBaseColor = !_isBaseColor;
            return;
        }
        _component.color = color;
    }

   

}

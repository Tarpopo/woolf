using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class TextCombo: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnimationClip _zoom;
    [SerializeField] private float _dissolutionTime;
    
    private Animator _animator;
    private readonly Dictionary<int, string> _dictionary = new Dictionary<int, string>()
    {
        {2, "Good"},
        {5, "Nice"},
        {10,"Perfect"}
    };

    private ColorDissolution<TextMeshProUGUI> _colorDissolution;
    
    private void Start()
    {
        _text.color=Color.clear;
        _animator = GetComponent<Animator>();
        _colorDissolution=new ColorDissolution<TextMeshProUGUI>(_text);    
    }

    public void CheckText(int hitCount)
    {
        foreach (var item in _dictionary)
        {
            if (item.Key != hitCount) continue;
            _animator.Play(_zoom.name);
            _colorDissolution.StartDissolution(_dissolutionTime);
            _text.text = item.Value;
        }
    }
    //InAnimationEvent
    public void ChangeColor() { }

}

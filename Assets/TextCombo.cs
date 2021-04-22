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
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private AnimationClip _electricSphere;
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

    public bool CheckText(int hitCount)
    {
        foreach (var item in _dictionary)
        {
            if (item.Key != hitCount) continue;
            _animator.Play(_zoom.name);
            Toolbox.Get<ParticleManager>().PlayDetachedParticle(_electricSphere,_text.transform.position,0.8f,_text.transform);
            _colorDissolution.StartDissolution(_dissolutionTime);
            _text.text = item.Value;
            return true;
        }
        return false;
    }
    //InAnimationEvent
    public void ChangeColor() { }

}

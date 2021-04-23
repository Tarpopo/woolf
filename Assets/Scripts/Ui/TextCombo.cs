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
    
    private AudioSource _audioSource;
    private Animator _animator;
    public List<ComboTextItem> _items;
    

    private ColorDissolution<TextMeshProUGUI> _colorDissolution;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _text.color=Color.clear;
        _animator = GetComponent<Animator>();
        _colorDissolution=new ColorDissolution<TextMeshProUGUI>(_text);    
    }

    public bool CheckText(int hitCount)
    {
        foreach (var item in _items)
        {
            if (item.HitValue != hitCount) continue;
            _audioSource.PlayOneShot(item.Sound);
            _animator.Play(_zoom.name);
            Toolbox.Get<ParticleManager>().PlayDetachedParticle(_electricSphere,_text.transform.position,0.8f,_text.transform);
            _colorDissolution.StartDissolution(_dissolutionTime);
            _text.text = item.Name;
            return true;
        }
        return false;
    }
    //InAnimationEvent
    public void ChangeColor() { }
}
[Serializable]
public struct ComboTextItem
{
    public int HitValue;
    public string Name;
    public SimpleSound Sound;
}

using TMPro;
using UnityEngine;
public class HitScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _xTextMexh;
    [SerializeField] private Color _color;
    [SerializeField] private AnimationClip _zoom;
    [SerializeField] private TextCombo _textCombo;
    [SerializeField] private float _dissolutionTime;
    [SerializeField] private AnimationClip _electricExplo;
    
    private int _hitCount;
    private int _MaxHitCount;
    private ColorDissolution<TextMeshProUGUI> _xTextDissolution;
    private ColorChanger<TextMeshProUGUI> _hitsColor;
    private Animator _animator;

    private void Start()
    {
        _xTextDissolution = new ColorDissolution<TextMeshProUGUI>(_xTextMexh);
        _hitsColor = new ColorChanger<TextMeshProUGUI>(_xTextMexh);
        _animator = GetComponent<Animator>();
        _hitsColor.SetBaseColor(_xTextMexh.color);
        _xTextMexh.color=Color.clear;
    }
    
    private void ClearHits()
    {
        _hitCount = 0;
    }

    public void UpdateHitCount()
    {
        _hitCount++;
        if (_textCombo.CheckText(_hitCount))
        {
            Toolbox.Get<ParticleManager>().PlayDetachedParticle(_electricExplo,_xTextMexh.transform.position,0.8f,_xTextMexh.transform);
        }
        _xTextMexh.text = "x" + _hitCount;
        _animator.Play(_zoom.name);
        _xTextDissolution.StartDissolution(_dissolutionTime,ClearHits);
    }
    //InAnimationEvent
    public void ChangeColor()
    {
        _hitsColor.ChangeColor(_color,true);
    }
    
}

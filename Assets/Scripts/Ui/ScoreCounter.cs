using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private int _minCountForHit;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private AnimationClip _zoom;
    [SerializeField] private GameObject _coin;
    [SerializeField] private float _flySpeed;
    [SerializeField] private SimpleSound _coinSound;
    [SerializeField] private TextMeshProUGUI _finalScore;
    private ManagerPool _pool;
    private AudioSource _audioSource;
    private ColorDissolution<TextMeshProUGUI> _textDissolution;
    private Animator _animator;
    private int _allCount;

    private void Start()
    {
        _pool = Toolbox.Get<ManagerPool>();
        _audioSource = GetComponent<AudioSource>();
        _pool.AddPool(PoolType.Entities).PopulateWith(_coin,15);
        _animator = GetComponent<Animator>();
        _textDissolution=new ColorDissolution<TextMeshProUGUI>(_count);
    }

    public void AddCount(int comboCount,Vector3 enemyPosition)
    {
        _allCount +=_minCountForHit*comboCount;
        UpdateText(_allCount);
        _textDissolution.StartDissolution(3.5f);
        var coin=_pool.Spawn(PoolType.Entities, _coin, enemyPosition);
        StartCoroutine(MoveCoin(coin.transform, transform));
    }

    private IEnumerator MoveCoin(Transform coinTransform,Transform moveTo)
    {
        while (Vector2.Distance(coinTransform.position, moveTo.position) > 0.04f)
        {
            coinTransform.position = Vector3.Slerp(coinTransform.position, moveTo.position, _flySpeed);
            yield return null;
        }
        _audioSource.PlayOneShot(_coinSound);
        _animator.Play(_zoom.name);
        _pool.Despawn(PoolType.Entities,coinTransform.gameObject);
    }

    private void UpdateText(int count)
    {
        _count.text = count.ToString();
        _finalScore.text = count.ToString();
    }
    
    
    public void ChangeColor() { }
}

using System;
using Unity.Mathematics;
using UnityEngine;

public class Actor:MonoBehaviour
{
    public DataActor Data;
    protected Rigidbody2D _rigidBody;
    public AnimationBehavior AnimaBeh;
    private SpriteRenderer _renderer;
    protected float time;
    [SerializeField] protected Transform _attackPos;
    public bool _takeDamage = false;
    private AnimationsType animations;
    private int _currentHealth;
    protected Collider2D[] enemy;
    
    public  virtual void AttackEnemy(int damage=1)
    {
        if (!_takeDamage)
        {
             enemy = Physics2D.OverlapBoxAll( _attackPos.position, Data.attackSize,0, Data.whoisEnemy);
             for (int i = 0; i < enemy.Length; i++)
             {
                 if (enemy[i])
                 {
                     enemy[i].GetComponent<Actor>().TakeDamage(damage);
                 }
             }
           
        }
    }

    private void Start()
    {
        _currentHealth = Data.Health;
        _rigidBody = GetComponent<Rigidbody2D>();
        ManagerUpdate.AddTo(this);  
        AnimaBeh = GetComponent<AnimationBehavior>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        StartGame();
    }
    protected virtual void StartGame(){ }
    protected virtual void Death()
    {
        gameObject.transform.position = Vector2.zero;
        gameObject.SetActive(false);
        ManagerUpdate.RemoveFrom(this);
    }   
        
    public void Restart()
    {
        if (!gameObject.activeSelf)
        {
            ManagerUpdate.AddTo(this);
            gameObject.SetActive(true);
        }
    }
        
    protected virtual bool Timer()
    {
        if (time >0)
        {
            time -= Time.deltaTime;
            return true;
        }
        return false;
    }

    public virtual void TakeDamage(int damage=1)
    {
        _takeDamage = true;
        AnimaBeh.PlayAnim(AnimationsType.takeDamage);
        _currentHealth -= damage;
        if(_currentHealth<=0) Death();
        time = AnimaBeh.clips[(int) AnimationsType.takeDamage]._clip.length;
    }

    public void EndTakeDamage()
    {
        _takeDamage = false;
    }
        

   
}
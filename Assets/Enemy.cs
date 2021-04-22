using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Enemy : Actor,ITick
{
    private Transform _playerTransform;
    public float followArea;
    public float notFollowArea;
    public float speed;
    private float _attackColdown;
    public float attackColdown;
    private float attackEndTime;
    private Player _player;
   // public Color _Color;
    public Rigidbody2D _rigidBody;
    //private bool isAttack = false;
    private float Distance;
    private float _attackPlaceDistance;
    private int _havePlace = -1;
    private Transform _attackPosPlace;
    private float _blueCicleTime;
    public float _setBlueCicleTime;
    private SpriteRenderer _spriteRenderer;
    //private NavMeshAgent agent;
    protected override void StartGame()
    {
        //agent = GetComponent<NavMeshAgent>();
        _spriteRenderer=GetComponent<SpriteRenderer>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer.receiveShadows = true;
        _spriteRenderer.shadowCastingMode = ShadowCastingMode.On;
        //attackEndTime = data.clips[(int)AnimationsType.hit].length;
    }
    
    public void Tick()
    {
        if (Timer()) return;
        
        Distance = Vector2.Distance(transform.position, _playerTransform.position);
        if (_playerTransform)
        {
            RotateToPlayer();
            //FollowPlayer();
            //Attack();
            //_rigidBody.velocity = ((Vector2)_playerTransform.position-_rigidBody.position).normalized * speed;
            _rigidBody.MovePosition(_rigidBody.position+(Vector2)(_playerTransform.position-transform.position)*speed);
            if(_havePlace!=-1)Hit();
        }
        

    }
    
    private void RotateToPlayer()
    {
        transform.localScale = new Vector3(Mathf.Sign(_playerTransform.position.x-transform.position.x), 1, 1); 
        //attackPos.x =attackPosX* -1;
        ////data.attackPos.x =attackPosX*-1;
    }

    // public override void Attack(int damage = 1)
    // {
    //     if(_attackColdown<=0&&Distance<=notFollowArea||_attackColdown<=0&&isAttack)
    //     {
    //         if (!isAttack)
    //         {
    //             //Animation(AnimationsType.hit);
    //             Anima(AnimationsType.hit);
    //             time=data.clips[(int)AnimationsType.hit].length;
    //             isAttack = true;
    //             _takeDamage = false;
    //         }
    //         //attackEndTime -= Time.deltaTime;
    //         if (time <= 0&&isAttack)
    //         {
    //             base.Attack();
    //             _attackColdown = attackColdown;
    //             isAttack = false;
    //             //attackEndTime=data.clips[(int)AnimationsType.hit].length;
    //         }
    //     }
    // }
    // private bool IsAnimationPlaying (AnimationClip clip)
    // {
    //     var anim_state = AnimaBeh._animator.GetCurrentAnimatorStateInfo(0);
    //     if (anim_state.IsName(clip.name))
    //         return true;
    //     return false;
    // }
    private void Hit()
    {
        if (_attackColdown > 0)
        {
            _attackColdown -= Time.deltaTime;
        }
        else
        {
            // if(_attackColdown<=0&&Distance<=notFollowArea)
            // {
            //     Animation(AnimationsType.hit);
            //     _attackColdown = attackColdown;
            //     time = AnimaBeh.clips[(int) AnimationsType.hit]._clip.length;
            // }
            if(_attackColdown<=0&&_attackPlaceDistance<=_player._attackPlaceDistance)
            {
                //Animation(AnimationsType.hit);
                _attackColdown = attackColdown;
                time = AnimaBeh.clips[(int) AnimationsType.hit]._clip.length;
            }
        }
    }
    // protected override void Death() 
    // {
    //     _player._attackPlace.Remove(_havePlace);
    //     _havePlace = -1;
    //     base.Death();
    // }
    //
    // public override void AttackEnemy(int damage = 1)
    // {
    //     if(!_player._blockTrigger)base.AttackEnemy(damage);
    //     else
    //     {
    //         if (_player._blockTimeNow <= 0)
    //         {
    //             base.AttackEnemy(damage);
    //             return;
    //         }
    //         time = AnimaBeh.clips[(int) AnimationsType.hitBack]._clip.length;
    //         Animation(AnimationsType.hitBack);
    //     }
    // }
    //
    // private void FollowPlayer()
    // {
    //     if (Distance <= followArea)
    //     {
    //         if (_havePlace == -1)
    //         {
    //             CalcAttackPlace();
    //             if (_havePlace == -1)
    //             {
    //                 if(Distance<=1)Animation(AnimationsType.idle);
    //                 else
    //                 {
    //                     _rigidBody.MovePosition((Vector2)_playerTransform.position*speed*Time.deltaTime);
    //                     //agent.destination = _playerTransform.position;
    //                 }
    //             }
    //         }
    //
    //         if (_havePlace!=-1)
    //         {
    //             if(_blueCicleTime>0) _blueCicleTime -= Time.deltaTime;
    //             _attackPlaceDistance = Vector3.Distance(transform.position, _player._attackPlacePos[_havePlace].position);
    //             if (_attackPlaceDistance > _player._attackPlaceDistance&&_blueCicleTime<=0)
    //             {
    //                 Animation(AnimationsType.walk);
    //                 _rigidBody.MovePosition(_rigidBody.position+(Vector2)_player._attackPlacePos[_havePlace].position*speed*Time.deltaTime);
    //                 //agent.destination = _attackPosPlace.position;
    //             }
    //             if(_blueCicleTime>0)Animation(AnimationsType.idle);
    //         }
    //         
    //     }
    //     if(_attackPlaceDistance<=_player._attackPlaceDistance&&_attackColdown>0)//||Distance>followArea)
    //     {
    //         if(_blueCicleTime<=0)_blueCicleTime = _setBlueCicleTime;
    //         //Animation(AnimationsType.idle);
    //         Animation(AnimationsType.idle);
    //     }
    //
    //     if (Distance>followArea)
    //     {
    //
    //         if (_havePlace != -1)
    //         {
    //             _player._attackPlace.Remove(_havePlace);
    //             _havePlace = -1;
    //         }
    //         Animation(AnimationsType.idle);
    //     }
    //     
    // }
    //
    // private void CalcAttackPlace()
    // {
    //     int count=_player._attackPlace.Count;
    //     if (count == 0)
    //     {
    //         var position = transform.position;
    //         float dist1 = Vector3.Distance(position, _player._attackPlacePos[0].position);
    //         float dist2 = Vector3.Distance(position, _player._attackPlacePos[1].position);
    //         if (dist1 < dist2)
    //         {
    //             _havePlace = 0;
    //         }
    //         else
    //         {
    //             _havePlace = 1;
    //         }
    //         _attackPosPlace = _player._attackPlacePos[_havePlace];
    //         _player._attackPlace.Add(_havePlace);
    //     }
    //     if (count == 1)
    //     {
    //         if (_player._attackPlace[0] == 0)
    //         {
    //             _havePlace = 1;
    //         }
    //         else
    //         {
    //             _havePlace = 0;
    //         }
    //         _attackPosPlace = _player._attackPlacePos[_havePlace];
    //         _player._attackPlace.Add(_havePlace);
    //     }
    //     
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(attackPos, data.attackSize);
        Gizmos.DrawWireSphere(transform.position,followArea);
        Gizmos.DrawWireSphere(transform.position,notFollowArea);
    }
}

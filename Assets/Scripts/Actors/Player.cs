
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Player : Actor,ITick
{
    public Joystick joy;
    public float speed;
    //private Rigidbody2D _rigidBody;
    private FightingCombo _fight;
    private GameObject button1;
    public float _blockTimeNow;
    public float _blockTime;
    public bool _blockTrigger = false;
    
    [SerializeField] private HitScore _HitScore;
    
    
    public float _attackPlaceDistance;
    //public List<int> _attackPlace;
    //private bool _uppercutTrig = false;
    //private Enemy _enemy;
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private JumpBehaviour _jumpBehaviour;
    
    [SerializeField] private List<Transform>  _freeAttackPlace;
    private List<Transform> _occupiedAttackPlace;
    
    protected override void StartGame()
    {
        _occupiedAttackPlace=new List<Transform>(_freeAttackPlace.Count);
        _jumpBehaviour = GetComponent<JumpBehaviour>();
        _spriteRenderer=GetComponent<SpriteRenderer>();
        //_rigidBody = GetComponent<Rigidbody2D>();
        _fight = GetComponent<FightingCombo>();
        _transform = transform;
        //_spriteRenderer.receiveShadows = true;
        //_spriteRenderer.shadowCastingMode = ShadowCastingMode.On;
    }
    public void Tick()
    {
        if (_jumpBehaviour.JumpState) return;
        if (IsBlock()) return;
        if (Timer()) return;
        Walk();
        Idle();
    }

    public Transform GetAttackPlace()
    {
        if(_freeAttackPlace.Count==0) return null;
        _occupiedAttackPlace.Add(_freeAttackPlace[0]);
        _freeAttackPlace.RemoveAt(0);
        return _occupiedAttackPlace[0];
    }

    public void FreeAttackPlace(Transform attackPlace)
    {
        _freeAttackPlace.Add(attackPlace);
        _occupiedAttackPlace.Remove(attackPlace);
    }

    public void JumpHit()
    {
        if (_jumpBehaviour.JumpState == false) return;
        AnimaBeh.PlayAnim(AnimationsType.jumpHit);
    }

    private void Walk()
    {
        if (_fight.input == null)// && time <=0)
        {
            float _localScale = transform.localScale.x;

            // float hor=Input.GetAxis("Horizontal");
            // float ver = Input.GetAxis("Vertical");
            if (joy.go)
            {
                // if (hor > 0)
                // {
                //     if (_spriteRenderer.flipX)
                //     {
                //         _spriteRenderer.flipX = false;
                //         attackPos.x =attackPosX;
                //     }
                //
                //     // if (_localScale <= 0)
                //     // {
                //     //     transform.localScale = new Vector3(1, 1, 1);
                //     //     attackPos.x =attackPosX;
                //     //     //data.attackPos.x = attackPosX;
                //     // }
                // }
                // if (hor < 0)
                // {
                //     if (_spriteRenderer.flipX == false)
                //     {
                //         _spriteRenderer.flipX = true;
                //         attackPos.x =attackPosX*-1;
                //     }
                //     // if (_localScale >= 0)
                //     // {
                //     //     transform.localScale = new Vector3(-1, 1, 1);
                //     //     attackPos.x =attackPosX* -1;
                //     //     //data.attackPos.x = attackPosX * -1;
                //     // }
                // }
                //Vector3 direction=new Vector3(hor,0,ver).normalized;
                
                _transform.localScale = new Vector3(Mathf.Sign(joy.posDirection.x),_transform.localScale.y,
                    _transform.localScale.z);
                _rigidBody.MovePosition(_transform.position + joy.posDirection.normalized * (speed * Time.fixedDeltaTime));
                AnimaBeh.PlayAnim(AnimationsType.walk);
            }
            else
            {
                AnimaBeh.PlayAnim(AnimationsType.idle);
            }

            //rigid.MovePosition(transform.position+joy.posDirection* speed * Time.fixedDeltaTime);
        }
    }

    // protected override void TakeDamage(int damage = 1)
    // {
    //     base.TakeDamage();
    //     //_fight.timer = time;
    // }

    // public void UpperCut()
    // {
    //     _uppercutTrig = true;
    // }

    public override void AttackEnemy(int damage = 1)
    {
        if (!_takeDamage)
        {
            enemy = Physics2D.OverlapBoxAll( _attackPos.position, Data.attackSize,0, Data.whoisEnemy);
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i])
                {
                    _HitScore.UpdateHitCount(enemy[i].transform.position);
                    enemy[i].GetComponent<Actor>().TakeDamage(damage);
                }
            }
           
        }
    }

    public void SetBlock(bool state)
    {
        _blockTrigger = state;
    }

    public bool IsBlock()
    {
        if (_blockTrigger)
        {
            AnimaBeh.PlayAnim(AnimationsType.block);
            if (_blockTimeNow > 0)
            {
                _blockTimeNow-=Time.deltaTime;
            }
            return true;
        }
        else
        {
            //_blockTimeNow = 0;
            _blockTimeNow = _blockTime;
            return false;
        }
    }
    // public void Jump()
    // {
    //    //if (_isJump) return;
    //     _isJump = true;
    //     //_startJumpY = transform.position.y;
    //     _rigidBody.AddForce(Vector2.up*_jumpForce,ForceMode2D.Impulse);
    //     _rigidBody.gravityScale = 1;
    // }
    private void Idle()
    {
        if (_fight.input == null &&joy.go==false)
        {
            AnimaBeh.PlayAnim(AnimationsType.idle);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireCube(_attackPos.position,Data.attackSize);
    }
    
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red; 
    //    
    // }
}

// public struct BluePos
// {
//     public Transform _attackPlacePos;
//     private bool _attackPlace;
// }


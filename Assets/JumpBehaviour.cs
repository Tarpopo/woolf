using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    public bool JumpState { get; private set; }
    private Vector3 _startJumpPosition;
    private Transform _transform;
    private Rigidbody2D _rigidBody;
    private LayerMask _startLayer;
    private Joystick _joystick;
    private AnimationBehavior _animationBehavior;
    
    private void Start()
    {
        //_actorTransform = transform;
        _animationBehavior = GetComponent<AnimationBehavior>();
        _transform = transform;
        _startLayer = gameObject.layer;
        //_actorCollider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _joystick = GetComponent<Joystick>();
    }

    private void FixedUpdate()
    {
        if (JumpState)
        {
            if (_transform.position.y < _startJumpPosition.y)
            {
                _rigidBody.velocity=Vector2.zero;
                gameObject.layer = _startLayer;
                _rigidBody.gravityScale = 0;
                JumpState = false;
            }
        }
    }
    
    public void Jump()
    {
        if (JumpState) return;
        _animationBehavior.PlayAnim(AnimationsType.jump);
        _startJumpPosition = _transform.position;
        gameObject.layer = 2;
        //_boxColliderTransform.position = _startTransformJump.position;
        JumpState = true;
        var direction = Vector3.up;
        if (_joystick)
        {
            direction = _joystick.go == false ? Vector3.up : new Vector3(Mathf.Sign(_joystick.posDirection.x)*0.5f,0.7f)*1.2f;
        }
        _rigidBody.AddForce(direction * _jumpForce,ForceMode2D.Impulse);
        _rigidBody.gravityScale = 1;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (_isJump == false) return;
    //     gameObject.layer = _startLayer;
    //     _rigidBody.gravityScale = 0;
    //     _isJump = false;
    // }
}

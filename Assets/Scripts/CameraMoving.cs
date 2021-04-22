using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Camera _camera;
    [SerializeField]
    private Vector2 _threshold;
    private Vector2 _distance;
    public float _speed;
    public Transform _playerPosition;
    public Player _playerScript;
    
    private void Start()
    {
        _distance = CalculateThreshold();
        _camera=Camera.main;
    }

    private void LateUpdate()
    {
        
        Vector3 newPosition = transform.position;
        float differenceX = Vector2.Distance(transform.position.x*Vector2.right, _playerPosition.position.x*Vector2.right);
        float differenceY = Vector2.Distance(transform.position.y*Vector2.up, _playerPosition.position.y*Vector2.up);
        
        if (Mathf.Abs(differenceX) >= _distance.x)
        {
            newPosition.x = Mathf.Round(_playerPosition.position.x);
        }
        
        if (Mathf.Abs(differenceY) >= _distance.y)
        {
            newPosition.y = Mathf.Round(_playerPosition.position.y);
        }

        float moveSpeed = _playerScript.speed >= _speed ? _playerScript.speed : _speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }
    
    private Vector2 CalculateThreshold()
    {
        if (_camera == null) _camera = Camera.main;
        Rect aspect = _camera.pixelRect;
        var orthographicSize = _camera.orthographicSize;
        
        Vector2 t=new Vector2(orthographicSize*2*aspect.width/aspect.height,orthographicSize*2);
        t -= _threshold;
        return t;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.green;
        Vector2 thresholdBox=CalculateThreshold();
        Gizmos.DrawWireCube(transform.position,new Vector3(thresholdBox.x,thresholdBox.y,1));
    }
}

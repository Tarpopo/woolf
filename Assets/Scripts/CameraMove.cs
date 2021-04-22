using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
   public GameObject player;
   public Vector2 followOffset;
   private Vector2 treshold;
   public float speed;
   private Player _player;
 
   private void Start()
   {
      treshold = CalculateThreshold();
      _player = player.GetComponent<Player>();
   }

   private void FixedUpdate()
   {
      Vector2 follow = player.transform.position;
      float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
      float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

      Vector3 newPosition = transform.position;
      if (Mathf.Abs(xDifference) >= treshold.x)
      {
         newPosition.x = follow.x;
      }
      if (Mathf.Abs(yDifference) >= treshold.y)
      {
         newPosition.y = follow.y;
      }
      //float moveSpeed = _player.speed > speed ? _player.speed : speed;
      transform.position = Vector3.MoveTowards(transform.position, newPosition, _player.speed * Time.deltaTime);
   }

   private Vector3 CalculateThreshold()
   {
      Rect aspect = Camera.main.pixelRect;
		Vector2 t=new Vector2(Camera.main.orthographicSize*2*aspect.width/aspect.height,Camera.main.orthographicSize*2);
      t.x -= followOffset.x;
      t.y -= followOffset.y;
      return t;
   }

   private void OnDrawGizmos()
   {
      Gizmos.color=Color.red;
      Vector2 border = CalculateThreshold();
      Gizmos.DrawWireCube(transform.position,new Vector3(border.x,border.y,-10));
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DataActor",menuName ="Data/DataActor")]
public class DataActor : ScriptableObject
{
    
    [Header("Attack Zone")]
    public Vector2 attackSize;
    public LayerMask whoisEnemy;
    
    [Header("Animations")]
    public AnimaClips[] clips;
    public RuntimeAnimatorController anim_controller;
    
    [Header("Health")]
    public int Health;

    [Header("Air Zone")]
    public Vector2 sizeCube;
    public Vector2 posCube;

    [Header("Sounds")] 
    public SimpleSound TakeDamage;
    public SimpleSound Hit;
    public SimpleSound Step;
   
}

[System.Serializable]
public struct AnimaClips
{
    public AnimationsType type;
    //public float length;
    public AnimationClip _clip;
}
 
 public enum AnimationsType
 {
     idle=0,
     run,
     jump,
     walk,
     hit,
     takeDamage,
     block,
     hitBack,
     jumpHit
 }
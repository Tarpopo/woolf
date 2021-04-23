using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DefaultNamespace;
using UnityEngine;

public class AnimationBehavior : MonoBehaviour
{
    private Actor _actor;
    public AnimaClips[] clips;
    public Animator _animator;
   // public GameObject animatorChild;
    private void OnEnable()
    {
        _actor = GetComponent<Actor>();
        //clips = _actor.data.clips;
        clips=new AnimaClips[10];
        for (int i = 0; i <_actor.Data.clips.Length; i++)
        {
            clips[(int)_actor.Data.clips[i].type]=_actor.Data.clips[i];
            //clips.Insert((int)_actor.data.clips[i].type,_actor.data.clips[i]);
        }
        _animator = GetComponent<Animator>();
        if (_actor.Data.anim_controller) _animator.runtimeAnimatorController = _actor.Data.anim_controller;
        _actor.AnimaBeh = this;
    }
    public void PlayAnim(AnimationsType type)
    {
        if (_animator)
        {
            _animator.Play(clips[(int)type]._clip.name);
        }
    }

}

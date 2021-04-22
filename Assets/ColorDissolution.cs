using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorDissolution<T> :ITick where T : Graphic
{
   
   [SerializeField]private T _component;
   private float _currentTime;
   private Action _action;
   public ColorDissolution(T component)
   {
      _component = component;
   }
   
   public void Tick()
   {
      if(_currentTime>0) DissolutionColor();
   }
   
   private void DissolutionColor()
   {
      _currentTime -= Time.deltaTime;
      if (_currentTime <= 0)
      {
         _action?.Invoke();
         ManagerUpdate.RemoveFrom(this);
      }
      _component.color = Color.Lerp( Color.clear, _component.color, _currentTime);
   }

   public void StartDissolution(float time, Action action = null)
   {
      if (_currentTime <=0)
      {
         _action = action;
         _currentTime = time;
         _component.color=Color.white;
         ManagerUpdate.AddTo(this);
      }
      _currentTime = time;
   }
   
}

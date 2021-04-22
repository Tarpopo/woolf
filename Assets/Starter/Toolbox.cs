﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DefaultNamespace;
using UnityEngine;

public class Toolbox : Singleton<Toolbox>
{
   private Dictionary<Type,object>data =new Dictionary<Type, object>();

   public static void Add(object obj)
   {
      var add = obj;
       var manager = obj as ManagerBase;
       if (manager != null)
          add = Instantiate(manager);
      else return;
      //print(obj.GetType());
      Instance.data.Add(obj.GetType(), add);
      
      if (add is IAwake)
      {
         (add as IAwake).OnAwake();
      }
   }
   public static void UpdateAllAwake()
   {
      foreach (var manager in Toolbox.Instance.data)
      {
         var obj = manager.Value as IAwake;
         obj?.OnAwake();
      }
   }
   public static T Get<T>()
   {
      object resolve;
      Instance.data.TryGetValue(typeof(T), out resolve);
      return (T) resolve;
   }

   public void ClearScene()
   {
      
   }


}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public List<ManagerBase>managers=new List<ManagerBase>();

    private void Awake()
    {
        if (Toolbox.Get<ManagerPool>() != null)
        {
            Toolbox.UpdateAllAwake();
            return;
        }
        
        foreach (var managerBase in managers )
        {
            Toolbox.Add(managerBase); 
        }
    }
}

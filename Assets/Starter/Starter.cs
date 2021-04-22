using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public List<ManagerBase>managers=new List<ManagerBase>();

    private void Awake()
    {
        foreach (var managerBase in managers )
        {
            Toolbox.Add(managerBase); 
        }
    }
}

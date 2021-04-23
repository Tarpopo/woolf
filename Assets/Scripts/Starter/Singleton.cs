﻿using UnityEngine;

namespace DefaultNamespace
{
    public class Singleton <T>: MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        var singleton=new GameObject("[Singleton]"+typeof(T));
                        //singleton.transform.parent = GameObject.Find("[Setup]").transform;
                        _instance=singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                    }
                }

                return _instance;
            }
        }
    }
}
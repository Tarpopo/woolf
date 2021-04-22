
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Pool
    {
        private Transform parent;
        private List<IPoolable> pooledObjects=new List<IPoolable>();
        public Pool(int size = 0)
        {
            pooledObjects=new List<IPoolable>(size);
        }

        public void SetParent(Transform parent)
        {
            this.parent = parent;
        }
    }
}
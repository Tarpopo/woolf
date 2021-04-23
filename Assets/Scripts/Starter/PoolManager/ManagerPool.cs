using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ManagerPool : MonoBehaviour
{
    private Dictionary<int,Pool>pools=new Dictionary<int, Pool>();

    public Pool AddPool(PoolType id,int size=0,bool reparent=true)
    {
        Pool pool;
        if (pools.TryGetValue((int) id, out pool)==false)
        {
            pool=new Pool(size);
            pools.Add((int)id,pool);
            if (reparent)
            {
                var poolsGo = GameObject.Find("[POOLS]") ?? new GameObject("[POOLS]");
                var poolGo = new GameObject("pool:" + id);
                poolGo.transform.SetParent(poolsGo.transform);
                pool.SetParent(poolsGo.transform);
            }
        }

        return pool;
    }
}

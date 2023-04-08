using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supporting
{
    public class ObjectPool
    {
        private List<GameObject> PooledObjects;

        public ObjectPool(int amount, GameObject prefabOjectToPool)
        {
            PooledObjects = new List<GameObject>();
            GameObject tmp;
            for(int i = 0; i < amount; i++)
            {
                tmp = GameObject.Instantiate(prefabOjectToPool);
                tmp.SetActive(false);
                PooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject()
        {
            for(int i = 0; i < PooledObjects.Count; i++)
            {
                if(!PooledObjects[i].activeInHierarchy)
                {
                    return PooledObjects[i];
                }
            }

            return null;
        }

    }
}


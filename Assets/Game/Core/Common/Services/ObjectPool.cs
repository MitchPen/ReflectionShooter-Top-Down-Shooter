using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Core.Common.Services
{
    public class ObjectPool : MonoBehaviour
    {
        private Dictionary<String,PoolContainer> _labeledPool = new Dictionary<string, PoolContainer>();
        
        public void CreatePool(String poolLabel, GameObject obj, int poolLenght)
        {
            var newPool = new Queue<GameObject>();
            
            var poolParent = new GameObject().transform;
            poolParent.position = transform.position;
            poolParent.SetParent(transform);
            poolParent.name = poolLabel;
            
            for (int i = 0; i < poolLenght; i++)
            {
                var newItem = Instantiate(obj, transform);
                newItem.SetActive(false);
                newItem.transform.SetParent(poolParent.transform);
                newPool.Enqueue(newItem);
            }
            _labeledPool.Add(poolLabel,new PoolContainer(newPool, poolParent));
        }
        
        public T GetFromPoolWithType<T>(string label) where T: Object
        {
            return _labeledPool.ContainsKey(label) ? GetPooledObject<T>(_labeledPool[label].Pool) : null;
        }

        public GameObject GetFromPool(string label)
        {
            return _labeledPool.ContainsKey(label) ? GetPooledObject(_labeledPool[label].Pool) : null;
        }

        private GameObject GetPooledObject(Queue<GameObject> pool)
        {
            if (pool.Count == 0) return null;
            var objectFromPool = pool.Dequeue();
            return objectFromPool;
        }
        
        private T GetPooledObject<T>(Queue<GameObject> pool) where T : Object
        {
            if (pool.Count == 0) return null;
            var objectFromPool = pool.Dequeue();
            objectFromPool.TryGetComponent<T>(out T result);
            if (result == null)
            {
                pool.Enqueue(objectFromPool);
                return null;
            }
            return result;
        }

        public void ReturnToPool(String label, GameObject obj)
        {
            if (_labeledPool.ContainsKey(label))
            {
                obj.transform.SetParent(_labeledPool[label].Parent);
                _labeledPool[label].Pool.Enqueue(obj);
            }
        }
    }

    public struct PoolContainer
    {
        public Transform Parent;
        public Queue<GameObject> Pool;

        public PoolContainer(Queue<GameObject> Pool, Transform Parent)
        {
            this.Pool = Pool;
            this.Parent = Parent;
        }
    }
}
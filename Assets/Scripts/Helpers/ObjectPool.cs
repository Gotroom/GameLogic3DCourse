using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Ermolaev_3D
{
    public class ObjectPool
    {
        private List<PoolableObject> _pool;
        private readonly GameObject _rootObject;
        private readonly PoolableObject _baseObject;

        public ObjectPool(PoolableObject obj, int count)
        {
            _baseObject = obj;
            _pool = new List<PoolableObject>();
            _rootObject = new GameObject(obj.Name);
            for (int i = 0; i < count; i++)
            {
                var poolableObject = AddToPool();
                poolableObject.PlaceToPool += OnPlacingToPool;
            }
        }

        private PoolableObject AddToPool()
        {
            var poolObject = GameObject.Instantiate(_baseObject, Vector3.zero, Quaternion.identity, _rootObject.transform);
            poolObject.gameObject.SetActive(false);
            _pool.Add(poolObject);
            return poolObject;
        }

        public BaseObjectModel GetFromPool()
        {
            var obj = _pool.Find(e => !e.gameObject.activeInHierarchy);
            if (obj == null)
            {
                return AddToPool();
            }
            return _pool.Find(e => !e.gameObject.activeInHierarchy);
        }

        private void OnPlacingToPool(PoolableObject obj)
        {
            var poolObject = _pool.Find(e => e == obj);
            if (poolObject)
            {
                poolObject.gameObject.SetActive(false);
            }
        }

    }
}


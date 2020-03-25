using UnityEngine;
using System;


namespace Ermolaev_3D
{
    public class PoolableObject : BaseObjectModel
    {
        public event Action<PoolableObject> PlaceToPool;

        protected virtual void ReturnToPool()
        {
            PlaceToPool.Invoke(this);
        }
    }
}


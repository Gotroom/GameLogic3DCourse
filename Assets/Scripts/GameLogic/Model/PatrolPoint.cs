using System;
using UnityEngine;

namespace Ermolaev_3D
{
    public class PatrolPoint : MonoBehaviour
    {
        [SerializeField] private PatrolPointObject _prefab;
        private BotPath _rootWayPoint;

        public void InstantiateObj(Vector3 pos)
        {
            if (!_rootWayPoint)
            {
                _rootWayPoint = new GameObject("WayPoint").AddComponent<BotPath>();
            }

            if (_prefab != null)
            {
                Instantiate(_prefab, pos, Quaternion.identity, _rootWayPoint.transform);
            }
        }

    }
}
using UnityEngine;

namespace Ermolaev_3D
{
    public readonly struct CollisionInfo
    {
        private readonly Vector3 _dir;
        private readonly float _damage;

        public CollisionInfo(float damage, Vector3 dir = default)
        {
            _damage = damage;
            _dir = dir;
        }

        public Vector3 Direction => _dir;

        public float Damage => _damage;
    }
}

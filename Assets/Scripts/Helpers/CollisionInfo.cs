using UnityEngine;

namespace Ermolaev_3D
{
    public readonly struct CollisionInfo
    {
        private readonly Vector3 _dir;
        private readonly float _damage;

        private readonly ContactPoint _contact;
        private readonly Transform _objCollision;

        public CollisionInfo(float damage, ContactPoint point, Transform objCollision, Vector3 dir = default)
        {
            _damage = damage;
            _dir = dir;
            _contact = point;
            _objCollision = objCollision;
        }

        public Vector3 Direction => _dir;

        public float Damage => _damage;

        public ContactPoint Contact => _contact;

        public Transform ObjCollision => _objCollision;

    }
}

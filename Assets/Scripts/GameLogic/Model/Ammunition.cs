using UnityEngine;

namespace Ermolaev_3D
{
    public abstract class Ammunition : PoolableObject
    {
        [SerializeField] protected float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;
        protected float _curDamage; // todo доделать свой урон
        private float _lossOfDamageAtTime = 0.2f;

        public AmmunitionType Type = AmmunitionType.Bullet;

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        private void Start()
        {
            Invoke(nameof(ReturnToPool), _timeToDestruct);
            InvokeRepeating(nameof(LossOfDamage), 0, 1);
        }

        public virtual void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(dir);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        protected override void ReturnToPool()
        {
            base.ReturnToPool();
            CancelInvoke(nameof(LossOfDamage));
        }
    }
}

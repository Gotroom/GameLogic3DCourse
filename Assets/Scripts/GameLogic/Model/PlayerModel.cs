using UnityEngine;
using System;

namespace Ermolaev_3D
{
    public class PlayerModel : BaseObjectModel, IDamagable
    {
        public event Action<float> HealthChanged;

        [SerializeField] private float _health = 100.0f;

        public float Health
        {
            get { return _health; }
        }

        public void SetDamage(CollisionInfo info)
        {
            if (_health > 0.0f)
            {
                _health -= info.Damage;
                HealthChanged.Invoke(_health);
            }

            if (_health <= 0.0f)
            {
                HealthChanged.Invoke(0.0f);
            }
        }
    }
}
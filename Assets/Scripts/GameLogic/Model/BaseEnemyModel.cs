using UnityEngine;
using System;

namespace Ermolaev_3D
{
    public abstract class BaseEnemyModel : BaseObjectModel, IDamagable, ISelectable
    {
        public event Action<BaseEnemyModel> Killed = delegate { };

        public float HealthPoints = 100;
        public float TimeToDestroy = 10.0f;

        private bool _isDead = false;

        public virtual string GetMessage()
        {
            return gameObject.name;
        }

        public virtual void SetDamage(CollisionInfo info)
        {
            if (_isDead)
            {
                return;
            }

            if (HealthPoints > 0)
            {
                HealthPoints -= info.Damage;
            }

            if (HealthPoints <= 0)
            {
                Destroy(gameObject, TimeToDestroy);
                Killed.Invoke(this);
                _isDead = true;
            }
        }
    }
}


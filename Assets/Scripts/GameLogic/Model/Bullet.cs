﻿using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class Bullet : Ammunition
    {
        private void OnCollisionEnter(Collision collision) // todo своя обработка полета и получения урона
        {
            // дописать доп урон
            var setDamage = collision.gameObject.GetComponent<IDamagable>();

            if (setDamage != null)
            {
                setDamage.SetDamage(new CollisionInfo(_curDamage, collision.contacts[0], collision.transform, Rigidbody.velocity));
            }
            Invoke(nameof(ReturnToPool), _timeToDestruct);
        }
    }
}

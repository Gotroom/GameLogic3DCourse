﻿using UnityEngine;
using System.Collections;


namespace Ermolaev_3D
{
    public class Arrow : Ammunition
    {
        private float _destroyTimeAfterCollision = 0.2f;

        private void OnCollisionEnter(Collision collision)
        {

            if (!collision.gameObject.CompareTag(TagManager.ARROW))
            {
                Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                var setDamage = collision.gameObject.GetComponent<IDamagable>();
                if (setDamage != null)
                {
                    setDamage.SetDamage(new CollisionInfo(_curDamage, collision.contacts[0], collision.transform, Rigidbody.velocity));
                }
                Invoke(nameof(ReturnToPool), _destroyTimeAfterCollision);
            }
        }

        public override void AddForce(Vector3 dir)
        {
            base.AddForce(dir);
            Rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
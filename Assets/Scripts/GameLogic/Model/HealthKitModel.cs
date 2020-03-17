using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class HealthKitModel : EffectorModel
    {
        public float HealingPower = 10.0f;

        public override void ApplyEffect(Collision collision)
        {
            var damagable = collision.gameObject.GetComponent<IDamagable>();

            if (damagable != null)
            {
                damagable.SetDamage(new CollisionInfo(-HealingPower, collision.contacts[0], collision.transform, Rigidbody.velocity));
                Destroy(gameObject);
            }
        }
    }
}
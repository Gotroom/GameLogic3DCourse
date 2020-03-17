using UnityEngine;
using System.Collections;


namespace Ermolaev_3D
{
    public class MineModel : EffectorModel
    {
        [SerializeField] private ParticleSystem _explosionParticles;
        [SerializeField] private float _explosionRadius = 10.0f;
        [SerializeField] private float _damage = 10.0f;

        private float _destroyTimeAfterCollision = 0.2f;

        public override void ApplyEffect(Collision collision)
        {
            Explode(collision);
        }

        private void Explode(Collision collision)
        {
            var colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
            foreach (var collider in colliders)
            {
                var setDamage = collision.gameObject.GetComponent<IDamagable>();
                if (setDamage != null)
                {
                    setDamage.SetDamage(new CollisionInfo(_damage, collision.contacts[0], null, Rigidbody.velocity));
                    Instantiate(_explosionParticles, transform.position, transform.rotation);
                    Destroy(gameObject, _destroyTimeAfterCollision);
                }
            }
        }
    }
}
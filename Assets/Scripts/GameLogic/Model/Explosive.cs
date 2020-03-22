using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class Explosive : Ammunition
    {
        [SerializeField] private float _explosionRadius = 10.0f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ParticleSystem _explosionParticles;

        private float _destroyTimeAfterCollision = 0.2f;

        private void OnCollisionEnter(Collision collision)
        {
            Explode(collision);

            Invoke(nameof(ReturnToPool), _timeToDestruct);
        }

        private void Explode(Collision collision)
        {
            var colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
            foreach (var collider in colliders)
            {
                var setDamage = collision.gameObject.GetComponent<IDamagable>();
                if (setDamage != null)
                {
                    setDamage.SetDamage(new CollisionInfo(_curDamage, collision.contacts[0], null, Rigidbody.velocity));
                }
            }
            Instantiate(_explosionParticles, transform.position, transform.rotation);
        }
    }
}

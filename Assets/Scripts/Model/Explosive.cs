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
            Explode();

            DestroyAmmunition(_destroyTimeAfterCollision);
        }

        private void Explode()
        {
            var collisions = Physics.OverlapSphere(transform.position, _explosionRadius);
            foreach (var collision in collisions)
            {
                var setDamage = collision.gameObject.GetComponent<IDamagable>();
                if (setDamage != null)
                {
                    setDamage.SetDamage(new CollisionInfo(_curDamage, Rigidbody.velocity));
                }
            }
            Instantiate(_explosionParticles, transform);
        }
    }
}

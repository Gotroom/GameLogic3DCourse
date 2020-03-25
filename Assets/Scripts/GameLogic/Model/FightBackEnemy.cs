using UnityEngine;


namespace Ermolaev_3D
{
    public class FightBackEnemy : BaseEnemyModel
    {
        [SerializeField] private Transform _weaponPosition;
        [SerializeField] private Ammunition _ammunition;

        [SerializeField] private float _force = 999.0f;

        public override string GetMessage()
        {
            return base.GetMessage() + $": {HealthPoints}";
        }

        public override void SetDamage(CollisionInfo info)
        {
            base.SetDamage(info);
            transform.LookAt(-info.Direction);
            var projectile = Instantiate(_ammunition, _weaponPosition.position, _weaponPosition.rotation);
            if (projectile)
                projectile.AddForce(-info.Direction * _force);
        }
    }
}


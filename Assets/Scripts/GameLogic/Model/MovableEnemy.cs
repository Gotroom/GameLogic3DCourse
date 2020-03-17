using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class MovableEnemy : BaseEnemyController
    {
        public float PullBackForce = 0.1f;

        public override string GetMessage()
        {
            return base.GetMessage() + $": {HealthPoints}";
        }

        public override void SetDamage(CollisionInfo info)
        {
            base.SetDamage(info);
            transform.position += info.Direction * PullBackForce;
        }
    }
}


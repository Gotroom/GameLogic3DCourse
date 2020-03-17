using UnityEngine;


namespace Ermolaev_3D
{
    public abstract class EffectorModel : BaseObjectModel, IEffect
    {

        private void OnCollisionEnter(Collision collision)
        {
            ApplyEffect(collision);
        }

        public abstract void ApplyEffect(Collision collision);
    }
}


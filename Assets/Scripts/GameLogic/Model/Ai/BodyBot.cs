using System;

namespace Ermolaev_3D
{
    public sealed class BodyBot : BaseObjectModel, IDamagable, ISelectable
    {
        public event Action<CollisionInfo> OnApplyDamageChange;
        public event Func<string> WasSelected;

        public string GetMessage()
        {
            return WasSelected?.Invoke();
        }

        public void SetDamage(CollisionInfo info)
        {
            OnApplyDamageChange?.Invoke(info);
        }
    }
}

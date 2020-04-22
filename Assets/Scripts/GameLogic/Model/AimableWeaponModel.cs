using UnityEngine;


namespace Ermolaev_3D
{
    public abstract class AimableWeaponModel : WeaponModel
    {
        public abstract void TakeAim();
        public abstract void CancelTakingAim();
        public abstract void ProcessScrollWheel(bool isUp);
    }
}
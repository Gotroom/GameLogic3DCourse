using System.Linq;

namespace Ermolaev_3D
{
	public sealed class Gun : WeaponModel
	{
        public override void Fire()
		{
			if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;

            UseAmmunitionFromPool();

			Clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechargeTime); // todo таймер контроллер 
		}
    }
}
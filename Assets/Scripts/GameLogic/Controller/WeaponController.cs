namespace Ermolaev_3D
{
    public sealed class WeaponController : BaseController
    {
        private WeaponModel _weapon;

        public override void On(params BaseObjectModel[] weapon)
        {
            if (IsActive) return;
            if (weapon.Length > 0) _weapon = weapon[0] as WeaponModel;
            if (_weapon == null) return;
            base.On(_weapon);
            _weapon.Visible = true;
            UiInterface.WeaponUiText.SetActive(true);
            UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _weapon.Visible = false;
            _weapon = null;
            UiInterface.WeaponUiText.SetActive(false);
        }

        public void Fire()
        {
            _weapon.Fire();
            UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
        }

        public void Draw()
        {
            if (_weapon is DrawableWeaponModel weapon)
            {
                weapon.Draw();
            }
        }

        public void Release()
        {
            if (_weapon is DrawableWeaponModel weapon)
            {
                weapon.Release();
            }
        }

        public void TakeAim()
        {
            if (_weapon is AimableWeaponModel weapon)
            {
                ServiceLocator.Resolve<AimController>().ShowAiming(weapon);
            }
        }

        public void CancelTakingAim()
        {
            if (_weapon is AimableWeaponModel weapon)
            {
                ServiceLocator.Resolve<AimController>().HideAiming(weapon);
            }
        }

        public void ProcessWheelScroll(bool isUp)
        {
            if (_weapon is AimableWeaponModel weapon)
            {
                ServiceLocator.Resolve<AimController>().ProcessScrollWheel(weapon, isUp);
            }
        }

        public void ReloadClip()
        {
            if (_weapon == null) return;
            _weapon.ReloadClip();
            UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
        }
    }
}

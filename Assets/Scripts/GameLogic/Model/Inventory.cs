using UnityEngine;
using System.Collections.Generic;


namespace Ermolaev_3D
{
	public sealed class Inventory : IInitializable
	{
		private List<WeaponModel> _weapons = new List<WeaponModel>(5);

		public List<WeaponModel> Weapons => _weapons;

		public FlashLightModel FlashLight { get; private set; }

        private int _currentWeaponIndex = 0;

		public void Initialization()
		{
			_weapons.InsertRange(0, ServiceLocatorMonoBehaviour.GetService<CharacterController>().
				GetComponentsInChildren<WeaponModel>());

			foreach (var weapon in Weapons)
			{
				weapon.Visible = false;
			}

			FlashLight = Object.FindObjectOfType<FlashLightModel>();
			FlashLight.Switch(FlashLightActiveType.Off);
		}

		public WeaponModel GetWeapon(int index)
        {
            if (index < _weapons.Count && index >= 0)
            {
                _currentWeaponIndex = index;
                return _weapons[index];
            }
            return null;
        }

        public WeaponModel GetNextWeapon()
        {
            if (_currentWeaponIndex + 1 >= _weapons.Count)
            {
                _currentWeaponIndex = 0;
            }
            else
            {
                _currentWeaponIndex++;
            }
            return _weapons[_currentWeaponIndex];
        }

        public WeaponModel GetPreviousWeapon()
        {
            if (_currentWeaponIndex - 1 < 0)
            {
                _currentWeaponIndex = _weapons.Count - 1;
            }
            else
            {
                _currentWeaponIndex--;
            }
            return _weapons[_currentWeaponIndex];
        }

        public void RemoveWeapon(WeaponModel weapon)
        {
            _weapons.Remove(weapon);
        }
	}
}
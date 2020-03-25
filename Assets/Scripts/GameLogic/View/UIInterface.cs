using UnityEngine;

namespace Ermolaev_3D
{
	public sealed class UIInterface
	{
        private FlashLightTextUI _flashLightUiText;

        public FlashLightTextUI LightUiText
        {
            get
            {
                if (!_flashLightUiText)
                    _flashLightUiText = Object.FindObjectOfType<FlashLightTextUI>();
                return _flashLightUiText;
            }
        }

        private FlashLightBarUI _flashLightUiBar;

        public FlashLightBarUI FlashLightUiBar
        {
            get
            {
                if (!_flashLightUiBar)
                    _flashLightUiBar = Object.FindObjectOfType<FlashLightBarUI>();
                return _flashLightUiBar;
            }
        }

        private WeaponUIText _weaponUiText;

		public WeaponUIText WeaponUiText
		{
			get
			{
				if (!_weaponUiText)
					_weaponUiText = Object.FindObjectOfType<WeaponUIText>();
				return _weaponUiText;
			}
		}

		private SelectableMessageUI _selectionObjMessageUi;

		public SelectableMessageUI SelectionObjMessageUi
		{
			get
			{
				if (!_selectionObjMessageUi)
					_selectionObjMessageUi = Object.FindObjectOfType<SelectableMessageUI>();
				return _selectionObjMessageUi;
			}
		}

        private HealthUI _healthUI;

        public HealthUI HealthUI
        {
            get
            {
                if (!_healthUI)
                    _healthUI = Object.FindObjectOfType<HealthUI>();
                return _healthUI;
            }
        }
    }
}
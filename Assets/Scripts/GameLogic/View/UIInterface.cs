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

        private MinimapUI _minimapUI;

        public MinimapUI MinimapUI
        {
            get
            {
                if (!_minimapUI)
                    _minimapUI = Object.FindObjectOfType<MinimapUI>();
                return _minimapUI;
            }
        }

        private RadarUI _radarUI;

        public RadarUI RadarUI
        {
            get
            {
                if (!_radarUI)
                    _radarUI = Object.FindObjectOfType<RadarUI>();
                return _radarUI;
            }
        }

        private ViewFinderUI _viewFinderUI;

        public ViewFinderUI ViewFinderUI
        {
            get
            {
                if (!_viewFinderUI)
                    _viewFinderUI = Object.FindObjectOfType<ViewFinderUI>();
                return _viewFinderUI;
            }
        }

        public void SetActiveNonAim(bool value)
        {
            WeaponUiText.SetActive(value);
            SelectionObjMessageUi.SetActive(value);
            HealthUI.SetActive(value);
            MinimapUI.SetActive(value);
            RadarUI.SetActive(value);
        }
    }
}
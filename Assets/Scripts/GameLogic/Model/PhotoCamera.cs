using UnityEngine;
using System.Collections;

namespace Ermolaev_3D
{
    public class PhotoCamera : AimableWeaponModel
    {
        private const float FLASH_TIME = 0.1f;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _viewFinderCamera;

        private RenderTexture _texture;
        private ScreenshotTaker _screenshooter;
        private Light _flash;

        protected override void Awake()
        {
            base.Awake();
            _flash = GetComponentInChildren<Light>();
            _screenshooter = GetComponentInChildren<ScreenshotTaker>();
        }

        public override void Fire()
        {
            if (Clip.CountAmmunition <= 0) return;
            if (_flash)
            {
                _flash.enabled = true;
            }
            Invoke(nameof(FlashExpired), FLASH_TIME);
        }

        public override void TakeAim()
        {
            _mainCamera.enabled = false;
            _texture = _viewFinderCamera.targetTexture;
            _viewFinderCamera.targetTexture = null;
        }

        public override void CancelTakingAim()
        {
            _mainCamera.enabled = true;
            _viewFinderCamera.targetTexture = _texture;
        }

        private void FlashExpired()
        {
            if (_flash)
            {
                _flash.enabled = false;
            }
            if (_screenshooter)
            {
                _screenshooter.TakeScreenShot();
            }
            Clip.CountAmmunition--;
        }
    }
}
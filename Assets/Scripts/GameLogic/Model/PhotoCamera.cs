using UnityEngine;
using System.Collections;

namespace Ermolaev_3D
{
    public class PhotoCamera : AimableWeaponModel
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _viewFinderCamera;

        private RenderTexture tx;

        public override void Fire()
        {
            if (Clip.CountAmmunition <= 0) return;
            GetComponentInChildren<ScreenshotTaker>().TakeScreenShot();
            Clip.CountAmmunition--;
        }

        public override void TakeAim()
        {
            _mainCamera.enabled = false;
            tx = _viewFinderCamera.targetTexture;
            _viewFinderCamera.targetTexture = null;
        }

        public override void CancelTakingAim()
        {
            _mainCamera.enabled = true;
            _viewFinderCamera.targetTexture = tx;
        }
    }
}
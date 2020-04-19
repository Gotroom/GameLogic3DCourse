using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

namespace Ermolaev_3D
{
    public class PhotoCamera : AimableWeaponModel
    {
        private const float FLASH_TIME = 0.1f;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _viewFinderCamera;
        [SerializeField] private GameObject _flashObject;

        private RenderTexture _texture;
        private ScreenshotTaker _screenshooter;
        private Light _flash;
        private PostProcessVolume _prostProcessVolume;
        private DepthOfField _depthOfField;

        //Reference from Helios 44-2 Camera lens
        private enum RANGE
        {
            CLOSE_RANGE = 0,
            MID_RANGE,
            LONG_RANGE,
            EXTREME_LONG_RANGE
        };
        private float[] DEPTH_OF_FIELD_VALUES = { 0.5f, 1.0f, 2.0f, 10.0f };
        private float[] DEPTH_OF_FIELD_DELTA = { 0.1f, 0.3f, 1.0f, 999.0f };
        private RANGE _currentRange; 

        protected override void Awake()
        {
            base.Awake();
            _flash = _flashObject.GetComponent<Light>();
            _screenshooter = GetComponentInChildren<ScreenshotTaker>();
            _prostProcessVolume = _viewFinderCamera.GetComponent<PostProcessVolume>();
            _prostProcessVolume.profile.TryGetSettings(out _depthOfField);

            _rechargeTime = 0.1f;
        }

        public override void Fire()
        {
            if (!_isReady) return;
            _isReady = false;
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

        public override void ProcessScrollWheel(bool isUp)
        {
            if (isUp)
            {
                IncreaseDepthOfField();
            }
            else
            {
                DecreaseDepthOfField();
            }
        }

        private void IncreaseDepthOfField()
        {
            if (_depthOfField)
            {
                var focusDistance = _depthOfField.focusDistance.value;
                if (focusDistance >= DEPTH_OF_FIELD_VALUES[(int)RANGE.EXTREME_LONG_RANGE])
                {
                    _depthOfField.focusDistance.value = DEPTH_OF_FIELD_DELTA[(int)RANGE.EXTREME_LONG_RANGE];
                }
                else if (focusDistance >= DEPTH_OF_FIELD_VALUES[(int)RANGE.LONG_RANGE])
                {
                    _depthOfField.focusDistance.value += DEPTH_OF_FIELD_DELTA[(int)RANGE.LONG_RANGE];
                }
                else if (focusDistance >= DEPTH_OF_FIELD_VALUES[(int)RANGE.MID_RANGE])
                {
                    _depthOfField.focusDistance.value += DEPTH_OF_FIELD_DELTA[(int)RANGE.MID_RANGE];
                }
                else
                {
                    _depthOfField.focusDistance.value += DEPTH_OF_FIELD_DELTA[(int)RANGE.CLOSE_RANGE];
                }
            }
        }

        private void DecreaseDepthOfField()
        {
            if (_depthOfField)
            {
                var focusDistance = _depthOfField.focusDistance.value;
                
                if (focusDistance <= DEPTH_OF_FIELD_VALUES[(int)RANGE.CLOSE_RANGE])
                {
                    _depthOfField.focusDistance.value = DEPTH_OF_FIELD_VALUES[(int)RANGE.CLOSE_RANGE];
                    return;
                }
                else if (focusDistance <= DEPTH_OF_FIELD_VALUES[(int)RANGE.MID_RANGE])
                {
                    _depthOfField.focusDistance.value -= DEPTH_OF_FIELD_DELTA[(int)RANGE.CLOSE_RANGE];
                }
                else if (focusDistance <= DEPTH_OF_FIELD_VALUES[(int)RANGE.LONG_RANGE])
                {
                    _depthOfField.focusDistance.value -= DEPTH_OF_FIELD_DELTA[(int)RANGE.MID_RANGE];
                }
                else if (focusDistance <= DEPTH_OF_FIELD_VALUES[(int)RANGE.EXTREME_LONG_RANGE])
                {
                    _depthOfField.focusDistance.value -= DEPTH_OF_FIELD_DELTA[(int)RANGE.LONG_RANGE];
                }
                if (focusDistance > DEPTH_OF_FIELD_VALUES[(int)RANGE.EXTREME_LONG_RANGE])
                {
                    _depthOfField.focusDistance.value = DEPTH_OF_FIELD_VALUES[(int)RANGE.EXTREME_LONG_RANGE];
                }
            }
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
            Invoke(nameof(ReadyShoot), _rechargeTime);
        }
    }
}
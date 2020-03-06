using System;
using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class FlashLightModel : BaseObjectModel
    {
        [SerializeField] private float _speed = 11;
        [SerializeField] private float _batteryChargeMax = 100;
        private Light _light;
        private Transform _goFollow;
        private Vector3 _vecOffset;
        public float BatteryChargeCurrent { get; private set; }
        public float BatteryChargePercentage => BatteryChargeCurrent / _batteryChargeMax;

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
            _goFollow = Camera.main.transform;
            _vecOffset = Transform.position - _goFollow.position;
            BatteryChargeCurrent = _batteryChargeMax;
        }

        public void Switch(FlashLightActiveType value)
        {
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Transform.position = _goFollow.position + _vecOffset;
                    Transform.rotation = _goFollow.rotation;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void Rotation()
        {
            Transform.position = _goFollow.position + _vecOffset;
            Transform.rotation = Quaternion.Lerp(Transform.rotation,
                _goFollow.rotation, _speed * Time.deltaTime);
        }

        public bool WasteBatteryCharge()
        {
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;
                return true;
            }
            return false;
        }

        public bool AccumulateBatteryCharge()
        {
            if (BatteryChargeCurrent < _batteryChargeMax)
            {
                BatteryChargeCurrent += Time.deltaTime;
                return true;
            }
            return false;
        }

        public bool IsLowBattery()
        {
            return BatteryChargeCurrent <= _batteryChargeMax / 10;
        }
    }
}

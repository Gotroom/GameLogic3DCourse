using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class FlashLightController : BaseController, IExecutable, IInitializable
    {
        private FlashLightModel _flashLightModel;
        private FlashLightUI _flashLightUi;

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUi = Object.FindObjectOfType<FlashLightUI>();
        }

        public override void On()
        {
            if (IsActive) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On();
            _flashLightModel.Switch(FlashLightActiveType.On);
            _flashLightUi.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
        }

        public void Execute()
        {
            if (!IsActive)
            {
                if (!_flashLightModel.AccumulateBatteryCharge())
                {
                    _flashLightUi.SetActive(false);
                }
                else
                {
                    _flashLightUi.Text = _flashLightModel.BatteryChargeCurrent;
                    _flashLightUi.Bar = _flashLightModel.BatteryChargePercentage;
                }
            }
            else
            {
                _flashLightModel.Rotation();
                if (_flashLightModel.WasteBatteryCharge())
                {
                    _flashLightUi.Text = _flashLightModel.BatteryChargeCurrent;
                    _flashLightUi.Bar = _flashLightModel.BatteryChargePercentage;
                }
                else
                {
                    Off();
                }
            }
        }
    }
}

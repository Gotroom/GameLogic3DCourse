using UnityEngine;

namespace Ermolaev_3D
{
    public sealed class FlashLightController : BaseController, IExecutable, IInitializable
    {
        private FlashLightModel _flashLightModel;

        public void Initialization()
        {
            UiInterface.LightUiText.SetActive(false);
            UiInterface.FlashLightUiBar.SetActive(false);
        }

        public override void On(params BaseObjectModel[] flashLight)
        {
            if (IsActive) return;
            if (flashLight.Length > 0) _flashLightModel = flashLight[0] as FlashLightModel;
            if (_flashLightModel == null) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On(_flashLightModel);
            _flashLightModel.Switch(FlashLightActiveType.On);
            UiInterface.LightUiText.SetActive(true);
            UiInterface.FlashLightUiBar.SetActive(true);
            UiInterface.FlashLightUiBar.SetColor(Color.green);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off); ;
            UiInterface.FlashLightUiBar.SetActive(false);
            UiInterface.LightUiText.SetActive(false);
        }

        public void Execute()
        {
            if (!IsActive)
            {
                _flashLightModel?.AccumulateBatteryCharge();
                return;
            }
            if (_flashLightModel.WasteBatteryCharge())
            {
                UiInterface.LightUiText.Text = _flashLightModel.BatteryChargeCurrent;
                UiInterface.FlashLightUiBar.Fill = _flashLightModel.BatteryChargePercentage;
                _flashLightModel.Rotation();

                if (_flashLightModel.IsLowBattery())
                {
                    UiInterface.FlashLightUiBar.SetColor(Color.red);
                }
            }
            else
            {
                Off();
            }
        }
    }
}

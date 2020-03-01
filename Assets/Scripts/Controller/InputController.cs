using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class InputController : BaseController, IExecutable
    {
        private KeyCode _activeFlashLight = KeyCode.F;
        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch();
            }
        }
    }
}

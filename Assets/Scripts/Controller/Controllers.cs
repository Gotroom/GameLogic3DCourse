using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class Controllers : IInitializable
    {
        private readonly IExecutable[] _executeControllers;

        public int Length => _executeControllers.Length;

        public IExecutable this[int index] => _executeControllers[index];

        public Controllers()
        {
            IMovable motor = default;
            if (Application.platform == RuntimePlatform.PS4)
            {
                //
            }
            else
            {
                motor = new MovableObjectModel(
                    ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            }

            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            _executeControllers = new IExecutable[3];

            _executeControllers[0] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[1] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[2] = ServiceLocator.Resolve<InputController>();
        }

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitializable initialization)
                {
                    initialization.Initialization();
                }
            }

            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<PlayerController>().On();
        }
    }
}

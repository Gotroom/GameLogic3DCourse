using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class Controllers : IInitializable
    {
        private readonly IExecutable[] _executeControllers;

        public int Length => _executeControllers.Length;

        public Controllers()
        {
            IMovable motor = new MovableObjectModel(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new AimController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new SelectionController());
            ServiceLocator.SetService(new BotController());
            ServiceLocator.SetService(new SaveLoadController());

            _executeControllers = new IExecutable[5];

            _executeControllers[0] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[1] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[2] = ServiceLocator.Resolve<InputController>();

            _executeControllers[3] = ServiceLocator.Resolve<SelectionController>();

            _executeControllers[4] = ServiceLocator.Resolve<BotController>();
        }

        public IExecutable this[int index] => _executeControllers[index];

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitializable initialization)
                {
                    initialization.Initialization();
                }
            }

            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<SaveLoadController>().Initialization();
            ServiceLocator.Resolve<AimController>().Initialization();
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<SelectionController>().On();
            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<BotController>().On();
        }
    }
}

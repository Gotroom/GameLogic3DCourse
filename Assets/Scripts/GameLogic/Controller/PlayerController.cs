namespace Ermolaev_3D
{
    public class PlayerController : BaseController, IInitializable, IExecutable
    {
        private readonly IMovable _motor;
        private readonly PlayerModel _player;

        public PlayerController(IMovable motor)
        {
            _motor = motor;
            _player = ServiceLocatorMonoBehaviour.GetService<PlayerModel>();
        }

        public void Initialization()
        {
            _player.HealthChanged += OnHealthChanged;
            UiInterface.HealthUI.Text = _player.Health;
        }

        public void Execute()
        {
            if (!IsActive) { return; }
            _motor.Move();
        }

        private void OnHealthChanged(float health)
        {
            if (health <= 0)
            {
                Off();
            }
            UiInterface.HealthUI.Text = health;
        }
    }
}
namespace Ermolaev_3D
{
    public class PlayerController : BaseController, IExecutable
    {
        private readonly IMovable _motor;

        public PlayerController(IMovable motor)
        {
            _motor = motor;
        }

        public void Execute()
        {
            if (!IsActive) { return; }
            _motor.Move();
        }
    }
}
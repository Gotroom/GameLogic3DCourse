using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class GameController : MonoBehaviour
    {
        public bool LoadRandomBots => _loadRandomBots;

        [SerializeField] bool _loadRandomBots = false;
        private Controllers _controllers;
        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            for (var i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }

        private void OnDisable()
        {
            _controllers.Dispose();
        }
    }
}

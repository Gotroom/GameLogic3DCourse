using UnityEngine;


namespace Ermolaev_3D
{
    public class DayNightController : BaseController, IInitializable, IExecutable
    {
        private GameObject _sun;

        public void Initialization()
        {
            var sunResource = Resources.Load<GameObject>("Sun");
            _sun = GameObject.Instantiate(sunResource, Vector3.zero, Quaternion.identity);
        }

        public void Execute()
        {
            _sun.transform.Rotate(2.0f * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}


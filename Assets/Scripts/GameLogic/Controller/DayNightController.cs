using UnityEngine;


namespace Ermolaev_3D
{
    public class DayNightController : BaseController, IInitializable, IExecutable
    {
        private GameObject _sun;

        public void Initialization()
        {
            var sunResource = Resources.Load<GameObject>("Sun");
            Quaternion angle = Quaternion.identity;
            angle.x = 2.0f;
            _sun = GameObject.Instantiate(sunResource, Vector3.zero, angle);
        }

        public void Execute()
        {
            _sun.transform.Rotate(0.5f * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}


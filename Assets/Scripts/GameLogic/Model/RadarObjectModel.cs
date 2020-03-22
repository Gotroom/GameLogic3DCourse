using UnityEngine;
using UnityEngine.UI;

namespace Ermolaev_3D
{
    public class RadarObjectModel : MonoBehaviour
    {
        [SerializeField] private Image _ico;

        private void OnValidate()
        {
            _ico = Resources.Load<Image>("Image");
        }

        private void OnDisable()
        {
            RadarUI.RemoveRadarObject(gameObject);
        }

        private void OnEnable()
        {
            RadarUI.RegisterRadarObject(gameObject, _ico);
        }
    }
}
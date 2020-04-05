using UnityEngine;
using UnityEngine.UI;


namespace Ermolaev_3D
{
    public class HealthUI : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public float Text
        {
            set => _text.text = $"{value}";
        }
    }
}


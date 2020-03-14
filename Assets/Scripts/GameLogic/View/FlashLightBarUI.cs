using UnityEngine;
using UnityEngine.UI;

namespace Ermolaev_3D
{
    public sealed class FlashLightBarUI : MonoBehaviour
    {
        private GameObject _bar;
        private Image _image;

        private void Awake()
        {
            _bar = gameObject;
            _image = GetComponentInChildren<Image>();
        }

        public float Fill
        {
            set => _bar.transform.localScale = new Vector2(1.0f, value);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetColor(Color col)
        {
            _image.color = col;
        }
    }
}
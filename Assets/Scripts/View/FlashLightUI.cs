using UnityEngine;
using UnityEngine.UI;


namespace Ermolaev_3D
{
    public sealed class FlashLightUI : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _bar;

        public float Text
        {
            set => _text.text = $"{value:0.0}";
        }

        public float Bar
        {
            set => _bar.transform.localScale = new Vector3(1.0f, value);
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
            _bar.gameObject.SetActive(value);
        }
    }
}

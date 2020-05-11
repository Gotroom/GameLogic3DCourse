using UnityEngine;
using UnityEngine.UI;

namespace Ermolaev_3D
{
    public class TextUI : MonoBehaviour, IControlText
    {
        private Text _text;

        public Text GetText
        {
            get
            {
                if (!_text)
                {
                    _text = transform.GetComponent<Text>();
                }
                return _text;
            }
        }
        public Dropdown GetControl
        {
            get
            {
                return null;
            }
        }

        public void SetInteractable(bool value)
        {
            GetControl.interactable = value;
        }

        public GameObject Instance => gameObject;
        public Selectable Control => GetControl;
    } 
}
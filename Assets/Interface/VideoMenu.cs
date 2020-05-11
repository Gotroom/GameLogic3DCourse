using UnityEngine;
using System.Collections.Generic;


namespace Ermolaev_3D
{
    public sealed class VideoMenu : BaseMenu
    {
        [SerializeField] private GameObject _videoPanel;

        [SerializeField] private TextUI _header;
        [SerializeField] private ComboboxUI _combobox;
        [SerializeField] private ButtonUi _back;

        private void Start()
        {
            _header.GetText.text = LangManager.Instance.Text("VideoMenuItems", "Header");

            var options = _combobox.GetOptions;
            for (int i = 0; i < options.Count; i++)
            {
                options[i].text = LangManager.Instance.Text("VideoMenuItems", $"Quality_{i}");
            }

            _combobox.GetControl.value = QualitySettings.GetQualityLevel();

            _combobox.GetControl.onValueChanged.AddListener(delegate
            {
                VideoQualityChanged(_combobox.GetControl.value);
            });

            _back.GetText.text = LangManager.Instance.Text("VideoMenuItems", "Back");
            _back.GetControl.onClick.AddListener(delegate
            {
                Back();
            });
        }

        public override void Hide()
        {
            if (!IsShow) return;
            _videoPanel.gameObject.SetActive(false);
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            _videoPanel.gameObject.SetActive(true);
            IsShow = true;
        }

        private void VideoQualityChanged(int value)
        {
            QualitySettings.SetQualityLevel(value);
        }

        private void Back()
        {
            Interface.Execute(InterfaceObject.OptionsMenu);
        }
    } 
}

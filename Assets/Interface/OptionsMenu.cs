using UnityEngine;
using UnityEngine.EventSystems;


namespace Ermolaev_3D
{
    public sealed class OptionsMenu : BaseMenu
    {
        [SerializeField] private GameObject _optionsPanele;

        [SerializeField] private ButtonUi _video;
        [SerializeField] private ButtonUi _sound;
        [SerializeField] private ButtonUi _game;
        [SerializeField] private ButtonUi _back;

        private void Start()
        {
            _video.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Video");
            _video.GetControl.onClick.AddListener(delegate
            {
                LoadVideoOptions();
            });

            _sound.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Sound");
            _sound.GetControl.onClick.AddListener(delegate
            {
                LoadSoundOptions();
            });

            _game.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Game");
            _game.GetControl.onClick.AddListener(delegate
            {
                LoadGameOptions();
            });

            _back.GetText.text = LangManager.Instance.Text("OptionsMenuItems", "Back");
            _back.GetControl.onClick.AddListener(delegate
            {
                Back();
            });
        }

        public override void Hide()
        {
            if (!IsShow) return;
            _optionsPanele.gameObject.SetActive(false);
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            _optionsPanele.gameObject.SetActive(true);
            IsShow = true;
        }

        private void LoadVideoOptions()
        {
            Interface.Execute(InterfaceObject.VideoOptions);

        }
        private void LoadSoundOptions()
        {
            Interface.Execute(InterfaceObject.AudioOptions);
        }
        private void LoadGameOptions()
        {
            Interface.Execute(InterfaceObject.GameOptions);
        }
        private void Back()
        {
            Interface.Execute(InterfaceObject.MainMenu);
        }
    } 
}

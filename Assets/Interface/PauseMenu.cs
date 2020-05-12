using UnityEngine;
using UnityEngine.SceneManagement;


namespace Ermolaev_3D
{
    public class PauseMenu : BaseMenu
    {
        private const int MAIN_MENU_BUILD_INDEX = 0;

        [SerializeField] private GameObject _pausePanel;

        [SerializeField] private TextUI _header;
        [SerializeField] private ButtonUi _save;
        [SerializeField] private ButtonUi _load;
        [SerializeField] private ButtonUi _options;
        [SerializeField] private ButtonUi _quitToMenu;
        [SerializeField] private ButtonUi _back;

        private void Start()
        {
            _header.GetText.text = LangManager.Instance.Text("PauseMenuItems", "Header");

            _save.GetText.text = LangManager.Instance.Text("PauseMenuItems", "Save");
            _save.GetControl.onClick.AddListener(delegate
            {
                Save();
            });
            _save.SetInteractable(false);

            _load.GetText.text = LangManager.Instance.Text("PauseMenuItems", "Load");
            _load.GetControl.onClick.AddListener(delegate
            {
                Load();
            });
            _load.SetInteractable(false);

            _options.GetText.text = LangManager.Instance.Text("PauseMenuItems", "Options");
            _options.GetControl.onClick.AddListener(delegate
            {
                ShowOptions();
            });

            _quitToMenu.GetText.text = LangManager.Instance.Text("PauseMenuItems", "Quit");
            _quitToMenu.GetControl.onClick.AddListener(delegate
            {
                LoadMainMenu();
            });

            _back.GetText.text = LangManager.Instance.Text("PauseMenuItems", "Back");
            _back.GetControl.onClick.AddListener(delegate
            {
                Back();
            });
        }

        public override void Hide()
        {
            if (!IsShow) return;
            _pausePanel.gameObject.SetActive(false);
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            _pausePanel.gameObject.SetActive(true);
            IsShow = true;
        }

        private void Load()
        {
            //todo
        }

        private void Save()
        {
            //todo
        }

        private void ShowOptions()
        {
            Interface.Execute(InterfaceObject.OptionsMenu);
        }

        private void LoadMainMenu()
        {
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            if (SceneManagerHelper.Instance.Check())
            {
                Interface.LoadSceneAsync(SceneManagerHelper.Instance.Scenes.MainMenu.name);
            }
            else
            {
                string pathToScene = SceneUtility.GetScenePathByBuildIndex(MAIN_MENU_BUILD_INDEX);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
                Interface.LoadSceneAsync(sceneName);
            }
        }

        private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            // init game
            SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
        }

        private void Back()
        {
            Interface.BackToGame();
        }
    } 
}

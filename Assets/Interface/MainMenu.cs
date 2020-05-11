using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ermolaev_3D
{
    public sealed class MainMenu : BaseMenu
    {
        private const int GAME_BUILD_INDEX = 1;

        [SerializeField] private GameObject _mainPanale;

        [SerializeField] private ButtonUi _newGame;
        [SerializeField] private ButtonUi _continue;
        [SerializeField] private ButtonUi _options;
        [SerializeField] private ButtonUi _quit;

        private void Start()
        {
            _newGame.GetText.text = LangManager.Instance.Text("MainMenuItems", "NewGame");
            _newGame.GetControl.onClick.AddListener(delegate
            {
                LoadNewGame();
            });

            //_continue.GetText.text = LangManager.Instance.Text("MainMenuItems", "Continue");
            //_continue.SetInteractable(false);
            _options.GetText.text = LangManager.Instance.Text("MainMenuItems", "Options");
            _options.GetControl.onClick.AddListener(delegate
            {
                ShowOptions();
            });

            _quit.GetText.text = LangManager.Instance.Text("MainMenuItems", "Quit");
            _quit.GetControl.onClick.AddListener(delegate
            {
                Interface.QuitGame();
            });
        }

        public override void Hide()
        {
            if (!IsShow) return;
            _mainPanale.gameObject.SetActive(false);
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            _mainPanale.gameObject.SetActive(true);
            IsShow = true;
        }

        private void ShowOptions()
        {
            Interface.Execute(InterfaceObject.OptionsMenu);
        }

        private void LoadNewGame()
        {
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;

            if (SceneManagerHelper.Instance.Check())
            {
                Interface.LoadSceneAsync(SceneManagerHelper.Instance.Scenes.Game.name);
            }
            else
            {
                string pathToScene = SceneUtility.GetScenePathByBuildIndex(GAME_BUILD_INDEX);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
                Interface.LoadSceneAsync(sceneName);
            }
        }

        private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            // init game
            SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
        }
    }

}
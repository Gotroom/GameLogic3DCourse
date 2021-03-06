﻿using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MainMenu : BaseMenu
{

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
            LoadNewGame(SceneManagerHelper.Instance.Scenes.Game.SceneAsset.name);
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

    private void LoadNewGame(string lvl)
    {
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        Interface.LoadSceneAsync(lvl);
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // init game

        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }
}

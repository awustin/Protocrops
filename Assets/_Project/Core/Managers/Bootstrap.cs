using System;
using UnityEngine.SceneManagement;

public class Bootstrap : Singleton<Bootstrap>
{
    public enum SceneName
    {
        Operations,
        Gameplay,
        UI,
        Level,
        LoadMenu
    }

    private EventManager _eventManager;

    private void Awake()
    {
        BootstrapGame();
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager.StartGame += UnloadMenuAndStartGame;
    }

    private void OnDisable()
    {
        _eventManager.StartGame -= UnloadMenuAndStartGame;
    }

    private void BootstrapGame()
    {
        Scene gameplayScene = SceneManager.GetSceneByName(SceneName.Gameplay.ToString());
        bool hasGameplay = gameplayScene.IsValid();
        bool hasUI = SceneManager.GetSceneByName(SceneName.UI.ToString()).IsValid();
        bool hasLevel = SceneManager.GetSceneByName(SceneName.Level.ToString()).IsValid();
        bool hasLoadMenu = SceneManager.GetSceneByName(SceneName.LoadMenu.ToString()).IsValid();

        if (!hasGameplay && !hasUI && !hasLevel)
        {
            if (!hasLoadMenu)
                SceneManager.LoadScene(SceneName.LoadMenu.ToString(), LoadSceneMode.Additive);

            return;
        }

        if (!hasGameplay) SceneManager.LoadScene(SceneName.Gameplay.ToString(), LoadSceneMode.Additive);
        if (!hasUI) SceneManager.LoadScene(SceneName.UI.ToString(), LoadSceneMode.Additive);
        if (!hasLevel) SceneManager.LoadScene(SceneName.Level.ToString(), LoadSceneMode.Additive);
    }

    private void UnloadMenuAndStartGame()
    {
        SceneManager.LoadScene(SceneName.Gameplay.ToString(), LoadSceneMode.Additive);
        SceneManager.LoadScene(SceneName.UI.ToString(), LoadSceneMode.Additive);
        SceneManager.LoadScene(SceneName.Level.ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("LoadMenu");
    }
}
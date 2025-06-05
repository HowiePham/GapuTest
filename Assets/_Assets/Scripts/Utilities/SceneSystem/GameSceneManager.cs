using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : TemporaryMonoSingleton<GameSceneManager>
{
    [SerializeField] private SceneName homeScene = SceneName.Home;
    [SerializeField] private SceneName loadingScene = SceneName.Loading;
    [SerializeField] private SceneName targetScene;
    [SerializeField] private SceneName currentScene;
    [SerializeField] private float loadingProgress;
    private Dictionary<SceneName, string> _enumToSceneName;
    private const float FullLoadingProgress = 0.9f;
    private const float NewLoadingProgress = 0;

    protected override void Init()
    {
        InitSystem();
    }

    private void InitSystem()
    {
        _enumToSceneName = new Dictionary<SceneName, string>();
        var maxSceneIndex = (int)SceneName.NONE;
        for (var i = 0; i < maxSceneIndex; i++)
        {
            var scene = (SceneName)i;
            _enumToSceneName.Add(scene, scene.ToString());
        }
    }

    public void ResetLoadingProgress()
    {
        loadingProgress = NewLoadingProgress;
    }

    public void GoToHomeScene()
    {
        SetCurrentScene(homeScene);
        SceneManager.LoadScene(GetSceneName(homeScene));
    }

    public void EnableLoadingScene()
    {
        SetCurrentScene(loadingScene);
        SceneManager.LoadScene(GetSceneName(loadingScene));
    }

    public void LoadScene(SceneName scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SetCurrentScene(scene);
        SceneManager.LoadScene(GetSceneName(scene), mode);
    }

    public void LoadSceneAsync(SceneName scene, LoadSceneMode mode = LoadSceneMode.Single, Action OnLoadComplete = null)
    {
        SetCurrentScene(scene);
        StartCoroutine(HandleLoadingSceneAsync(scene, mode, OnLoadComplete));
    }

    private IEnumerator HandleLoadingSceneAsync(SceneName scene, LoadSceneMode mode = LoadSceneMode.Single, Action OnLoadComplete = null)
    {
        var loadingSceneAsync = SceneManager.LoadSceneAsync(GetSceneName(scene), mode);
        loadingSceneAsync.allowSceneActivation = false;

        while (loadingSceneAsync.progress < FullLoadingProgress)
        {
            loadingProgress = loadingSceneAsync.progress;
            yield return null;
        }

        loadingProgress = FullLoadingProgress;
        loadingSceneAsync.allowSceneActivation = true;

        yield return new WaitUntil(() => loadingSceneAsync.isDone);

        Debug.Log($"--- (Loading) Loading scene async Completed");
        OnLoadComplete?.Invoke();
    }

    public void UnloadScene(SceneName scene)
    {
        // var activeScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(GetSceneName(scene));
    }

    public void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(GetSceneName(currentScene));
    }

    public void LoadSceneWithLoadingScene(SceneName scene)
    {
        SetCurrentScene(scene);
        SetTargetScene(scene);
        EnableLoadingScene();
    }

    public void SetCurrentScene(SceneName scene)
    {
        currentScene = scene;
    }

    public void SetTargetScene(SceneName targetScene)
    {
        this.targetScene = targetScene;
    }

    public SceneName GetTargetScene()
    {
        return targetScene;
    }

    public float GetLoadingProgress()
    {
        return loadingProgress;
    }

    private string GetSceneName(SceneName sceneName)
    {
        return _enumToSceneName[sceneName];
    }
}
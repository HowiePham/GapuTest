using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneName currentScene;
    [SerializeField] private SceneName targetScene;
    [SerializeField] private bool isLoadAync;
    [SerializeField] private bool isLoadWithLoadingScreen;
    [SerializeField] private LoadSceneMode loadSceneMode;

    private GameSceneManager GameSceneManager => GameSceneManager.Instance;

    public void LoadScene()
    {
        if (isLoadAync) GameSceneManager.LoadSceneAsync(targetScene, loadSceneMode);
        else if (isLoadWithLoadingScreen) GameSceneManager.LoadSceneWithLoadingScene(targetScene);
        else GameSceneManager.LoadScene(targetScene, loadSceneMode);
    }

    public void UnloadScene()
    {
        GameSceneManager.UnloadScene(currentScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
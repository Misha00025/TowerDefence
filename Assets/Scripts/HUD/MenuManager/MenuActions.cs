using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void ExitGame()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void SaveSceneLoadingInfoLevel(SettingsManager settingsManager)
    {
        SceneLoadingInfo info = new SceneLoadingInfo();
        info.LocalizationSettings = settingsManager.LocalizationSettings;
        LoadingProcessor.Instance.RegisterLoadingModel(info);
    }
}

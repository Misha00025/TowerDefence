using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingProcessor : MonoBehaviour
{
    private static LoadingProcessor _instance;
    private Action _loadingModelAction;

    public static LoadingProcessor Instance
    {
        get
        {
            if (_instance == null)
                Initialize();

            return _instance;
        }
    }

    private static void Initialize()
    {
        _instance = new GameObject("LoadingProcessor").AddComponent<LoadingProcessor>();
        _instance.transform.SetParent(null);
        DontDestroyOnLoad(_instance);
    }

    public void ApplyLoadingModel()
    {
        _loadingModelAction?.Invoke();
        _loadingModelAction = null;
    }

    public void RegisterLoadingModel(SceneLoadingInfo loadingModel)
    {
        _loadingModelAction = () =>
        {
            foreach (var rootObjects in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var handler in rootObjects.GetComponentsInChildren<ISceneLoader>())
                {
                    handler.OnSceneLoaded(loadingModel);
                }
            }
        };
    }
}

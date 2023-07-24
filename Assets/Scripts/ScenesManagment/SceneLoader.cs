using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SceneLoader : MonoBehaviour, ISceneLoader
{
    [SerializeField] private SettingsManager _settingsStorage;
    [SerializeField] private SceneLoadingInfo _defaultInfo;
    private bool _isLoaded = false;

    private void Awake()
    {        
        LoadingProcessor.Instance.ApplyLoadingModel();
        
        if (!_isLoaded)
            OnSceneLoaded(_defaultInfo);
    }

    public void OnSceneLoaded(SceneLoadingInfo info)
    {
        _isLoaded = true; 
        _defaultInfo.LocalizationSettings = info.LocalizationSettings;
    }

    private void OnDestroy()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingsManager : MonoBehaviour
{
    private string _localizationSettings;

    public UnityEvent<SettingsManager> ValuesChanged = new UnityEvent<SettingsManager>();

    public string LocalizationSettings
    {
        get { return _localizationSettings; }
        set { _localizationSettings = value;  ValuesChanged.Invoke(this); }
    }
}

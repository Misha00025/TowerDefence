using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageToggler : MonoBehaviour
{
    [SerializeField] private List<Locale> _locales = new List<Locale>();
    [SerializeField] private LocalizationSettings _localizationSettings;

    public void ToggleLanguage()
    {
        Debug.Log("Try Togge");
        Locale selectedLocale = _localizationSettings.GetSelectedLocale();
        int selectedLocaleID = _locales.IndexOf(selectedLocale);
        int nextId = selectedLocaleID + 1;

        if (nextId == _locales.Count)
            nextId = 0;

        _localizationSettings.SetSelectedLocale(_locales[nextId]);
    }

}

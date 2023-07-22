using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalizationTools : EditorWindow
{
    [MenuItem("Localization/Create base localization file")]
    public static void CreateBaseLocalizationFile()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(i);
            foreach (GameObject obj in scene.GetRootGameObjects())
            {
                foreach (var text in obj.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    Debug.Log(text.text);
                }
            }
        }
    }

    [MenuItem("Localization/Set Translated Component On All Objects")]
    public static void SetTranslatedComponentOnAllObjects()
    {
        

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour, ISceneLoader
{
    public void OnSceneAwake()
    {
        
    }

    public void OnSceneLoaded(SceneLoadingInfo argument)
    {
        Debug.Log(argument.Level);
    }
}

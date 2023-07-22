using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneLoader
{
    void OnSceneLoaded(SceneLoadingInfo levelLoadingInfo);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneLoader
{
    void OnSceneAwake();
    void OnSceneLoaded(SceneLoadingInfo levelLoadingInfo);
}

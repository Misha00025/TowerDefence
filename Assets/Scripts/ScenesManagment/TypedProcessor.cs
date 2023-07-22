using System.Linq;
using UnityEngine;

public class TypedProcessor : MonoBehaviour
{
    private void Awake()
    {
        foreach(var handler in FindObjectsOfType<MonoBehaviour>().OfType<ISceneLoader>())
        {
            handler.OnSceneAwake();
        }
        LoadingProcessor.Instance.ApplyLoadingModel();
    }
}

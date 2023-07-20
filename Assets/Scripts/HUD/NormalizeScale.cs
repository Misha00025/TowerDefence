using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizeScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                Screen.SetResolution(600, 1080, true);
                break;
        }
    }
}

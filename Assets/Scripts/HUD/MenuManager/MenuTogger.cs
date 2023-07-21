using PauseManagement.Core;
using System.Collections.Generic;
using UnityEngine;

public class MenuTogger : MonoBehaviour
{
    [SerializeField] PauseManager _pauseManager;
    [SerializeField] Canvas _menuCanvas;
    [SerializeField] Canvas _ingameCanvas;
    [SerializeField] List<GameObject> _disabledObjects;

    private MenusManager _manager;
    private bool _menuOpened = false;

    public void Initialize(MenusManager manager)
    {
        _manager = manager;
        CloseMenu();
    }

    public void OpenMenu()
    {
        _pauseManager.Pause();
        _menuCanvas.enabled = true;
        _ingameCanvas.enabled = false;

        foreach (GameObject obj in _disabledObjects)
        {
            obj.SetActive(false);
        }

        _menuOpened = true;
    }

    public void CloseMenu()
    {
        _pauseManager.Resume();
        _menuCanvas.enabled = false;
        _ingameCanvas.enabled = true;

        foreach (GameObject obj in _disabledObjects)
        {
            obj.SetActive(true);
        }

        _menuOpened = false;
    }

    public void ToggeMenu()
    {
        if (_menuOpened)
            _manager.CloseMenu(this);
        else
            _manager.OpenMenu(this);
    }

}

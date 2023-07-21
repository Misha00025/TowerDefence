using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : MonoBehaviour
{
    private MenuTogger _oppenedMenu; 
    private bool _menuOpened = false;

    public bool MenuOppened => _menuOpened;

    public void OpenMenu(MenuTogger menu)
    {
        if (_menuOpened) return;

        menu.OpenMenu();
        _oppenedMenu = menu;
        _menuOpened = true;
    }

    public void CloseMenu(MenuTogger menu)
    {
        if (!_menuOpened) return;

        menu.CloseMenu();
        _oppenedMenu = null;
        _menuOpened = false;
    }

    public void ToggeMenu(MenuTogger menu)
    {
        if (_oppenedMenu != menu) return;

        menu.ToggeMenu();
    }

}

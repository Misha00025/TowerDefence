using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField] private List<GameObject> _inSelectedModeObjects = new List<GameObject>();
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public bool IsSelected { get; private set; }

    private void Awake()
    {
        GameObject gameObject = new GameObject();
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        
        Instantiate(gameObject, transform);

        UnSelect();
    }

    public virtual void Select()
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.sortingOrder = 102;
        foreach (var obj in _inSelectedModeObjects)
            obj.SetActive(true);
    }

    public virtual void UnSelect()
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.sortingOrder = 0;
        foreach (var obj in _inSelectedModeObjects)
            obj.SetActive(false);
    }
}

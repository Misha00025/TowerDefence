using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuilderInput
{
    private Builder _builder;
    private PlayerInput _playerInput;
    private bool _dragged = false;
    private Tower _draggetTower = null;

    public BuilderInput(PlayerInput playerInput, Builder builder)
    {
        _playerInput = playerInput;
        _builder = builder;
        _playerInput.ActionActivated.AddListener(OnPlayerInput);
    }

    private void OnPlayerInput(Ray ray, IncomingAction action)
    {
        switch (action)
        {
            case IncomingAction.Drag:
                StartDrag(ray);
                break;
            case IncomingAction.Drop:
                if (_draggetTower == null)
                    break;
                _builder.TryBuildTower(_draggetTower);
                _dragged = false;
                _draggetTower = null;
                break;
        }
    }

    private void StartDrag(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            TowerBlueprint blueprint;
            if (hit.collider.gameObject.TryGetComponent(out blueprint))
            {
                Tower tower = blueprint.GetInstanciatedObject();
                tower.transform.position = ray.origin;
                tower.GetComponent<SpriteRenderer>().sortingOrder = 102;
                _dragged = true;
                _draggetTower = tower;
                _playerInput.StartCoroutine(Drag(tower));
            }
        }
    }

    private IEnumerator Drag(Tower tower)
    {
        while (_dragged)
        {
            tower.transform.position = _playerInput.GetInputRay().origin;
            yield return null;
        }
    }
}

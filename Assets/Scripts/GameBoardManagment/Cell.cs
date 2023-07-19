using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private bool _canBuild = false;
    [SerializeField]
    private bool _canMove = false;
    public bool CanBuild => _canBuild;
    public bool CanMove => _canMove;
}

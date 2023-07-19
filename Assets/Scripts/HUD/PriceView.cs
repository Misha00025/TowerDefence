using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceView : MonoBehaviour
{
    [SerializeField] TowerBlueprint blueprint;
    [SerializeField] Text text;
    private void Awake()
    {
        text.text = $"{blueprint.Price}";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceView : MonoBehaviour
{
    [SerializeField] TowerBlueprint blueprint;
    [SerializeField] TextMeshProUGUI text;
    private void Awake()
    {
        text.SetText( $"{blueprint.Price}" );
    }
}

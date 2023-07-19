using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerWallet))]
public class PlayerWalletView : MonoBehaviour
{
    public void Initialize(PlayerWallet playerWallet, TextMeshProUGUI textMesh)
    {
        textMesh.SetText($"{playerWallet.Money}");
        playerWallet.MoneyChanged.AddListener(() =>
        {
            textMesh.SetText($"{playerWallet.Money}");
        });
    }
}

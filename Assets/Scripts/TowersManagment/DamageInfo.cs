using System;
using UnityEngine;

[Serializable]
public struct DamageInfo
{
    public DamageType Type;
    public Vector2 Direction;

    public DamageInfo(DamageInfo damageInfo, Vector2 direction)
    {
        Type = damageInfo.Type;
        Direction = direction;
    }
}

public enum DamageType
{
    Default,
    Piercing
}

using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Ingredient {

    [Serializable]
    public enum type
    {
        None,
        Corn,
        Grub,
        Dance,
        Candle,
        Fire,
        Light,
        Praise,
        Cut,
    }

    [SerializeField]
    type currentType;

    public type CurrentType
    {
        get
        {
            return currentType;
        }

        set
        {
            currentType = value;
        }
    }
}

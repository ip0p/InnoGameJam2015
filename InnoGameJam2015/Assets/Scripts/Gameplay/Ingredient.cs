using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Ingredient : MonoBehaviour {

    [Serializable]
    public enum type
    {
        shake,
        dance,
        drum
    }

    [SerializeField]
    type currenttype;

    public type Currenttype
    {
        get
        {
            return currenttype;
        }

        set
        {
            currenttype = value;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[Serializable]
public class Receipt
{

    [SerializeField]
    public string ID;

    /// <summary>
    ///   Basic duration this recipe requires, ie difficulty (in s).
    /// </summary>
    public float BaseDuration;

    [SerializeField]
    private Image symbol;

    [SerializeField]
    private List<Ingredient> ingredients;

    public List<Ingredient> Ingredients
    {
        get
        {
            return ingredients;
        }

        set
        {
            ingredients = value;
        }
    }
}

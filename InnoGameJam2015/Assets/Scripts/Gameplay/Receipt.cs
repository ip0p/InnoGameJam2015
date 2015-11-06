using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[Serializable]
public class Receipt
{

    [SerializeField]
    public string ID;

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

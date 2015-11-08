using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Receipt
{
    #region Fields

    /// <summary>
    ///   Basic duration this recipe requires, ie difficulty (in s).
    /// </summary>
    [SerializeField]
    public float BaseDuration;

    /// <summary>
    ///   Difficulty of recipe, the higher the more difficult.
    /// </summary>
    [SerializeField]
    public int Difficulty;

    [SerializeField]
    public string ID;

    [SerializeField]
    private List<Ingredient> ingredients;

    [SerializeField]
    private Image symbol;

    #endregion

    #region Properties

    public List<Ingredient> Ingredients
    {
        get
        {
            return this.ingredients;
        }

        set
        {
            this.ingredients = value;
        }
    }

    #endregion
}
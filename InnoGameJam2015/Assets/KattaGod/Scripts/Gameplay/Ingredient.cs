using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Ingredient {

    [Serializable]
    public enum type
    {
        shake,
        dance,
        drum
    }

    //GameManager gameManager;

    [SerializeField]
    type currenttype;

    void Start()
    {
        //GameObject.Find("GameManager").GetComponent<GameManager>();
    }

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

    public void Add()
    {
        //gameManager.AddIngredient(this);
    }
}

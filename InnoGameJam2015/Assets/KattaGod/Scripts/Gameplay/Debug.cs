using UnityEngine;
using System.Collections;

public class Debug : MonoBehaviour {

    GameManager gameManager;

    void Start ()
    {
        gameManager = GetComponent<GameManager>();
	}
	
	void Update ()
    {
        DebugKeys();
	}

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            gameManager.AddIngredient(Ingredient.type.Candle);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            gameManager.AddIngredient(Ingredient.type.Corn);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            gameManager.AddIngredient(Ingredient.type.Dance);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            gameManager.AddIngredient(Ingredient.type.Fire);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            gameManager.AddIngredient(Ingredient.type.Grub);
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            gameManager.AddIngredient(Ingredient.type.Light);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            gameManager.AddIngredient(Ingredient.type.Praise);
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            gameManager.AddIngredient(Ingredient.type.Cut);
        }
    }
}

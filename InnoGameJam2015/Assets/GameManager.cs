using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Text receiptText;
    Config config;
    List<Ingredient.type> currentIngredients;

	void Start ()
    {
        config = GameObject.Find("CONFIG").GetComponent<Config>();
        UpdateReceiptBookText();
    }

    void UpdateReceiptBookText()
    {
        receiptText.text = "";

        foreach (Receipt receipt in config.receiptBook)
        {
            receiptText.text += receipt.ID + "\n";
            receiptText.text += "------------" + "\n";
            foreach (Ingredient ingredient in receipt.Ingredients)
            {
                receiptText.text += ingredient.Currenttype.ToString() + "\n";
            }
        }
    }

    public void AddIngredient(Ingredient ing)
    {
        
    }
}

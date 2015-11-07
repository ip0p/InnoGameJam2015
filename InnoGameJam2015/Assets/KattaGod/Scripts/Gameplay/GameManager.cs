using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Text receiptText;
    public Config config;
    List<Ingredient> currentIngredients;

	void Start ()
    {
	    if (config == null)
	    {
	        config = GameObject.Find("CONFIG").GetComponent<Config>();
	    }
	    UpdateReceiptBookText();
    }

    void UpdateReceiptBookText()
    {
        if (receiptText == null)
        {
            return;
        }

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
        print("added" + ing.Currenttype.ToString());
    }
}

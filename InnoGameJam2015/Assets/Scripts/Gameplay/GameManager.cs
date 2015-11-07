using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject altar;
    public Text receiptText;
    Config config;
    List<Ingredient.type> currentIngredients = new List<Ingredient.type>();
    float receiptChangeTime = 4f;

    private Receipt currentReceipt;

    public Receipt CurrentReceipt
    {
        get
        {
            return currentReceipt;
        }

        set
        {
            currentReceipt = value;
        }
    }

    void Start ()
    {
        config = GameObject.Find("CONFIG").GetComponent<Config>();
        UpdateReceiptBookText();

        //TEST
        currentReceipt = config.receiptBook[0];
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
                receiptText.text += ingredient.CurrentType.ToString() + "\n";
            }
        }
    }

    public void AddIngredient(Ingredient.type ing)
    {
        currentIngredients.Add(ing);

        // check ingredients
        if (CheckIngredients())
        {
            // ok
            print("Added " + ing.ToString());

            if(ing == Ingredient.type.Candle || ing == Ingredient.type.Corn || ing == Ingredient.type.Grub)
                UpdateAltar(ing);

            if (ing == Ingredient.type.Light || ing == Ingredient.type.Fire)
            {
                UpgradeAltar(ing);
            }
        }

        else
        {
            // reset current ingredients and altar layers
            ResetIngredients();

            // fail
        }

        // receipt complete?
        if (currentIngredients.Count == currentReceipt.Ingredients.Count)
        {
            print("Receipt complete!");

            // reset current ingredients and altar layers
            ResetIngredients();

            // get random new receipt
        }
    }

    public void AddIngredient(int type)
    {
        AddIngredient((Ingredient.type)type);
    }

    private void UpdateAltar(Ingredient.type ing)
    {
        altar.transform.FindChild(ing.ToString()).gameObject.SetActive(true);
        //altar.transform.FindChild(ing.ToString()).GetComponent<AltarLayer>().Count++;
    }

    private void UpgradeAltar(Ingredient.type ing)
    {
        //upgrade last ingredient
        string lastIngredient = currentIngredients[currentIngredients.Count - 2].ToString();
        altar.transform.FindChild(lastIngredient).GetComponent<AltarLayer>().Upgrade();
    }

    private void ResetIngredients()
    {
        currentIngredients.Clear();
        ResetAltar();
    }

    private void ResetAltar()
    {
        print("reset altar");
        for (int i = 0; i < altar.transform.childCount; i++)
        {
            // deactivate layers and count
            altar.transform.GetChild(i).gameObject.SetActive(false);
            altar.transform.GetChild(i).GetComponent<AltarLayer>().Count = 1;
        }
    }

    private bool CheckIngredients()
    {
        for (int i = 0; i < currentIngredients.Count; i++)
        {
                if (currentIngredients[i] != currentReceipt.Ingredients[i].CurrentType)
                {
                    print("Wrong Ingredient!");
                    return false;
                }
        }
        return true;
    }
}

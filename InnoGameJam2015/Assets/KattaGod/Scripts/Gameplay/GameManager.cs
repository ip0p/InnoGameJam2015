using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject altar;
    public Text receiptText;
    public Config config;
    List<Ingredient.type> currentIngredients = new List<Ingredient.type>();
    float receiptChangeTime = 4f;
    private Receipt currentReceipt;

    public event Action<Receipt> RecipeFailed;
    public event Action<Receipt> RecipeComplete;

    Ingredient.type lastIngredientType;

    List<AltarLayer> altarLayers;

    Dictionary<Ingredient.type, int> ingredientCount;

    private void OnRecipeFailed(Receipt recipe)
    {
        var handler = this.RecipeFailed;
        if (handler != null)
        {
            handler(recipe);
        }
    }

    private void OnRecipeComplete(Receipt recipe)
    {
        var handler = this.RecipeComplete;
        if (handler != null)
        {
            handler(recipe);
        }
    }

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
	    if (config == null)
	    {
        config = GameObject.Find("CONFIG").GetComponent<Config>();
	    }

        UpdateReceiptBookText();



        //altar = GameObject.Find("World/Altar");

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

            if (ing == Ingredient.type.Light || ing == Ingredient.type.Fire || ing == Ingredient.type.Cut)
            {
                UpgradeAltar(ing);
            }

            lastIngredientType = ing;

            // receipt complete?
            if (currentIngredients.Count == currentReceipt.Ingredients.Count)
            {
                print("Receipt complete!");

                OnRecipeComplete(currentReceipt);
                // reset current ingredients and altar layers
                ResetIngredients();

                // get random new receipt
            }
        }
        else
        {
            // reset current ingredients and altar layers
            ResetIngredients();
            OnRecipeFailed(currentReceipt);
            // fail
        }
    }

    public void AddIngredient(int type)
    {
        AddIngredient((Ingredient.type)type);
    }

    private void UpdateAltar(Ingredient.type ing)
    {
        //altar.transform.FindChild(ing.ToString().Contains).gameObject.SetActive(true);

        //foreach (AltarLayer layer in altar.transform.GetComponentsInChildren<AltarLayer>())
        //{
        //    if (layer.transform.name.Contains(ing.ToString()))
        //    {
        //        if (!layer.gameObject.activeSelf)
        //        {
        //            layer.gameObject.SetActive(true);
        //            break;
        //        }
        //    }
        //}

        for (int i = 0; i < altar.transform.childCount; i++)
        {
            if(altar.transform.GetChild(i).name.Contains(ing.ToString()))
                if (!altar.transform.GetChild(i).gameObject.activeSelf)
                {
                    altar.transform.GetChild(i).gameObject.SetActive(true);
                    break;
                }
        }
        //altar.transform.FindChild(ing.ToString()).GetComponent<AltarLayer>().Count++;
    }

    private void UpgradeAltar(Ingredient.type ing)
    {
        //upgrade last ingredient

        //string lastIngredient = currentIngredients[currentIngredients.Count - 2].ToString();
        //altar.transform.FindChild(lastIngredient).GetComponent<AltarLayer>().Upgrade();


        for (int i = 0; i < altar.transform.childCount; i++)
        {
                if (altar.transform.GetChild(i).gameObject.activeSelf)
                {
                    altar.transform.GetChild(i).GetComponent<AltarLayer>().Upgrade(ing);
                }
        }
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
            altar.transform.GetChild(i).GetComponent<AltarLayer>().Index = 0;
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

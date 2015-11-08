using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;


public class GameManager : MonoBehaviour
{
    public GameObject altar;
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

        GetComponent<KattaGod.Progression.VictoryBehaviour>().Defeat += GameManager_Defeat;
    }

    private void GameManager_Defeat()
    {
        Application.LoadLevel("GameOver");
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
        for (int i = 0; i < altar.transform.childCount; i++)
        {
            if(altar.transform.GetChild(i).name.Contains(ing.ToString()))
                if (!altar.transform.GetChild(i).gameObject.activeSelf)
                {
                    altar.transform.GetChild(i).gameObject.SetActive(true);
                    break;
                }
        }
    }

    private void UpgradeAltar(Ingredient.type ing)
    {
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

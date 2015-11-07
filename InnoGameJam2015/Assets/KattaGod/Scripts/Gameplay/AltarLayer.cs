using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AltarLayer : MonoBehaviour {

    public List<Sprite> images;

    public Ingredient.type IngredientType;
    private int index;

    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
            SetImage();
        }
    }

    void SetImage()
    {
        
            GetComponent<SpriteRenderer>().sprite = images[index];
    }

    public void Upgrade(Ingredient.type ing)
    {
        switch(ing)
        {
            case Ingredient.type.Fire:
                if (images[1] != null && index == 0)
                    Index = 1;
                break;

            case Ingredient.type.Cut:
                if (images[2] != null && index == 0)
                    Index = 2;
                break;
        }
    }


}

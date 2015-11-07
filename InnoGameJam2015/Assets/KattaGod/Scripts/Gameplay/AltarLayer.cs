using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AltarLayer : MonoBehaviour {

    int count = 1;
    public List<Sprite> images;
    public List<Sprite> upgradedImages;

    public int Count
    {
        get
        {
            return count;
        }

        set
        {
            count = value;
            SetImage();
        }
    }

    void SetImage()
    {
        GetComponent<Image>().sprite = images[count - 1];
    }

    public void Upgrade()
    {
        GetComponent<Image>().sprite = upgradedImages[count - 1];
    }


}

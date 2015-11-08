using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

    public Text scoreText;

    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("LastScore").ToString();
        PlayerPrefs.DeleteKey("LastScore");
    }
    
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel("Start");
        }
	}
}

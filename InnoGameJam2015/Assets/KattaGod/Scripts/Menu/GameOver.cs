using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel("Start");
        }
	}
}

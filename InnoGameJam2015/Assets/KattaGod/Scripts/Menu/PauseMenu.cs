using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	void OnEnable ()
    {
        //Time.timeScale = 0f;
	}

    public void ButtonRestart()
    {
        //Time.timeScale = 1f;
        Application.LoadLevel("Entry");
    }

    public void ButtonQuit()
    {
        //Time.timeScale = 1f;
        Application.Quit();
    }
}

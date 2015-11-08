using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {


    public SpriteRenderer splash;
    float fadeTime = 1.5f;
    public Color transparent;
    bool faded = false;
    public GameObject textObject;

    void Start ()
    {
        splash.color = transparent;
        StartCoroutine(FadeIn());
	}

    void Update()
    {
        if (faded && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeOut());
        }
    }
	
    IEnumerator FadeIn()
    {
        Color start = splash.color;

        float fade = 0;
        while (fade < fadeTime)
        {
            fade += Time.deltaTime;
            splash.color = Color.Lerp(start, Color.white, fade/fadeTime);
            yield return null;
        }

        faded = true;
        textObject.SetActive(true);

    }

    IEnumerator FadeOut()
    {
        textObject.SetActive(false);

        Color start = splash.color;

        float fade = 0;
        while (fade < fadeTime)
        {
            fade += Time.deltaTime;
            splash.color = Color.Lerp(start, transparent, fade / fadeTime);
            yield return null;
        }

        Application.LoadLevel("Entry");
    }


}

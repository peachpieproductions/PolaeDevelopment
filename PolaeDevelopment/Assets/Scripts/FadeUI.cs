using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FadeUI : MonoBehaviour {

    public Vector2 offset;
    public float delay;
    public float speed = 1;
    public bool affectChildren;
    public AudioClip soundEffect;
    [Range(0f, 1f)]
    public float soundVolume = 1;
    Image imageComponent;
    Text textComponent;
    public bool active;
    public GameObject splashScreen;
    bool thisSplashScreenHasBeenShown;
    Vector3 startPos;
    Vector3 targetPos;
    Color col;
    bool fading;
    bool child;
    List<FadeUI> childFades = new List<FadeUI>();
    public bool isSplashScreen;


    // Use this for initialization
    void Awake() {
        startPos = transform.localPosition;

        //children
        if (affectChildren) {
            foreach (Transform t in transform) {
                if (t.childCount > 0) {
                    foreach (Transform tt in t) {
                        if (tt != t) AddFade(tt);
                    }
                }
                AddFade(t);
            }
        }

    }

    private void Start() {
        StartFade(true);
    }

    void AddFade(Transform t) {
        FadeUI scr = t.GetComponent<FadeUI>();
        if (scr == null) {
            scr = t.gameObject.AddComponent<FadeUI>();
            scr.delay = delay;
            scr.speed = speed;
            scr.child = true;
            scr.splashScreen = splashScreen;
            childFades.Add(scr);
            scr.StartFade(true);
        }
    }


    public void StartFade(bool start) {
        if (fading) return;

        if (affectChildren) {
            foreach (FadeUI scr in childFades) {
                if (scr != this) scr.StartFade(start);
            }
        }

        imageComponent = GetComponent<Image>();
        textComponent = GetComponent<Text>();
        active = start;
        if (active) {
            if (imageComponent != null) {
                col = imageComponent.color;
                col.a = 0;
                imageComponent.color = col;
            }
            if (textComponent != null) {
                col = textComponent.color;
                col.a = 0;
                textComponent.color = col;
            }
            targetPos = startPos;
            if (!child) transform.localPosition = startPos + (Vector3)offset;

            if (!fading) StartCoroutine(Fade());
            else if (soundEffect != null) AudioManager.am.PlayClip(soundEffect, false, soundVolume);

        } else {
            targetPos = startPos + (Vector3)offset;
            if (!fading) StartCoroutine(Fade());
            foreach (FadeUI f in childFades) {
                f.StartFade(false);
            }
        }
    }

    IEnumerator Fade() {
        bool done = false;
        fading = true;

        if (splashScreen != null && active) {
            if (!splashScreen.GetComponent<FadeUI>().thisSplashScreenHasBeenShown) {
                AppUIController.uic.showingSplashScreen = true;
                splashScreen.GetComponent<FadeUI>().thisSplashScreenHasBeenShown = true;
                splashScreen.SetActive(true);
                splashScreen.transform.SetSiblingIndex(splashScreen.transform.parent.childCount - 1);
                splashScreen.GetComponent<FadeUI>().StartFade(true);
            }
        }

        var spd = speed;
        if (!active) spd *= 2;
        if (delay > 0 && active) yield return new WaitForSeconds(delay);
        if (AppUIController.uic.showingSplashScreen && !isSplashScreen) { yield return new WaitForSeconds(2f); AppUIController.uic.showingSplashScreen = false; }
        if (soundEffect != null && active) AudioManager.am.PlayClip(soundEffect, false, soundVolume);
        while (true) {
            col = Color.white;
            if (imageComponent != null) {
                col = imageComponent.color;
                if (active && col.a < 1) { col.a += .01f * spd; if (col.a >= 1) { done = true;  } }
                if (!active && col.a > 0) { col.a -= .01f * spd; if (col.a <= 0) { done = true; } }
                imageComponent.color = col;
            }
            if (textComponent != null) {
                col = textComponent.color;
                if (active && col.a < 1) { col.a += .01f * spd; if (col.a >= 1) { done = true; } }
                if (!active && col.a > 0) { col.a -= .01f * spd; if (col.a <= 0) { done = true; } }
                textComponent.color = col;
            }
            if (textComponent == null && imageComponent == null) done = true;

            if (done && Vector3.Distance(transform.localPosition, targetPos) < 1f) {
                fading = false;
                if (!active && !child) gameObject.SetActive(false);
                else {
                    if (splashScreen != null && active) {
                        if (!child) {
                            splashScreen.SetActive(false);
                        }
                    }
                }
                yield break;
            }

            //pos
            if (!child) transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, .05f * spd);

            yield return null;
        }
    }

}

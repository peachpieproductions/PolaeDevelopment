using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FadeUI : MonoBehaviour {

    public bool startOnPlay;
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
    Vector3 startPos;
    Vector3 targetPos;
    Color col;
    bool fading;
    bool child;


    // Use this for initialization
    void Awake() {
        startPos = transform.localPosition;

        //children
        if (affectChildren) {
            foreach (Transform t in transform) {
                FadeUI scr = t.GetComponent<FadeUI>();
                if (scr == null) {
                    scr = t.gameObject.AddComponent<FadeUI>();
                    scr.delay = delay;
                    scr.speed = speed;
                    scr.child = true;
                    scr.splashScreen = splashScreen;
                    if (startOnPlay) scr.StartFade(true);
                }
            }
        }

        if (startOnPlay) StartFade(true);

    }


    public void StartFade(bool start) {
        if (fading) return;

        if (affectChildren) {
            foreach (FadeUI scr in transform.GetComponentsInChildren<FadeUI>()) {
                if (startOnPlay && scr != this) scr.StartFade(true);
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
        }
    }

    IEnumerator Fade() {
        bool done = false;

        if (splashScreen != null && active) {
            if (!child) {
                splashScreen.SetActive(true);
                splashScreen.transform.SetSiblingIndex(splashScreen.transform.parent.childCount-1);
                splashScreen.GetComponent<FadeUI>().StartFade(true);
            }
            yield return new WaitForSeconds(1f);
        }

        var spd = speed;
        if (!active) spd *= 2;
        if (delay > 0 && active) yield return new WaitForSeconds(delay);
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

            if (done && Vector3.Distance(transform.localPosition, targetPos) < 1f) {
                fading = false;
                if (!active) gameObject.SetActive(false);
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

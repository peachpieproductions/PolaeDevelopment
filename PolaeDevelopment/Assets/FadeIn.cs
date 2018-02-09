using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public Vector2 offset;
    public float delay;
    public float speed = 1;
    public bool affectChildren;
    public AudioClip soundEffect;
    [Range(0f, 1f)]
    public float soundVolume = 1;
    Image imageComponent;
    Text textComponent;
    

	// Use this for initialization
	void Start () {

        //children
        if (affectChildren) {
            foreach(Transform t in transform) {
                FadeIn scr = t.GetComponent<FadeIn>();
                if (scr == null) {
                    scr = t.gameObject.AddComponent<FadeIn>();
                    scr.delay = delay;
                    scr.speed = speed;
                }
            }
        }

        imageComponent = GetComponent<Image>();
        textComponent = GetComponent<Text>();
        StartCoroutine(Fade());
    }

    IEnumerator Fade() {
        Color col = Color.white;
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
        
        if (delay > 0) yield return new WaitForSeconds(delay);
        var targetPos = transform.localPosition;
        transform.localPosition += (Vector3)offset;

        if (soundEffect != null) AudioManager.am.PlayClip(soundEffect,false,soundVolume);
        
        while (col.a < 1 || transform.localPosition != targetPos) {
            col.a += .01f * speed;
            if (imageComponent != null) imageComponent.color = col;
            if (textComponent != null) textComponent.color = col;

            //pos
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, .05f * speed);

            yield return null;
        }
    }
}

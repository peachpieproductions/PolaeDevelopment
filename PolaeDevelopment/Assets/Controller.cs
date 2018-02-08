using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
    [Header("Put Panos Here")]
    public Texture[] Panoramas;

    [Header("References")]
    public GameObject invertedSphere;
    MeshRenderer mr;

    int currentPano;

    private void Start() {
        mr = invertedSphere.GetComponent<MeshRenderer>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            GoToNext();
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            GoToPrev();
        }
    }

    public void GoToNext() {
        if (invertedSphere.activeSelf) {
            currentPano++;
            if (currentPano == Panoramas.Length) currentPano = 0;
        } else invertedSphere.SetActive(true);
        mr.material.mainTexture = Panoramas[currentPano];
    }

    public void GoToPrev() {
        if (invertedSphere.activeSelf) {
            currentPano--;
            if (currentPano < 0) currentPano = Panoramas.Length-1;
        } else invertedSphere.SetActive(true);
        mr.material.mainTexture = Panoramas[currentPano];
    }

}

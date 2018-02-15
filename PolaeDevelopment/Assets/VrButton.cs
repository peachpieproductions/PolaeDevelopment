using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class VrButton : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData pointerEventData) {
        StartCoroutine(SwitchToVR());
    }

    IEnumerator SwitchToVR() {

        string[] devices = new string[] { "daydream", "Oculus" };
        XRSettings.LoadDeviceByName(devices);

        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
        yield return null;

        // Now it's ok to enable VR mode.
        XRSettings.enabled = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Navigate : MonoBehaviour, IPointerClickHandler {

    public int goToPage;

    public void OnPointerClick(PointerEventData pointerEventData) {
        var img = GetComponent<Image>(); if (img != null) { if (img.color.a < .9f) return; }
        AppUIController.uic.GoToPage(goToPage);
    }

}

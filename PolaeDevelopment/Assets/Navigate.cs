using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Navigate : MonoBehaviour, IPointerClickHandler {

    public bool backButton;
    public int goToPage;

    public void OnPointerClick(PointerEventData pointerEventData) {
        var img = GetComponent<Image>(); if (img != null) { if (img.color.a < .9f) return; }
        if (backButton) {
            var getFade = transform.parent.GetComponent<FadeUI>();
            if (getFade != null) getFade.StartFade(false);
        } else AppUIController.uic.GoToPage(goToPage);
    }

}

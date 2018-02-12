using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppUIController : MonoBehaviour {

    public static AppUIController uic;
    public GameObject[] pages;
    public int currentPage;
    public int lastPage;

    private void Start() {
        uic = GetComponent<AppUIController>();
    }

    public void GoToPage(int i) {

        lastPage = currentPage;
        currentPage = i;
        if (!pages[i].activeSelf) pages[i].SetActive(true);
        var  getFade = pages[i].GetComponent<FadeUI>();
        if (getFade != null) getFade.StartFade(true); //fade in new page
        pages[i].transform.SetAsLastSibling();
    }



}

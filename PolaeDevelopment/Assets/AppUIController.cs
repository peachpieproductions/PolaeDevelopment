using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppUIController : MonoBehaviour {

    public static AppUIController uic;
    public GameObject[] pages;
    public int currentPage;
    public bool showPanoStill;
    public GameObject canvas;
    public Image whiteWashScreen;
    public Transform invertedSphere;
    Vector2 invertedSphereSpeed; //x , y
    public bool showingSplashScreen;

    [Header("UI")]
    public GameObject vrButton;

    private void Start() {
        Application.targetFrameRate = 60;
        uic = GetComponent<AppUIController>();
    }

    private void Update() {
        if (showPanoStill) {
            if (Input.GetMouseButton(0)) {
                invertedSphereSpeed.x = -Input.GetAxis("Mouse X");
                invertedSphereSpeed.y = Input.GetAxis("Mouse Y");
            }
            if (invertedSphereSpeed.x != 0) invertedSphereSpeed.x = Mathf.Lerp(invertedSphereSpeed.x, 0, .1f);
            if (invertedSphereSpeed.y != 0) invertedSphereSpeed.y = Mathf.Lerp(invertedSphereSpeed.y, 0, .1f);
            Camera.main.transform.Rotate(invertedSphereSpeed.y, invertedSphereSpeed.x, 0);
            Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0);
        }
    }

    public void GoToPage(int i) {
        if (currentPage == i) return;
        //var getFade = pages[currentPage].GetComponent<FadeUI>();
        //if (getFade != null) getFade.StartFade(false); //fade out current
        currentPage = i;
        if (!pages[i].activeSelf) pages[i].SetActive(true);
        var getFade = pages[i].GetComponent<FadeUI>();
        if (getFade != null) getFade.StartFade(true); //fade in new page
        pages[i].transform.SetAsLastSibling();
    }

    public void ShowPanoStill() {
        StartCoroutine(ExperienceTransistion(1));
    }

    public void ExitPanoStill() {
        StartCoroutine(ExperienceTransistion(0));
    }

    public IEnumerator ExperienceTransistion(int type) {
        StartCoroutine(WhiteWash());
        yield return new WaitForSeconds(1f);
        switch(type) {
            case 0:
                vrButton.SetActive(false);
                canvas.SetActive(true);
                showPanoStill = false;
                invertedSphere.gameObject.SetActive(false);
                break;
            case 1:
                vrButton.SetActive(true);
                canvas.SetActive(false);
                showPanoStill = true;
                invertedSphere.gameObject.SetActive(true);
                invertedSphere.GetComponent<MeshRenderer>().material.mainTexture = uic.pages[currentPage].GetComponent<Node>().panorama360;
                break;
        }
    }

    public IEnumerator WhiteWash() {
        whiteWashScreen.gameObject.SetActive(true);
        var col =  whiteWashScreen.color;
        col.a = 0;
        whiteWashScreen.color = col;
        while (true) {
            while (col.a < 1) {
                col.a += .05f;
                whiteWashScreen.color = col;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            while (col.a > 0) {
                col.a -= .025f;
                whiteWashScreen.color = col;
                yield return null;
            }
            whiteWashScreen.gameObject.SetActive(false);
            break;
        }
    }

    



}

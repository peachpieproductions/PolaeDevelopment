using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Node : MonoBehaviour {

    [Header("Node Data")]
    public bool active;
    public int nodeId;
    public int[] nodesUnlocked;
    public string titleText;
    public string logLineText;
    [TextArea(3, 10)]
    public string[] descriptionText;
    [TextArea(3, 10)]
    public string[] articleText;
    public Vector3 date;
    public Image image;
    public Texture panorama360;
    public VideoClip videoMedia;
    public GameObject arMedia;
    public GameObject vrMedia;
    Transform texts;

    private void Start() {
        texts = transform.Find("Texts");
        if (texts != null) {
            texts.GetChild(0).GetComponent<Text>().text = titleText;
            texts.GetChild(1).GetComponent<Text>().text = logLineText;
            texts.GetChild(2).GetComponent<Text>().text = descriptionText[0];
            texts.GetChild(3).GetComponent<Text>().text = articleText[0];
        }
    }






}

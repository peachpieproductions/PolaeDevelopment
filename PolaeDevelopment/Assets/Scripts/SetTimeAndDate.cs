using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTimeAndDate : MonoBehaviour {

    Text timeText;
    Text dateText;
    Vector3 timeTextTargetPos;

    // Use this for initialization
    void Awake () {
        timeText = GetComponent<Text>();
        dateText = transform.GetChild(0).GetComponent<Text>();
        timeTextTargetPos = timeText.transform.localPosition;

        //date
        if (System.DateTime.Now.Day > 9) dateText.text = System.DateTime.Now.ToString("dddd, MMMM dd");
        else dateText.text = System.DateTime.Now.ToString("dddd, MMMM d");


        //string monthVar = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"); print(monthVar);
        //StartCoroutine(UpdateTime());
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnEnable() {
        StartCoroutine(UpdateTime());
    }

    IEnumerator FadeIn() {
        transform.localPosition += new Vector3(0, 96);
        var col = timeText.color;
        col.a = 0;
        var col2 = dateText.color;
        col2.a = -.5f;
        while (col2.a < 1) {
            col.a += .01f;
            col2.a += .0075f;
            timeText.color = col;
            dateText.color = col2;

            //pos
            var pos = transform.localPosition;
            pos.y = Mathf.Lerp(pos.y, timeTextTargetPos.y, .05f);
            transform.localPosition = pos;
            yield return null;
        }
    }

    IEnumerator UpdateTime() {
        while (true) {
            int hour = System.DateTime.Now.Hour;
            if (hour > 9 && hour < 13 || hour > 21) timeText.text = System.DateTime.Now.ToString("hh:mm");
            else timeText.text = System.DateTime.Now.ToString("h:mm");
            yield return new WaitForSeconds(3f);
        }
    }
}

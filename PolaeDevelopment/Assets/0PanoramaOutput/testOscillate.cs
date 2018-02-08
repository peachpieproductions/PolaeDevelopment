using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testOscillate : MonoBehaviour {

    Vector3 startPos;
    float osc;
    float offset;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
        offset = Random.Range(0, 1000);
	}
	
	// Update is called once per frame
	void Update () {
        osc = Mathf.Sin(Time.time * 2 + offset);
        transform.position = startPos + new Vector3(0,osc);
	}
}

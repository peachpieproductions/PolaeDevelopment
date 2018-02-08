using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float speed = 8;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float spd = speed;
        if (Input.GetKey(KeyCode.LeftShift)) spd *= 2;

        if (Input.GetKey(KeyCode.W)) {
            transform.parent.position += transform.forward * Time.deltaTime * spd;
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.parent.position += transform.forward * -1f * Time.deltaTime * spd;
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.parent.position += transform.right * -1f * Time.deltaTime * spd;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.parent.position += transform.right * Time.deltaTime * spd;
        }
        if (Input.GetKey(KeyCode.Q)) {
            transform.parent.position += transform.up * -1f * Time.deltaTime * spd;
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.parent.position += transform.up * Time.deltaTime * spd;
        }

    }
}

using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    Transform childCamera;

	// Use this for initialization
	void Start () {
        childCamera = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (horizontal != 0) {
            this.transform.Translate(this.transform.right * horizontal);
        }

        if(vertical != 0) {
            this.transform.Translate(this.transform.forward * vertical);
        }

        if (scroll != 0) {
            this.transform.Translate(this.childCamera.forward * scroll);
        }
    }
}

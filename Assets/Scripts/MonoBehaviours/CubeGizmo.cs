using UnityEngine;
using System.Collections;

public class CubeGizmo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos() {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 1, 1, 1F);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}

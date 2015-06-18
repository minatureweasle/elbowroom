using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	Vector3 forward;

	void Start () {
		forward = new Vector3 (0, 0, 20);
	}

	void Update () {
		if (Input.GetKey (KeyCode.W)) 
		{
			GetComponent<Rigidbody>().AddForce(forward);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			GetComponent<Rigidbody>().AddForce(-forward);
		}
	}
}

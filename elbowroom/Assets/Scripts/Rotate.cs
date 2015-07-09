using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.LeftArrow)){

			transform.Rotate(new Vector3(0,90f,0));

		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)){

			transform.Rotate(new Vector3(0,-90f,0));

		}

	}
}

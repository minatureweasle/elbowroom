using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){

		if (collider.tag == "Player") {
			collider.GetComponent<EbbController>().AllowWallJump();
		}

	}
}

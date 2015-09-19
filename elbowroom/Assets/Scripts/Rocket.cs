using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour {

	public float speed = 50f;

    //move in the forward direction of this rocket's transform
	void Update () {
		GetComponent<Rigidbody>().velocity = transform.up*speed;
	}
}

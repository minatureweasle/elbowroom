using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour {

	public float speed = 50f;

	void Update () {
		GetComponent<Rigidbody>().velocity = transform.up*speed;
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour {

	public static float speed = 50f;

	void Start () {
	
	}

	void Update () {
		GetComponent<Rigidbody>().velocity = transform.up*speed;
	}
}

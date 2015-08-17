using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	public float rotateSpeed = 100;

	public enum Axis {X, Y, Z};

	public Axis rotationAxis = Axis.Y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 axis = transform.up;
		if (rotationAxis == Axis.X)
			axis = transform.right;
		else if (rotationAxis == Axis.Z)
			axis = transform.forward;

		transform.RotateAround (transform.position, axis, rotateSpeed * Time.deltaTime);
	}
}

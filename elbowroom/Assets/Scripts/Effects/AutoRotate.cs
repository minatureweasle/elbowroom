using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	public enum Axis {X, Y, Z};
	public Axis rotationAxis = Axis.Y;

	public float rotateSpeed = 100;

	Vector3 axisVector;

	void Start(){
		SetAxis ();
	}

	void Update () {
		transform.RotateAround (transform.position, axisVector, rotateSpeed * Time.deltaTime);
	}

	//set the axis that this object will rotate around
	void SetAxis(){
		Vector3 axisVector = transform.up;
		if (rotationAxis == Axis.X)
			axisVector = transform.right;
		else if (rotationAxis == Axis.Z)
			axisVector = transform.forward;
	}
}

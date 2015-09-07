using UnityEngine;
using System.Collections;

public class PulsingLight : MonoBehaviour {

	float startingIntensity;

	void Start () {
		startingIntensity = GetComponent<Light> ().intensity;
	}

	//continuously set the light's intensity to a follow a Sine wave
	void Update () {
		GetComponent<Light> ().intensity = startingIntensity + Mathf.Sin(Time.time)/3;
	}
}

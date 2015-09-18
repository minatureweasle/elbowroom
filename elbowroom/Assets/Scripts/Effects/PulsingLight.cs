using UnityEngine;
using System.Collections;

public class PulsingLight : MonoBehaviour {

	float startingIntensity;

    Light light;

	void Start () {
        light = GetComponent<Light>();
        startingIntensity = light.intensity;
	}

	//continuously set the light's intensity to a follow a Sine wave
	void Update () {
        light.intensity = startingIntensity + Mathf.Sin(Time.time) / 3;
	}
}

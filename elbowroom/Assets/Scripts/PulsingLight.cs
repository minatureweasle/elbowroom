using UnityEngine;
using System.Collections;

public class PulsingLight : MonoBehaviour {

	float startingIntensity;

    Light myLight;

    //cache the this object's Light component and store its starting intensity
	void Start () {
        myLight = GetComponent<Light>();
        startingIntensity = myLight.intensity;
	}

	//continuously set the light's intensity to a follow a Sine wave
	void Update () {
        myLight.intensity = startingIntensity + Mathf.Sin(Time.time) / 3;
	}
}

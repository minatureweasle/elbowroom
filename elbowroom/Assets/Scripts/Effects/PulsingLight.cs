using UnityEngine;
using System.Collections;

public class PulsingLight : MonoBehaviour {

	float startingIntensity;

	float offset = 0;

	// Use this for initialization
	void Start () {
		startingIntensity = GetComponent<Light> ().intensity;
	}
	
	// Update is called once per frame
	void Update () {
		offset += Random.Range (-0.05f, 0.05f);
		GetComponent<Light> ().intensity = startingIntensity + Mathf.Sin(Time.time + offset)/3;
	}
}

using UnityEngine;

public class Segment : MonoBehaviour {

	//All the prefabs that can spawn after this one
	public GameObject [] nextPrefabs;

	//Manually set the length of this segment
	public float segmentLength = 0;

	public float GetSegmentLength(){
		if (segmentLength == 0)
			return GetComponent<Renderer>().bounds.size.z;
		else
			return segmentLength;
	}

	public float GetSegmentWidth(){
		return GetComponentInChildren<Renderer>().bounds.size.x;
	}

	void Start () {
	
	}
	
	void Update () {
	}
}

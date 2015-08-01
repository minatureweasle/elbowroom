using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour {

	public int room = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			Application.LoadLevel(room);
		}
	}
}

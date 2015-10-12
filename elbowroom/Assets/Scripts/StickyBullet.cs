using UnityEngine;
using System.Collections;

public class StickyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				transform.GetComponent<Rocket>().isStuck = true;
				Destroy(GetComponent<Rigidbody>()); 
				transform.parent = col.transform;
				//Destroy(GetComponent<Rocket>());
			
	}
}

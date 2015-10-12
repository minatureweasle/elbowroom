using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public float speed = 50f;

	float momentOfDestruction;
	public float lifetime = 5;
	public bool isStuck = false;

	void Start(){
		momentOfDestruction = Time.time + lifetime;
	}
    //move in the forward direction of this rocket's transform
	void Update () {
		if (isStuck){
			stick();
		}
		else{
			propel();
		}
	}

	void stick(){
		if (Time.time >= momentOfDestruction)
			Destroy(gameObject);
	}

	void propel(){
		GetComponent<Rigidbody>().velocity = transform.up*speed;
		if (Time.time >= momentOfDestruction)
			Destroy(gameObject);

	}
}

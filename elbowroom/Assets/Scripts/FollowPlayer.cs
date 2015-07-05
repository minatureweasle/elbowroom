using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject playerToFollow;

	void Start () {
	
	}

	void Update () {

		transform.position = new Vector3(transform.position.x, transform.position.y, playerToFollow.transform.position.z - 8);
	}
}

using UnityEngine;
using System.Collections;

public class PlockMove : MonoBehaviour {

	public float speed;
	public GameObject[] waypoints;
	private int currentWaypoint;
	// Use this for initialization
	void Start () {
		currentWaypoint = 0;
		transform.LookAt (waypoints[currentWaypoint].transform.position);
	}
	// Update is called once per frame
	void Update () {
		if (PastWaypoint()) {
			GetNextWaypoint ();
		} else {
			transform.position += transform.forward * Time.deltaTime * speed;
		}
	}
	void GetNextWaypoint(){
		currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
		transform.LookAt (waypoints[currentWaypoint].transform.position);
	}
	bool PastWaypoint(){
		Vector3 vectorToCurrentWaypoint = waypoints[currentWaypoint].transform.position - transform.position;
		if (Vector3.Dot (vectorToCurrentWaypoint, transform.forward) <= 0) {
			return true;
		}
		else{
			return false;
			}
	}
}

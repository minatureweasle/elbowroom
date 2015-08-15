using UnityEngine;
using System.Collections;

public class ObstructionTrap : MonoBehaviour {

	public static float maxDistance = 5;

	public static float maxHeight = 0;

	Transform players;

	void Start () {

		players = GameObject.Find("Players").transform;

	}

	void Update () {

		//GameObject closestPlayer = players.playerArray [0];
		float distanceToClosestPlayer = Mathf.Infinity;

		//find closest player
		for (int i = 0; i < players.childCount; i++) {
			float thisDistance = (players.GetChild(i).position - transform.position).magnitude;
			if (thisDistance < maxDistance)
			{
				if (thisDistance < distanceToClosestPlayer)
				{
					//closestPlayer = players.playerArray[i];
					distanceToClosestPlayer = thisDistance;
				}
			}

		}
		//rise and fall
		if (distanceToClosestPlayer != Mathf.Infinity) {
			Vector3 newPosition = transform.position;

			newPosition.y = Mathf.Clamp (newPosition.y + 0.1f / distanceToClosestPlayer, -2.9f, maxHeight); //5 - distanceToClosestPlayer;

			transform.position = newPosition;
		} 
		else {
			Vector3 newPosition = transform.position;
			
			newPosition.y = Mathf.Clamp (newPosition.y - 0.1f, -2.9f, maxHeight); //5 - distanceToClosestPlayer;
			
			transform.position = newPosition;

		}

		//Debug.Log (distanceToClosestPlayer);
	
	}

}

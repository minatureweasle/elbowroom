using UnityEngine;
using System.Collections;

public class WallTrap : MonoBehaviour {

	float timeOfNextWallChange = Mathf.Infinity;

	public float wallChangeInterval = 1;

	public bool fallHalfway = false;

	public Transform [] walls;

	bool [] wallRising;

	int currentWall = 0;



	void Start () {
		wallRising = new bool[walls.Length];
		for (int i = 0; i < wallRising.Length; i++) {
			wallRising[i] = true;
		}

		wallRising [0] = false;

		timeOfNextWallChange = Time.time + wallChangeInterval;
	}

	void Update () {

		if (Time.time > timeOfNextWallChange) {

			//the current wall rises, the next wall falls
			wallRising[currentWall] = true;
			currentWall = (currentWall + 1)%walls.Length;
			wallRising[currentWall] = false;

			//schedule the next time the above will happen
			timeOfNextWallChange = Time.time + wallChangeInterval;
		}

		for (int i = 0; i < walls.Length; i++) {
			if (wallRising[i]){
				Vector3 destination = walls[i].localPosition;
				destination.y = 0;
				walls[i].localPosition = Vector3.Lerp(walls[i].localPosition, destination, 10f*Time.deltaTime);
			}
			else{
				Vector3 destination = walls[i].localPosition;
				destination.y = -6;
				if (fallHalfway)
					destination.y = -3;
				walls[i].localPosition = Vector3.Lerp(walls[i].localPosition, destination, 10f*Time.deltaTime);
			}
		}

	
	}
}

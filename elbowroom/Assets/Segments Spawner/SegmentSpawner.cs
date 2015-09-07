using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SegmentSpawner : MonoBehaviour {


	public GameObject [] segmentPrefabs;

	public GameObject endPrefab;

	float accumulatedLength = 0;

	public int numSegmentsToSpawn = 10;


	public float minimumLengthAhead = 20f;

	public float minimumLengthBehind = 20f;

	float furthestPositionBack = 0;

	//always stored in advance
	GameObject nextPrefab;

	int currentLane = 0;

	public float spaceBetweenSegments = 0.1f;

	void Start () {

		//first select randomly a prefab from this array. 
		//From then on, select only from the segment prefabs in the chosen segment
		nextPrefab = segmentPrefabs[Random.Range (0, segmentPrefabs.Length)];

		while (numSegmentsToSpawn > 0) {

			SpawnStoredSegment ();

		}

		SpawnEndSegment ();


	}
	
	void Update () {
	}

	void SpawnStoredSegment(){
		//spawn the stored prefab
		GameObject segmentGO = (GameObject)Instantiate(nextPrefab, transform.position + Vector3.forward*accumulatedLength, Quaternion.identity);

		//get the segment component of it
		Segment segment = segmentGO.GetComponent<Segment> ();

		//move the component ahead half its own length so that its back is at end of the previous segment
		segmentGO.transform.position += Vector3.forward*(segment.GetSegmentLength () / 2);

		//move the segments left or right and make sure they are always reachable from the previous one
		currentLane += Random.Range (-1, 2);
		currentLane = Mathf.Clamp (currentLane, -1, 1);
		segmentGO.transform.position += Vector3.right*(segment.GetSegmentWidth ()*currentLane);

		//if no subsequent prefabs have been chosen for this segment, select one at random
		if (segment.nextPrefabs.Length == 0) {

			//get a random index for the following prefab
			int nextPrefabIndex = Random.Range (0, segmentPrefabs.Length);

			//store the prefab that will be used next (randomly chosen from among all known segment prefabs)
			nextPrefab = segmentPrefabs [nextPrefabIndex];

		} else {

			//get a random index for the following prefab
			int nextPrefabIndex = Random.Range (0, segment.nextPrefabs.Length);

			//store the prefab that will be used next (randomly chosen from among the current segment's next prefabs)
			nextPrefab = segment.nextPrefabs [nextPrefabIndex];
		}

		//add the length of this segment to the accumulated length
		accumulatedLength += segment.GetSegmentLength () + spaceBetweenSegments;

		numSegmentsToSpawn--;

	}

	void SpawnEndSegment(){
		//spawn the stored prefab
		GameObject segmentGO = (GameObject)Instantiate(endPrefab, transform.position + Vector3.forward*accumulatedLength, Quaternion.identity);

		//move the component ahead half its own length so that its back is at end of the previous segment
		segmentGO.transform.position += Vector3.forward*(15);
		
		//move the segments left or right and make sure they are always reachable from the previous one
		currentLane += Random.Range (-1, 2);
		currentLane = Mathf.Clamp (currentLane, -1, 1);
		segmentGO.transform.position += Vector3.right*(10*currentLane);
	}
}

using UnityEngine;
using System.Collections;

public class CubeField : MonoBehaviour {

	public GameObject cubePrefab;

	public int rows = 6;//z
	public int columns = 10;//x

	Transform[,] cubes;

	void Start () {

		cubes = new Transform[rows,columns];

		float cubeWidth = cubePrefab.GetComponent<Renderer> ().bounds.size.x;
		float cubeHeight = cubePrefab.GetComponent<Renderer> ().bounds.size.z;

		for (int z = 0; z < rows; z++) {
			for (int x = 0; x < columns; x++) {
				GameObject cube = (GameObject)Instantiate(cubePrefab, transform.position + new Vector3(x*cubeWidth, 0, z*cubeHeight), Quaternion.identity);
				cubes[z,x] = cube.transform;
				cube.transform.SetParent(transform);
			}
		}
	
	}

	void Update () {
		for (int z = 0; z < rows; z++) {
			for (int x = 0; x < columns; x++) {
				cubes[z,x].transform.position = new Vector3(cubes[z,x].transform.position.x, Mathf.Sin((x + Time.time*10)*0.5f)*0.6f, cubes[z,x].transform.position.z);
			}
		}
	}
}

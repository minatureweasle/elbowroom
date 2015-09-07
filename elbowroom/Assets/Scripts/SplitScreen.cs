using UnityEngine;
using System.Collections;

public class SplitScreen : MonoBehaviour {

	public enum ViewType {ALL, LEFT, RIGHT, TOP, BOTTOM}
	public ViewType myViewType;

	public float spacing = 0.01f;

	Camera myCamera;

	void Start () {

		myCamera = GetComponent<Camera> ();

		Rect newRect = myCamera.rect;

		//set the camera to fill only half of the screen
		if (myViewType == ViewType.LEFT) {
			newRect.xMax = 0.5f - spacing;
		}
		else if (myViewType == ViewType.RIGHT) {
			newRect.xMin = 0.5f + spacing;
		}
		else if (myViewType == ViewType.TOP) {
			newRect.yMin = 0.5f + spacing;
		}
		else if (myViewType == ViewType.BOTTOM) {
			newRect.yMax = 0.5f - spacing;
		}

		myCamera.rect = newRect;
	
	}
}

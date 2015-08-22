using UnityEngine;
using System.Collections;

public class SplitScreen : MonoBehaviour {

	public enum ViewType {ALL, LEFT, RIGHT, TOP, BOTTOM}
	public ViewType myViewType;

	Camera myCamera;

	void Start () {

		myCamera = GetComponent<Camera> ();

		Rect newRect = myCamera.rect;

		if (myViewType == ViewType.LEFT) {
			newRect.xMax = 0.49f;
		}
		else if (myViewType == ViewType.RIGHT) {
			newRect.xMin = 0.51f;
		}
		else if (myViewType == ViewType.TOP) {
			newRect.yMin = 0.5f;
		}
		else if (myViewType == ViewType.BOTTOM) {
			newRect.yMax = 0.5f;
		}

		myCamera.rect = newRect;
	
	}
}

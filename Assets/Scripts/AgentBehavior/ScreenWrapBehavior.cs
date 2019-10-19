using UnityEngine;

public class ScreenWrapBehavior : MonoBehaviour {
	private Vector3 screenBottomLeft;
	private Vector3 screenTopRight;
	private Camera cam;
	[Range(0, 2)] public float falloff;
	
	void Start() {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update() {
		var position = transform.position;
		screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, position.z));
		screenBottomLeft.x -= falloff;
		screenBottomLeft.y -= falloff;
		screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, position.z));
		screenTopRight.x += falloff;
		screenTopRight.y += falloff;
		
		var newPosition = position;
		if (transform.position.x < screenBottomLeft.x || transform.position.x > screenTopRight.x) {
			newPosition.x = -newPosition.x;
			newPosition.x = Mathf.Max(newPosition.x, screenBottomLeft.x);
			newPosition.x = Mathf.Min(newPosition.x, screenTopRight.x);
		}
		if (transform.position.y < screenBottomLeft.y || transform.position.y > screenTopRight.y) {
			newPosition.y = -newPosition.y;
			newPosition.y = Mathf.Max(newPosition.y, screenBottomLeft.y);
			newPosition.y = Mathf.Min(newPosition.y, screenTopRight.y);
		}
		transform.position = newPosition;
	}
}

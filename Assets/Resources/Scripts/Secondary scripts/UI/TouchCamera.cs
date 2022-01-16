// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchCamera : MonoBehaviour {
	Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	float oldTouchDistance;

	public float LastOrthoSize;
	//Zoom on PC
	float cameraDistanceMax = 20f;
	float cameraDistanceMin = 5f;
	float cameraDistance = 10f;
	float scrollSpeed = 0.5f;

	float speed = 0.05f;
	void Start(){
	}
	void Update() {
		if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
			return;
		}
		if ((SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor") && Camera.main.GetComponent<CameraFollow>().editplay!=0)) {
			return;
		}

		//Zoom on computer
		if (!Application.isMobilePlatform) {

			LastOrthoSize = Camera.main.orthographicSize;
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				if (Camera.main.orthographicSize >= 2f) {
					Camera.main.orthographicSize -= 1f;
					Camera.main.GetComponent<CameraFollow> ().camsizeInt = Mathf.RoundToInt (Camera.main.orthographicSize);
					GetComponent<CameraFollow> ().ParallaxParent.GetComponent<Transform> ().localScale *= Camera.main.orthographicSize / LastOrthoSize;
					//speed -= 0.05f;
				}
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				if (Camera.main.orthographicSize <= 19f) {
					Camera.main.orthographicSize += 1f;
					Camera.main.GetComponent<CameraFollow> ().camsizeInt = Mathf.RoundToInt (Camera.main.orthographicSize);
					GetComponent<CameraFollow> ().ParallaxParent.GetComponent<Transform> ().localScale *= Camera.main.orthographicSize / LastOrthoSize;
					//speed += 0.05f;
				}
			}

			// set camera position
			if (Input.GetMouseButton (0) && GetComponent<CameraFollow> ().panning) {
				if (!IsPointerOverUIObject ()) {
					//No panning if point is over UI
					transform.position -= new Vector3 (Input.GetAxisRaw ("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * speed, 0f);
				}
			}


		} else if (Application.isMobilePlatform) {
			//Zoom on mobile
			if (Input.touchCount == 0) {
				oldTouchPositions [0] = null;
				oldTouchPositions [1] = null;
			} else if (Input.touchCount == 1) {
				if (oldTouchPositions [0] == null || oldTouchPositions [1] != null) {
					oldTouchPositions [0] = Input.GetTouch (0).position;
					oldTouchPositions [1] = null;
				} else {
					Vector2 newTouchPosition = Input.GetTouch (0).position;

					if (Camera.main.GetComponent<CameraFollow> ().panning) {
						if (!IsPointerOverUIObject ()) {
							//No panning if point is over UI
							transform.position += transform.TransformDirection ((Vector3)((oldTouchPositions [0] - newTouchPosition) * Camera.main.orthographicSize / Camera.main.pixelHeight * 2f));
						}
					}

					oldTouchPositions [0] = newTouchPosition;
				}
			} else {
				if (oldTouchPositions [1] == null) {
					oldTouchPositions [0] = Input.GetTouch (0).position;
					oldTouchPositions [1] = Input.GetTouch (1).position;
					oldTouchVector = (Vector2)(oldTouchPositions [0] - oldTouchPositions [1]);
					oldTouchDistance = oldTouchVector.magnitude;
				} else {
					Vector2 screen = new Vector2 (Camera.main.pixelWidth, Camera.main.pixelHeight);
				
					Vector2[] newTouchPositions = {
						Input.GetTouch (0).position,
						Input.GetTouch (1).position
					};
					Vector2 newTouchVector = newTouchPositions [0] - newTouchPositions [1];
					float newTouchDistance = newTouchVector.magnitude;

					if (Camera.main.GetComponent<CameraFollow> ().panning) {
						if (!IsPointerOverUIObject ()) {
							//No panning if point is over UI
							transform.position += transform.TransformDirection ((Vector3)((oldTouchPositions [0] + oldTouchPositions [1] - screen) * Camera.main.orthographicSize / screen.y));
							//transform.localRotation *= Quaternion.Euler (new Vector3 (0, 0, Mathf.Asin (Mathf.Clamp ((oldTouchVector.y * newTouchVector.x - oldTouchVector.x * newTouchVector.y) / oldTouchDistance / newTouchDistance, -1f, 1f)) / 0.0174532924f));
						}
					}

					LastOrthoSize = Camera.main.orthographicSize;

					//max is 20f
					if (Camera.main.orthographicSize * (oldTouchDistance / newTouchDistance) <= 20f) {
						Camera.main.orthographicSize *= oldTouchDistance / newTouchDistance;
					} else {
						Camera.main.orthographicSize = 20f;
					}
					Camera.main.GetComponent<CameraFollow> ().camsizeInt = Mathf.RoundToInt (Camera.main.orthographicSize);
					GetComponent<CameraFollow>().ParallaxParent.GetComponent<Transform> ().localScale *= Camera.main.orthographicSize / LastOrthoSize;

					if (Camera.main.GetComponent<CameraFollow> ().panning) {
						if (!IsPointerOverUIObject ()) {
							//No panning if point is over UI
							transform.position -= transform.TransformDirection ((newTouchPositions [0] + newTouchPositions [1] - screen) * Camera.main.orthographicSize / screen.y);
						}
					}
					oldTouchPositions [0] = newTouchPositions [0];
					oldTouchPositions [1] = newTouchPositions [1];
					oldTouchVector = newTouchVector;
					oldTouchDistance = newTouchDistance;
				}
			}
		}
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

		List<RaycastResult> tempresults2 = new List<RaycastResult>();

		foreach (RaycastResult obj in results) {
			if (obj.gameObject.tag != "chatUI") {
				//Ignore chat top panel
				tempresults2.Add(obj);
			}
		}
		return tempresults2.Count > 0;
	}
}

using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

    void Update()
    {
		//Exit if camera or the component does not exist
		if (Camera.main == null) {
			return;
		}
		if (Camera.main != null) {
			if (Camera.main.GetComponent<CameraFollow> () == null) {
				return;
			}
		}

        transform.LookAt(Camera.main.transform);
    }
}
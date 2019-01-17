using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamWatch : MonoBehaviour 
{
	public Transform watchAt;

	void Update()
	{
		if (watchAt != null)
			transform.position = new Vector3 (watchAt.position.x, CamController.instance.distance.y, watchAt.position.z);
	}
}

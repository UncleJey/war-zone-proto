using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour 
{
	public Vector3 distance;
	Camera _cam;
	public static CamController instance;

	void Awake()
	{
		instance = this;
		_cam = GetComponentInChildren<Camera> ();
	}

	public static void MoveTo(Vector3 pPoint)
	{
		instance.transform.position = new Vector3 (pPoint.x, instance.distance.y, pPoint.z);
	}

	public void Rotate()
	{
		
	}

	public static void ChangeDistance(float dX)
	{
		instance.distance.y += dX;
		if (instance.distance.y < instance.distance.x)
			instance.distance.y = instance.distance.x;
		else if (instance.distance.y > instance.distance.z)
			instance.distance.y = instance.distance.z;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLimit : MonoBehaviour 
{
	public Vector2 limits;

	void LateUpdate()
	{
		bool changed = false;
		float rx = transform.localRotation.eulerAngles.x;
		if (rx > 180)
			rx -= 360;

		float rz = transform.localRotation.eulerAngles.z;
		if (rz > 180)
			rz -= 360;

		if (rx > limits.y) 
		{
			rx = limits.y - 5;
			changed = true;
		}
		else if (rx < limits.x) 
		{
			rx = limits.x + 5;
			changed = true;
		}

		if (rz > limits.y) 
		{
			rz = limits.y - 5;
			changed = true;
		}
		else if (rz < limits.x) 
		{
			rz = limits.x + 5;
			changed = true;
		}

		if (changed)
			transform.localRotation = Quaternion.Euler (rx, transform.localRotation.eulerAngles.y, rz);
	}
}

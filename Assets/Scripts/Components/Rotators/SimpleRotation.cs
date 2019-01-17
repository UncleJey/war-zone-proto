using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour 
{
	public Vector3 speed;

	void Update()
	{
		transform.localRotation = Quaternion.Euler (transform.localRotation.eulerAngles + speed * Time.deltaTime);
	}
}

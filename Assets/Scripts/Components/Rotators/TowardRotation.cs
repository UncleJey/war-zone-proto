using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardRotation : MonoBehaviour 
{
	public bool ready = true;
	public float speed;
	Quaternion lookAt;
	bool intReady = false;
	public float delta = 10f;

	public void RotateTo(Vector3 pPoint)
	{
		Debug.Log ("rotate to " + pPoint);
		lookAt = Quaternion.LookRotation (transform.position - pPoint);
		ready = false;
		intReady = false;
	}

	void Update()
	{
		if (!intReady)
		{
			transform.rotation = Quaternion.LerpUnclamped (transform.rotation, lookAt, Time.deltaTime * speed);
			float dt = transform.rotation.eulerAngles.y - lookAt.eulerAngles.y;
			if (dt < 0)
				dt = -dt;
			
			ready = dt < delta;
			intReady = dt < 1f;
			if (ready)
				Debug.Log ("rotation done!");
		}
	}

}

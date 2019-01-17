using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunc : MonoBehaviour 
{
	public BodyRender body;
	public BodyClass data;

	public void Init(string pClassName)
	{
		data = Bodys.Get(pClassName);
		body.DoRender (data.model);

		//	Debug.LogError ("empty model " + pObject.ToString ());
	}
}

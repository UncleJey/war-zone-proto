using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Шасси
/// </summary>
public class Propulsion : MonoBehaviour 
{
	public BodyRender left;
	public BodyRender right;
	public PropulsionClass data;
	public PropModels extra;

	public void Init(string pClassName, PropModels pExtra)
	{
		extra = pExtra;
		data = Propulsions.Get(pClassName);
		string Lmodel = data.model;
		string Rmodel = string.Empty;// pObject.propstr(PropertyType.mountModel);

		if (extra != null)
		{
			if (!string.IsNullOrEmpty (extra.left))
				Lmodel = extra.left;
			else
				Debug.Log ("have no left prop");
			if (!string.IsNullOrEmpty (extra.right))
				Rmodel = extra.right;
			else
				Debug.Log ("have no right prop");
		}

		if (!string.IsNullOrEmpty (Lmodel))
			left.DoRender (Lmodel);
		else
			left.Clear (true);

		if (!string.IsNullOrEmpty (Rmodel))
			right.DoRender (Rmodel);
		else
			right.Clear (true);

		//	Debug.LogError ("empty model " + pObject.ToString ());
	}

	public void Clear()
	{
		left.Clear (true);
		right.Clear (true);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Рендер двухкомпонентного объекта для UI
/// </summary>
public class ComponentRenderer : MonoBehaviour 
{
	[SerializeField]
	private BodyRender fireWork;
	[SerializeField]
	private BodyRender fireBase;
	[SerializeField]
	private Transform _base;
	Vector3 center;

	public void Init(string pModel1, string pModel2)
	{
		center = Vector3.zero;
		Clear ();
		if (!string.IsNullOrEmpty (pModel1)) 
		{
			fireWork.DoRender (pModel1);
			center += fireWork.center * fireWork.scale / 100;
		}
		else
			fireWork.Clear (true);

		if (!string.IsNullOrEmpty (pModel2)) 
		{
			fireBase.DoRender (pModel2);
			center += fireBase.center * fireBase.scale / 100;
			center = center / 2;
		}
		else
			fireBase.Clear (true);

		center.z = 0;
		_base.transform.localPosition = -center;
	}

	public void Clear()
	{
		fireWork.Clear (true);
		fireBase.Clear (true);
	}
}

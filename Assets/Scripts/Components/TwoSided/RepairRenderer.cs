using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairRenderer : MonoBehaviour
{
	public BodyRender fireWork;
	public BodyRender fireBase;
	GunClass data;

	public void Init(string pClassName)
	{
		data = Guns.Get (pClassName);
		if (!string.IsNullOrEmpty (data.model))
			fireWork.DoRender (data.model);
		else
			fireWork.Clear (true);

		if (!string.IsNullOrEmpty (data.mountModel))
			fireBase.DoRender (data.mountModel);
		else
			fireBase.Clear (true);
	}

	public void Clear()
	{
		fireWork.Clear (true);
		fireBase.Clear (true);
	}


}

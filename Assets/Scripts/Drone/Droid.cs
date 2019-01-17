#define DEBUG_DROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorithms;

public class Droid : MonoBehaviour 
{
	public Trunc body;
	public Propulsion propulsion;

	public WeaponRenderer weaponRender;
	public SensorRenderer sensorRender;
	public ConstructRenderer constructRender;
	public RepairRenderer repairRender;

	Vector3[] connectors = null;

	/// <summary>
	/// Данные
	/// </summary>
	public UnitClass data;

	public void Init(string pClassName)
	{
		data = Units.Get (pClassName);

		// body
		body.Init (data.body);
		connectors = body.body.data.connector;

		if (!string.IsNullOrEmpty(data.propulsion))
			propulsion.Init (data.propulsion, body.data.GetExtraModel(data.propulsion));
		else
			propulsion.Clear ();

		if (connectors != null)
			weaponRender.transform.localPosition = connectors [0];

		// repair
		if (!string.IsNullOrEmpty (data.repair))
			repairRender.Init (data.repair);
		else
			repairRender.Clear ();

		// weapons 1 or 2
		if (data.weapons != null)
			weaponRender.Init (data.GetWeapon (0));
		else
			weaponRender.Clear ();

		//construct
		if (!string.IsNullOrEmpty (data.construct))
			constructRender.Init (data.construct);
		else
			constructRender.Clear ();

		//radar
		if (!string.IsNullOrEmpty (data.sensor))
			sensorRender.Init (data.sensor);
		else
			sensorRender.Clear ();

		//transform.localPosition = new Vector3(0,body.body.center.y,0);
	}
}

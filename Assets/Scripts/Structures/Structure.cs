//#define DEBUG_STRUC
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour 
{
	List<BodyRender> _renders = new List<BodyRender>();
	public BodyRender defRender;
	public ComponentRenderer weaponRender;
	public SimpleRotation simpleRot;
	//public Weapon weapon;
	//public Sensor sensor;

	JsonObject myData;
	Vector3[] connectors = null;

	BodyRender DoClone(string pObj)
	{
//		Debug.Log ("clone " + pObj);
		BodyRender tmpRender = Instantiate<BodyRender>(defRender);
		tmpRender.transform.parent = transform;
//		tmpRender.transform.localRotation = Quaternion.;
		tmpRender.transform.localPosition = Vector3.zero;
		tmpRender.DoRender (pObj);
		if (tmpRender.data != null && tmpRender.data.connector != null)
			connectors = tmpRender.data.connector;
		
		_renders.Add (tmpRender);
		return tmpRender;
	}

	public void Init(JsonObject pData)
	{
		DelRenders ();

		myData = pData;
		#if DEBUG_STRUC
		Debug.Log ("init " + pData.propstr (PropertyType.name) + "\r\n"+myData.ToString());
		#endif
		string _base = myData.propstr (PropertyType.baseModel);
		JsonArray _aweapons = myData.JsonArray (PropertyType.weapons);
		string _weapon = _aweapons==null?null:_aweapons.First ().ToString(); //TODO: второе добавить
		string _sensor = myData.propstr (PropertyType.sensorID);

		JsonArray _body = myData.JsonArray (PropertyType.structureModel);

		if (!string.IsNullOrEmpty (_base))
			DoClone (_base);

		if (_body != null)
			foreach (string s in _body)
				DoClone(s);

		weaponRender.Clear ();
		simpleRot.enabled = false;
		if (connectors != null && connectors.Length > 0)
			weaponRender.transform.localPosition = connectors [0];
		else
			weaponRender.transform.localPosition = Vector3.zero;
		
		if (!string.IsNullOrEmpty (_weapon))
		{
			JsonObject stat = Stats.GetObject (_weapon, StatType.Wpn);
			if (stat != null)
			{
				#if DEBUG_STRUC
				Debug.Log ("weapon: "+stat.ToString ());
				#endif
				weaponRender.Init ( PropertyType.model, PropertyType.mountModel);
				//DoClone (s);
			}
			else
				Debug.LogError ("weapon not found: " + _weapon);
		}

		if (!string.IsNullOrEmpty (_sensor))
		{
			JsonObject stat = Stats.GetObject (_sensor, StatType.Sys);
			if (stat != null)
			{
				#if DEBUG_STRUC
				Debug.Log ("sensor: "+stat.ToString ());
				#endif
				if (stat.ContainsKey (PropertyType.sensorModel)) 
				{
					weaponRender.Init ( PropertyType.sensorModel, PropertyType.mountModel);
					simpleRot.enabled = true;
				}
			}
			else
				Debug.LogError ("sensor not found: " + _sensor);
		}
	}

	public void DelRenders()
	{
		foreach (BodyRender r in _renders)
			Destroy (r.gameObject);
		_renders.Clear ();
		connectors = null;
	}

}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Стата
/// </summary>
public class Props
{
	public int intval = 0;
	public string[] arrval;

	string _strval = string.Empty;
	public string strval
	{
		get 
		{
			if (!string.IsNullOrEmpty (_strval))
				return _strval;

			if (arrval != null) 
			{
				string s = "";
				foreach (string ss in arrval)
					s = s + (s == "" ? "" : ",") + ss;
				return s;
			}

			if (intval != 0)
				return intval.ToString ();

			return string.Empty;
		}
		set 
		{
			_strval = value;
		}
	}

	public Props()
	{}

	public Props(int pVal)
	{
		intval = pVal;
	}

	public Props(string pVal)
	{
		_strval = pVal;
	}

	public Props (JsonObject pVal)
	{
		_strval = pVal.ToString ();
	}

	public Props (JsonArray pVal)
	{
		arrval = new string[pVal.Count];
		int i = 0;
		foreach (string v in pVal)
			arrval [i++] = v;
	}


//Helpers
	static Dictionary<string, string> _props = null;

	/// <summary>
	/// Найти енум свойства по имени
	/// </summary>
	public static string StrToProp(string pName)
	{
		if (_props == null)
		{
			_props = new Dictionary<string, string> ();
			foreach (string st in Enum.GetValues(typeof(string)))
				_props [st.ToString ()] = st;
		}
		if (_props.ContainsKey (pName))
			return _props [pName];
		else if (_props.ContainsKey (pName+"_"))
			return _props [pName+"_"];

		Debug.LogError ("Have no property " + pName);
		return PropertyType.NONE;
	}
}

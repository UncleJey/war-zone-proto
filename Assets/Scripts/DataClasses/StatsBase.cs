using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatsBase : MonoBehaviour 
{
	public string name;
	public string model;
	public StatType type;

	protected static Dictionary<StatType, StatsBase> bases = new Dictionary<StatType, StatsBase> ();

	public static StatsBase Get(StatType pType)
	{
		return bases[pType];
	}

	public abstract Sprite icon (string pName);

	public abstract BaseDataClass GetData(string pClassName);

	public abstract List<BaseDataClass> GetList ();
}

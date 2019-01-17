using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitIcons
{
#if UNITY_EDITOR
	public string name;
#endif
	public UnitEnums type;
	public Sprite sprite;
}

public class Units : StatsBase 
{
	private static Dictionary<string, UnitClass> props = new Dictionary<string, UnitClass> ();
	private static Units instance;
	[SerializeField]
	private UnitIcons[] icons;

	void Awake()
	{
		instance = this;
		props.Clear ();
		type = StatType.Templates;
		bases [type] = this;

		JsonObject stats = Stats.GetStat (type);	
		foreach (string s in stats.Keys) 
		{
			JsonObject stat = stats[s] as JsonObject;
			UnitClass prp = new UnitClass (stat);
			props[s] = prp;
		}
	}

	public override Sprite icon (string pName)
	{
		foreach (UnitIcons i in icons)
			if (i.type.ToString ().Equals (pName))
				return i.sprite;
		return null;
	}

	/// <summary>
	/// Найти экземпляр данных
	/// </summary>
	/// <param name="pClassName">P class name.</param>
	public static UnitClass Get(string pClassName)
	{
		if (props.ContainsKey (pClassName))
			return props [pClassName];
		return null;
	}

	/// <summary>
	/// Найти экземпляр данных для отображения
	/// </summary>
	/// <returns>The data.</returns>
	public override BaseDataClass GetData (string pClassName)
	{
		return Get (pClassName) as BaseDataClass;
	}

	/// <summary>
	/// Список доступных классов для использования
	/// </summary>
	public override List<BaseDataClass> GetList ()
	{
		List<BaseDataClass> list = new List<BaseDataClass> ();
		foreach (UnitClass u in props.Values)
			//TODO: ДОбавить условия
			list.Add (u);

		return list;
	}
}

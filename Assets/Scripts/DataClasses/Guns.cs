using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunIcons
{
#if UNITY_EDITOR
	public string name;
#endif
	public GunEnums type;
	public Sprite sprite;
}

public class Guns : StatsBase 
{
	private static Dictionary<string, GunClass> props = new Dictionary<string, GunClass> ();
	private static Guns instance;
	[SerializeField]
	private GunIcons[] icons;

	void Awake()
	{
		instance = this;
		props.Clear ();
		type = StatType.Wpn;
		bases [type] = this;

		JsonObject stats = Stats.GetStat (StatType.Wpn);	
		foreach (string s in stats.Keys) 
		{
			JsonObject stat = stats[s] as JsonObject;
			GunClass prp = new GunClass (stat);
			props[s] =prp;
		}
	}

	public override Sprite icon (string pName)
	{
		foreach (GunIcons i in icons)
			if (i.type.ToString ().Equals (pName))
				return i.sprite;
		return null;
	}

	/// <summary>
	/// Найти экземпляр данных
	/// </summary>
	/// <param name="pClassName">P class name.</param>
	public static GunClass Get(string pClassName)
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
		foreach (GunClass u in props.Values) 
		{
			//TODO: ДОбавить условия
			//UNDONE: ДОбавить условия доступности
			if (u.designable)
				list.Add (u);
		}
		return list;
	}
}

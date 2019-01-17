using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsViewer : MonoBehaviour 
{
	public GroupLayoutPool pool;
	StatsBase stats = null;
	public StatType type;
	public Text caption;
	public ComponentRenderer render;
	public BaseDataClass data;


	/// <summary>
	/// Инициализация основного модуля
	/// </summary>
	public void Init(StatType pType, string pID)
	{
		StopAllCoroutines ();
		type = pType;
		stats = StatsBase.Get(pType);
		data = stats.GetData (pID);
		if (data == null)
			Debug.LogError ("model "+pType.ToString()+": " + pID + " not found");

		Debug.Log (data.ToString ());
		render.Init(data.model, data.mountModel);

		pool.Clear ();
		List<StatClass> datas = data.stats ();

		caption.text = type.ToString()+": "+data.name;

		foreach (StatClass s in datas) 
		{
			Sprite spr = stats.icon (s.name);
			if (spr != null)
			{
				UIValueSlider sld = pool.InstantiateElement (true).GetComponent<UIValueSlider> ();
				s.icon = spr;
				sld.Init (s);
			}
		}
		StartCoroutine (resize ());
	}

	/// <summary>
	/// Инициализация второго параметра для сравнения
	/// </summary>
	public void Init2(BaseDataClass pData)
	{
	//	StopAllCoroutines ();
		BaseDataClass data2 = pData; //stats.GetData (pID);
		if (data2 == null)
			Debug.LogError ("model "+stats.type.ToString()+":  not found");

		Debug.Log (data2.ToString ());

		List<StatClass> datas = data2.stats ();

		//caption.text = type.ToString()+": "+data.name;

		int j = datas.Count - 1;
		while (j >= 0)
		{
			StatClass s = datas [j];
			int i = 0;
			while (true) 
			{
				LayoutElement el = pool.getElement (i++);
				if (el == null)
					break;
				else 
				{
					if (el.GetComponent<UIValueSlider> ().Init2 (s)) 
					{
						datas.Remove (s);
						break;
					}
				}
			}
			j--;
		}


		foreach (StatClass s in datas) 
		{
			/*
			Sprite spr = stats.icon (s.name);
			if (spr != null)
			{
				UIValueSlider sld = pool.InstantiateElement (true).GetComponent<UIValueSlider> ();
				s.icon = spr;
				sld.Init (s);
			}
			*/
			Debug.Log ("parameter " + s.name + " only one in "+type.ToString());
		}
//		StartCoroutine (resize ());
	}

	IEnumerator resize()
	{
		yield return null;
		RectTransform rt = GetComponent<RectTransform> ();
		float sz = pool.GetComponent<RectTransform> ().rect.height + 25;
		if (sz < 120)
			sz = 120;
		rt.sizeDelta = new Vector2 (rt.sizeDelta.x, sz);
	}
}

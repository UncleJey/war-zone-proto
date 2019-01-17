using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitDesigner : WindowBase , IDesignable
{
	public GroupLayoutPool pool;
	public UnitClass data;
	[SerializeField]
	private Button closeButton;
	/// <summary>
	/// Текущий выбранный итем
	/// </summary>
	private UIStatsViewer selectedView = null;

	void OnEnable()
	{
		closeButton.onClick.AddListener (()=>{Close();});
	}

	void OnDisable()
	{
		closeButton.onClick.RemoveAllListeners ();
	}

	void Instant(string pPropName, StatType pType)
	{
		if (!string.IsNullOrEmpty (pPropName)) 
		{
			UIStatsViewer view = pool.InstantiateElement (true).GetComponent<UIStatsViewer> ();
			view.Init (pType, pPropName);
		}
	}

	public override void Open (JsonObject pResearch, string pModel)
	{
		base.Open (pResearch, pModel);
		Init (pModel);
		GUIManager.Instance.GetWindow<UIComponentsPanel> ().Init (this, StatType.Propulsion);
	}

	public override void OnClose ()
	{
		base.OnClose ();
		GUIManager.Instance.GetWindow<UIComponentsPanel> ().Close ();
	}

	public void Init(string pModelName)
	{
		pool.Clear ();
		data = Units.Get (pModelName);

		Instant (data.propulsion, StatType.Propulsion);
		Instant (data.body, StatType.Body);
		Instant (data.repair, StatType.Repair);
		Instant (data.construct, StatType.Sys);
		Instant (data.sensor, StatType.Sys);

		if (data.weapons != null && data.weapons.Length > 0) 
		{
			for (int i = 0; i < data.weapons.Length; i++)
				Instant (data.weapons [i], StatType.Wpn);
		}
	}

#region IDesignable
	/// <summary>
	/// Запросить класс данных для указанного типа
	/// </summary>
	public BaseDataClass GetTypeClass(StatType pType)
	{
		int nr = 0;

		if (selectedView != null) 
		{
			if (selectedView.type.Equals (pType))
				return selectedView.data;
		}

		while (true) 
		{
			LayoutElement el = pool.getElement (nr++, false, false);
			if (el == null)
				return null;

			UIStatsViewer view = el.GetComponent<UIStatsViewer> ();
			if (view == null)
				return null;

			if (view.type.Equals (pType)) 
			{
				selectedView = view;
				return view.data;
			}
		}
		UnitClass c = data;
		return null;
	}

	/// <summary>
	/// Установить класс данных true - как основной false - как сравнение
	/// </summary>
	public void SetTypeClass (BaseDataClass pData, bool pAsMain)
	{
		if (selectedView == null) 
		{
			Debug.LogError ("Not selected!");
			return;
		}

		if (pAsMain) 
		{
			//selectedView.Init (pData);
		}
		else 
		{
			selectedView.Init2 (pData);
		}
	}

#endregion IDesignable

}

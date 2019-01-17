using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentsPanel : WindowBase 
{
	public Button propButton;
	public Button bodyButton;
	public Button wpnButton;
	public Button sysButton;

	public Button closeButton;

	[SerializeField]
	private GroupLayoutPool pool;

	[SerializeField]
	private ScrollRect scroll;

	Coroutine changer;
	/// <summary>
	/// Текущий итем
	/// </summary>
	private UIComponentsItem priItem;
	/// <summary>
	/// Итем выбранный для сравнения
	/// </summary>
	private UIComponentsItem secItem;

	/// <summary>
	/// Цвет текущего итема
	/// </summary>
	[SerializeField]
	private Color priColor;
	/// <summary>
	/// Цвет итема сравнения
	/// </summary>
	[SerializeField]
	private Color secColor;

	private IDesignable designer;

	public void Init(IDesignable pDesigner, StatType pType)
	{
		designer = pDesigner;
		Open ();
		StartCoroutine (DoStart (pType));
	}

	void OnEnable () 
	{
		propButton.onClick.AddListener (()=>{ChangeDir(StatType.Propulsion);});
		bodyButton.onClick.AddListener (()=>{ChangeDir(StatType.Body);});
		wpnButton.onClick.AddListener (()=>{ChangeDir(StatType.Wpn);});
		sysButton.onClick.AddListener (()=>{ChangeDir(StatType.Sys);});

		closeButton.gameObject.SetActive (false);
		scroll.onValueChanged.AddListener(onScrollChanged);
	}

	void OnDisable()
	{
		propButton.onClick.RemoveAllListeners ();
		bodyButton.onClick.RemoveAllListeners ();
		wpnButton.onClick.RemoveAllListeners ();
		sysButton.onClick.RemoveAllListeners ();
		scroll.onValueChanged.RemoveAllListeners ();
	}

	/// <summary>
	/// Скрываем модельки выходящие за рамки при скролле
	/// </summary>
	void onScrollChanged(Vector2 pPosition)
	{
		int i = 0;
		Vector2 limits = new Vector2 (scroll.content.anchoredPosition.y, scroll.content.anchoredPosition.y - 20);
		limits.y += scroll.GetComponent<RectTransform>().rect.height;
	//	Debug.Log (scroll.content.anchoredPosition.ToString()+" l:"+limits.ToString());

		while (true)
		{
			LayoutElement el = pool.getElement(i++,false,false);
			if (el != null)
			{
				if (el.gameObject.activeSelf)
					el.GetComponent<UIComponentsItem> ().Check (limits);
			}
			else
				break;
		}
	}

	IEnumerator DoStart(StatType pType)
	{
		yield return null;
		yield return null;
		ChangeDir (StatType.Propulsion);
		yield return null;
		yield return null;
		onScrollChanged (Vector2.zero);
	}

	void ChangeDir(StatType pType)
	{
		if (changer != null)
			StopCoroutine (changer);
		pool.Clear ();
		changer = StartCoroutine (ChangerC (pType));
	}

	void OnBtnClick(UIComponentsItem _item)
	{
		if (secItem != null) 
		{
			if (secItem.Equals (_item)) 
			{
				designer.SetTypeClass (secItem.data, true);
			}
			else 
			{
				secItem.ChangeColor (Color.white);
				if (_item.Equals (priItem))
					secItem = null;
				else 
				{
					secItem = _item;
					secItem.ChangeColor (secColor);
					designer.SetTypeClass (secItem.data, false);
				}
			}
		}
		else
		{
			if (_item.Equals (priItem))
				secItem = null;
			else 
			{
				secItem = _item;
				secItem.ChangeColor (secColor);
				designer.SetTypeClass (secItem.data, false);
			}
		}
	}

	IEnumerator ChangerC(StatType pType)
	{
		yield return null;
		StatsBase _base = StatsBase.Get (pType);
		BaseDataClass _selected = designer.GetTypeClass (pType);
		secItem = null;
		priItem = null;
		List<BaseDataClass> data = _base.GetList ();
		yield return null;
		int i = 0;
		foreach (BaseDataClass d in data) 
		{
			UIComponentsItem itm = pool.InstantiateElement ().GetComponent<UIComponentsItem> ();
			itm.Init (d, false);
			itm.OnButtonClick -= OnBtnClick;
			itm.OnButtonClick += OnBtnClick;
			if (d.Equals (_selected)) 
			{
				priItem = itm;
				itm.ChangeColor (priColor);
			}

			if (++i > 2) 
			{
				yield return null;
				i = 0;
			}
		}
		onScrollChanged (Vector2.zero);
	}
}

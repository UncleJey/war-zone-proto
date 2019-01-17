using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentsItem : MonoBehaviour 
{
	[SerializeField]
	private ComponentRenderer render;
	[SerializeField]
	private Text caption;
	[SerializeField]
	private Image image;

	/// <summary>
	/// Данные
	/// </summary>
	public BaseDataClass data;
	private RectTransform rt;

	public event System.Action<UIComponentsItem> OnButtonClick;

	void Awake()
	{
		GetComponent<Button> ().onClick.AddListener (onClick);
	}

	void Destroy()
	{
		GetComponent<Button> ().onClick.RemoveAllListeners ();
	}

	void onClick()
	{
		OnButtonClick.Execute (this);
	}

	public void ChangeColor(Color pColor)
	{
		image.color = pColor;
	}

	public void Init(BaseDataClass pData, bool pActivate = true)
	{
		if (!image.color.Equals(Color.white))
			image.color = Color.white;

		rt = GetComponent<RectTransform> ();
		data = pData;
		render.gameObject.SetActive (pActivate);
		render.Init (pData.model, pData.mountModel);
		//TODO: Локализация
		caption.text = data.name;
	}

	public void Check(Vector2 pLimits)
	{
		if (-rt.anchoredPosition.y < pLimits.x || -rt.anchoredPosition.y > pLimits.y) 
		{
			if (render.gameObject.activeSelf)
				render.gameObject.SetActive (false);
		}
		else if (!render.gameObject.activeSelf)
			render.gameObject.SetActive (true);
	}
}

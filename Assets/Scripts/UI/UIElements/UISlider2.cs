using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Двойной слайдер для сравнения
/// </summary>
public class UISlider2 : MonoBehaviour 
{
	public int maxValue;

	public RectTransform fillArea;
	public RectTransform slider;
	public RectTransform slider2;

	float _value = 0;
	/// <summary>
	/// Основной параметр
	/// </summary>
	public float Value
	{
		get
		{
			return _value;
		}
		set
		{
			slider.anchorMax = new Vector2(value / maxValue, 1);
			_value = value;
		}
	}

	float _value2 = 0;
	/// <summary>
	/// Параметр для сравнения
	/// </summary>
	public float Value2
	{
		get
		{
			return _value2;
		}
		set
		{
			slider2.anchoredPosition = new Vector2 (fillArea.rect.width * value / maxValue, 0);
			slider2.gameObject.SetActive (value > 0);
			_value2 = value;
		}
	}

}

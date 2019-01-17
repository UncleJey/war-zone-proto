using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIValueSlider : MonoBehaviour 
{
	public Image sprite;
	public Text val;
	public UISlider2 slider;

	StatClass myStat;

	/// <summary>
	/// Инициализация главного параметра
	/// </summary>
	public void Init(StatClass pStat)
	{
		myStat = pStat;
		slider.maxValue = pStat.MaxValue;
		slider.Value  = pStat.value;
		slider.Value2 = 0;

		val.text = pStat.value.ToString ();
		sprite.sprite = pStat.icon;
		sprite.SetNativeSize ();
	}

	/// <summary>
	/// Инициализация второго параметра (для сравнения) с подтверждением
	/// </summary>
	public bool Init2(StatClass pStat)
	{
		if (myStat.name.Equals (pStat.name)) 
		{
			slider.Value2 = pStat.value;
			return true;
		}

		return false;
	}

}

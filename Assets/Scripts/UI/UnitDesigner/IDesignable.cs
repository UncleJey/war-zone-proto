using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDesignable
{
	/// <summary>
	/// Запросить класс данных для указанного типа
	/// </summary>
	BaseDataClass GetTypeClass(StatType pType);

	/// <summary>
	/// Установить класс данных true - как основной false - как сравнение
	/// </summary>
	void SetTypeClass (BaseDataClass pData, bool pAsMain);

}

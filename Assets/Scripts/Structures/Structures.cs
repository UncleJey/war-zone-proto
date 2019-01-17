using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structures : MonoBehaviour 
{
	JsonObject stats;

	void Start () 
	{
		stats = Stats.GetStat (StatType.Struc);
//			testStruct.Init ((JsonObject)stats["A0BaBaFactory"]);
		//testStruct.transform.position = TheMap.MapToWorld (10, 10);
	}
	/*
	int nr = 0;
	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			string v = stats.Keys.ToArray () [nr++];
			testStruct.Init ((JsonObject)stats [v]);
		}
	}
*/
}

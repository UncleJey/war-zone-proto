using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerResearch 
{
	/// <summary>
	/// Исследования которые уже были произведены
	/// </summary>
	public List<string> doneTech = new List<string>();

	/// <summary>
	/// Доступные исследования
	/// </summary>
	public List<string> availTech = new List<string>();

	public static int maxResearchTime = 0;
	public static int maxResearchEnergy = 0;

	/// <summary>
	/// Инициализация исследований
	/// </summary>
	public void InitFirst()
	{ 
		availTech.Clear ();
		doneTech.Clear ();
		//availTech.Add ("A0ResearchFacility");
		JsonObject data = Stats.GetStat(StatType.Research);
		foreach (JsonObject o in data.Values) 
		{
			if (!o.ContainsKey ("requiredResearch"))
			{
				availTech.Add(o["id"].ToString());
				//Debug.Log (o["id"].ToString());
			}

			if (o.ContainsKey("researchPoints"))
			{
				int val = o["researchPoints"].IntVal();
				if (val > maxResearchTime)
				{
					maxResearchTime = val;
				}
			}
			else 
				Debug.LogError(o["id"].ToString()+" have no research time");

			if (o.ContainsKey("researchPower"))
			{
				int val2 = o["researchPower"].IntVal();
				if (val2 > maxResearchEnergy)
				{
					maxResearchEnergy = val2;
				}
			}
			else 
				Debug.LogError(o["id"].ToString()+" have no research energy");
		}
	}
}

// maxTime 56600
// R-SYS-Engeneering 1200
// R-SYS-Sensor-Turret 900
// R-Wpn-MG1Mk1 60
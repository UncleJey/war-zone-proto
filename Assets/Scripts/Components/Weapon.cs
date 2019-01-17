using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
	public JsonObject data;

	/* --------- Repair
	 droid repair init {
	"buildPoints": 250, 	
	"buildPower": 50, 	
	"designable": 1, 	
	"id": "LightRepair1", 	
	"location": "TURRET", 	
	"model": "GNMREPAR.PIE", 	
	"mountModel": "TRMECM1.PIE", 	
	"name": "Repair Turret", 	
	"repairPoints": 15, 	
	"time": 7, 	
	"weight": 800
	*/

	public void Init(JsonObject pObject)
	{
		data = pObject;
	
	}

	public void Clear()
	{
		
	}
}

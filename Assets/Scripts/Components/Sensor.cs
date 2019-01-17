using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour 
{
	public JsonObject data;

	/* ----------- Sensor
		"buildPoints": 1, 	
		"buildPower": 1, 	
		"hitpoints": 200, 	
		"id": "CCSensor", 	
		"location": "TURRET", 	
		"mountModel": "TRLSNSR1.PIE", 	
		"name": "*CC Sensor*", 	
		"power": 1000, 	
		"range": 3072, 	
		"sensorModel": "misensor.PIE", 	
	*/

	public void Init(JsonObject pObject)
	{
		data = pObject;

	}

	public void Clear()
	{
	}
}

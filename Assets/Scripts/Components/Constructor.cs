using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructor : MonoBehaviour 
{
	public JsonObject data;

	/* ------- Constructor
	"buildPoints": 85, 	
	"buildPower": 17, 	
	"constructPoints": 8, 	
	"designable": 1, 	
	"hitpoints": 25, 	
	"id": "Spade1Mk1", 	
	"name": "Truck", 	
	"sensorModel": "TRLCON.PIE", 	
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
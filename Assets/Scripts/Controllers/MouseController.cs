using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour 
{
	/*
	static MouseController instance;
	public Droid droid;

	public LayerMask groundLayer;

	// Use this for initialization
	void Awake ()
	{
		instance = this;
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{
			//Mapper
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 5000f, groundLayer))
			{
				droid.MoveTo (TheMap.WorldToMap(hit.point));
			}
		}	
	}
	*/
}

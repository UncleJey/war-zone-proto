using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Templates : MonoBehaviour 
{
	//Dictionary<string, BodyPart>  datas = new Dictionary<string, BodyPart>();
//	List<string> types = new List<string> ();

	public Droid testDroid;
	JsonObject stats;
	// Use this for initialization
	void Start () 
	{
		stats = Stats.GetStat (StatType.Templates);
//		int i = 0;
		//foreach (string p in stats.Keys) 
		//	datas [p] = new BodyPart ((JsonObject)stats [p]);

		testDroid.Init ("ConstructionDroid");
		//testDroid.Spawn (35, 17, 1);
		//testDroid.MoveTo (25, 30);
	}
	/*
	int nr = 0;
	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			string v = stats.Keys.ToArray () [nr++];
			string tpe = ((JsonObject)stats [v])["type"].ToString ();
			if (tpe == "DROID")
			{
				Debug.Log (v + stats [v]);
				testDroid.Init ((JsonObject)stats [v]);
			}
			else
				Debug.LogError (v + " " + tpe+" "+ stats [v]);

		}
	}
*/

}
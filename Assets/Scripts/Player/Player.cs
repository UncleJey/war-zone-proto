using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	/// <summary>
	/// Исследования
	/// </summary>
	public PlayerResearch reseach = new PlayerResearch();
	/// <summary>
	/// Шаблоны
	/// </summary>
	public PlayerTemplates templates = new PlayerTemplates();

	public static Player Current;

	void Awake()
	{
		if (Current == null) 
		{
			Current = this;
		}
	}

	void Start()
	{
		InitFirst ();

	}

	void InitFirst()
	{
		reseach.InitFirst ();
		templates.InitFirst ();
		/*
		setPower(1300);
		setPower(200, 6);
		setPower(200, 7);
		setAlliance(6, 7, true);

		// allow to build stuff
		enableStructure("A0CommandCentre", 0);
		enableStructure("A0PowerGenerator", 0);
		enableStructure("A0ResourceExtractor", 0);
		enableStructure("A0ResearchFacility", 0);
		enableStructure("A0LightFactory", 0);

		// needs to be done this way so doesn't enable rest of tree!
		makeComponentAvailable("MG1Mk1", me);

		completeResearch("R-Vehicle-Body01", me);
		completeResearch("R-Sys-Spade1Mk1", me);
		completeResearch("R-Vehicle-Prop-Wheels", me);
		*/

	}

}

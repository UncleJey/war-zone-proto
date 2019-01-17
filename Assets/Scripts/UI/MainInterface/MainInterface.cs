using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInterface : MonoBehaviour 
{
	public ToggleButton factory;
	public ToggleButton research;
	public ToggleButton build;
	public ToggleButton templates;
	public ToggleButton tasks;
	public ToggleButton command;
	public ToggleButton briefeng;

	void OnEnable()
	{
		research.button.onClick.AddListener (()=>{GUIManager.Instance.GetWindow<UIUnitDesigner>().Open(null, "A-Cobra-Trk-HMG");});
	}

	void OnDisable()
	{
		research.button.onClick.RemoveAllListeners ();
	}

}

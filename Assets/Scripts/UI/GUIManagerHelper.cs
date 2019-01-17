using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManagerHelper : MonoBehaviour 
{
	void Awake()
	{
		GUIElement<WindowBase>.InitElements (this);
	}
}

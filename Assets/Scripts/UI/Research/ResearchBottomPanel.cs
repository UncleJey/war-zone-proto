using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchBottomPanel : MonoBehaviour 
{
	public GroupLayoutPool pool;
	public GroupLayoutPool switchers;

	public void Show()
	{
		switchers.gameObject.SetActive (false);
	}
}

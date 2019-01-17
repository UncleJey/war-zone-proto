using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiResearchWindow : WindowBase 
{
	public GroupLayoutPool pool;

	public UISlider timeSlider, energySlider;
	public BodyRender render;
	public Text modelName, infoText;

	public static JsonObject curResearch = null;

	/*
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(0.3f);
		foreach (string r in Player.Current.reseach.availTech) 
		{
			LayoutElement go = pool.InstantiateElement ();
			go.GetComponent<ResearchItem> ().Init (r);
		}
	}
*/


	public override void Open (JsonObject pResearch, string pModel)
	{
		base.Open (pResearch, pModel);
		Change(pResearch, pModel);
	}

	public void Change(JsonObject pResearch, string pModel)
	{
		curResearch = pResearch;

		timeSlider.maxValue = PlayerResearch.maxResearchTime;
		energySlider.maxValue = PlayerResearch.maxResearchEnergy;

		timeSlider.Value = pResearch.IntVal("researchPoints");
		energySlider.Value = pResearch.IntVal("researchPower");
		modelName.text = pResearch.StrVal("name");
		render.DoRender(pModel);

		if (pResearch.ContainsKey("msgName"))
		{
			JsonObject txt = Stats.GetObject(pResearch["msgName"].ToString(), StatType.Messages);
			string msg = "";
			/*id, imdName, sequenceName, text*/
			if (txt != null)
			{
				msg = txt.SplitStrings("text");
			}

			infoText.text = msg;
		}
	}
}

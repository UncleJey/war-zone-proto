using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResearchItem : MonoBehaviour 
{
	public BodyRender render;
	public JsonObject myResearch;
	string model = "";
	public Image researchIcon;

	void OnEnable()
	{
		GetComponent<Button>().onClick.AddListener(OnBtnClick);
	}

	void OnDisable()
	{
		GetComponent<Button> ().onClick.RemoveAllListeners ();
	}

	void OnBtnClick()
	{
		GUIManager.Instance.GetWindow<UiResearchWindow>().Change(myResearch, model);
	}

	public void Init(string pName)
	{
		myResearch = Stats.GetObject (pName, StatType.Research);
		if (myResearch.ContainsKey("iconID"))
		{
			researchIcon.gameObject.SetActive(true);
			researchIcon.sprite = TextureManager.GetSprite(myResearch["iconID"].ToString());
			researchIcon.SetNativeSize();
		}
		else
			researchIcon.gameObject.SetActive(false);

		if (!myResearch.ContainsKey ("imdName")) 
		{
			if (myResearch.ContainsKey ("statID"))
			{
				JsonObject data = Stats.GetObject(myResearch["statID"].ToString());
				//Debug.Log(data.ToString());
				if (data.ContainsKey("sensorModel"))
					model = data["sensorModel"].ToString();
				else if (data.ContainsKey("model"))
					model = data["model"].ToString();
			}
			else
				Debug.LogError ("Have no name in " + myResearch.ToString ());
		}
		else
		{
			model = myResearch["imdName"].ToString();
			if (UiResearchWindow.curResearch == null) // Показываем первое исследование
				OnBtnClick();
		}
		if (!string.IsNullOrEmpty(model))
			render.DoRender (model);
		else 
			Debug.LogError("Model not found in "+pName);
	}
}

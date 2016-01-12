using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CampActionModel: MonoBehaviour{
	public string actionIcon = "Hunt"; // appended with "camp" to make the right UI button image
	public ArrayList costs = new ArrayList(); // list of edieIcon
	public ArrayList effects = new ArrayList(); // list of egameEffects

	public ArrayList paid = new ArrayList(); // list of edieIcon


	public bool isDirty = false;

	// checks to see if this
	public bool payCost(edieIcon cost){
		bool result = false;


		return result;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}



public class CampAction : MonoBehaviour {

	public string ActionName = "Hunt";

	GameObject actionIcon;
	ArrayList costGameObjects = new ArrayList();

	public CampActionModel model;

	// Use this for initialization
	void Start () {
		// associate references
		actionIcon = this.transform.FindChild ("ActionIcon").gameObject;
		costGameObjects.Add(this.transform.FindChild ("Cost1").gameObject);
		costGameObjects.Add(this.transform.FindChild ("Cost2").gameObject);
		costGameObjects.Add(this.transform.FindChild ("Cost3").gameObject);

		model = CampActionFactory.createActionNamed (ActionName);
	
		SpriteRenderer renderer = actionIcon.GetComponent<SpriteRenderer> ();
		// set up the action image
		string iconName = String.Format( "Sprites/UIElements/camp{0}", ActionName);	
		renderer.sprite = Resources.Load (iconName, typeof(Sprite)) as Sprite;

		Debug.Log (iconName);

		// set up the icons for costs
		int index = 0;
		foreach (GameObject dobject in costGameObjects) {

			Debug.Log("sheepish");

			// we'll need to convert the die icon into a filename :(
			SpriteRenderer icoRenderer = dobject.GetComponent<SpriteRenderer>();
			bool enabled = false;

			if (index < model.costs.Count){
				enabled = true;
				icoRenderer.sprite = Resources.Load(DieIconStringNames.getFilename((edieIcon)model.costs[index]),typeof(Sprite))as Sprite;

				Debug.Log(DieIconStringNames.getFilename((edieIcon)model.costs[index]));

			}

			icoRenderer.enabled = enabled;
			index++;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (model.isDirty) {
			// update whatever we's gotses

			model.isDirty = false;
		}
	}
}


static class CampActionFactory {
	static public CampActionModel createActionNamed(string name){

		switch (name){
		case "Forage":
			return createForage();

		case "Heal":
			return createHeal();

		case "Hunt":
			return createHunt();

		case "Plan":
			return createPlan();

		case "Reagents":
			return createReagents();

		case "Reorg":
			return createReorg();

		case "Rest":
			return createRest();

		case "Scout":
			return createScout();

		default:
			return createHunt();

		}
	}

	static public CampActionModel createForage(){
		CampActionModel result = new CampActionModel ();

		result.costs.Add (edieIcon.cups);
		result.costs.Add (edieIcon.maps);
		result.costs.Add (edieIcon.blades);

		return result;
	}

	static public CampActionModel createHeal(){
		CampActionModel result = new CampActionModel ();
		result.costs.Add (edieIcon.cups);
		result.costs.Add (edieIcon.cups);

		return result;
	}

	static public CampActionModel createHunt(){
		CampActionModel result = new CampActionModel ();
		result.costs.Add (edieIcon.blades);
		result.costs.Add (edieIcon.maps);
		return result;
	}

	static public CampActionModel createPlan(){
		CampActionModel result = new CampActionModel ();
		result.costs.Add (edieIcon.minds);
		result.costs.Add (edieIcon.maps);
		return result;
	}

	static public CampActionModel createReagents(){
		CampActionModel result = new CampActionModel ();
		result.costs.Add (edieIcon.cups);
		result.costs.Add (edieIcon.wands);
		result.costs.Add (edieIcon.maps);
		return result;
	}

	static public CampActionModel createReorg(){
		CampActionModel result = new CampActionModel ();
		result.costs.Add (edieIcon.minds);
		result.costs.Add (edieIcon.timePass);
		return result;
	}

	static public CampActionModel createRest(){
		CampActionModel result = new CampActionModel ();
		result.costs.Add (edieIcon.timePass);
		result.costs.Add (edieIcon.provisions);
		return result;
	}

	static public CampActionModel createScout(){
		CampActionModel result = new CampActionModel ();
		result.costs.Add (edieIcon.maps);
		result.costs.Add (edieIcon.boots);
		return result;
	}
}

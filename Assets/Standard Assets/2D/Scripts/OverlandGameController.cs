using UnityEngine;
using System.Collections;

public class OverlandGameController : MonoBehaviour {

	private CrewModel theCrew = new CrewModel();




	public void updateHealthTracker(){
		GameObject dobject = transform.Find ("HealthTracker").gameObject;
		HealthTracker tracker = dobject.GetComponent<HealthTracker> ();
		tracker.NeedsUpdate = true;
	}


	// Use this for initialization
	void Start () {
		// find DICE IN HAND and set the crew
		
		GameObject dobject = transform.Find("DiceInHand").gameObject;
		DiceInHandController dcontroller = dobject.GetComponent<DiceInHandController>();
		dcontroller.Crew = theCrew;

		// health tracker gets a link to crew too
		dobject = transform.Find ("HealthTracker").gameObject;
		HealthTracker hcontroller = dobject.GetComponent<HealthTracker> ();
		hcontroller.Crew = theCrew;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

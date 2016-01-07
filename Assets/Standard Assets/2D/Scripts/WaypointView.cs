using UnityEngine;
using System.Collections;
using System;

public class WaypointView : MonoBehaviour {

	// this is a single waypoint
	public OverlandWaypointModel model;


	private bool previousReached = false;
	private bool previousPassed = false;
	private bool previousBeyondNextCamp = false;

	public void drawBasedOnModel(){
		SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();
		
		// if this is an icon based slot...
		string iconName = String.Format( "Sprites/Icons/{0}", model.waypointType.ToString());	
		
		if (model.waypointType == ewaypointType.waypointIcon){
			iconName = String.Format( "Sprites/Icons/{0}", model.iconRequirement);	
		}

		previousReached = model.reached;
		previousPassed = model.passed;

		renderer.sprite = Resources.Load (iconName, typeof(Sprite)) as Sprite;
		
		renderer.color = Color.white;
	}



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (previousPassed != model.passed || previousReached != model.reached || previousBeyondNextCamp != model.beyondNextCamp) {
			previousPassed = model.passed;
			previousReached = model.reached;
			previousBeyondNextCamp = model.beyondNextCamp;

			SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();

			if (previousReached){
				if (previousPassed){
					// reached and passed = green
					renderer.color = Color.green;
				}else{
					// reached only = yellow
					renderer.color = Color.yellow;
				}
			}

			if (previousBeyondNextCamp){
				renderer.enabled = false;
			}else{
				renderer.enabled = true;
			}
		}
	}
}

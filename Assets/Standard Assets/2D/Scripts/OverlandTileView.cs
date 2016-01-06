using UnityEngine;
using System;
using System.Collections;

public class OverlandTileView : MonoBehaviour {

	public OverlandTileModel tileModel = new OverlandTileModel();


	// Use this for initialization
	void Start () {
		// generate all of the prefabs for the waypoints

		float xOff = -4.2F;
		float yOff = 1.5F;
		foreach (OverlandWaypointModel waypoint in tileModel.waypoints) {
			GameObject newObject = (GameObject)Instantiate(Resources.Load("Prefabs/WaypointView"));
			WaypointView objWv = newObject.GetComponent<WaypointView>();

			objWv.model = waypoint;
			objWv.drawBasedOnModel();

			newObject.transform.parent = this.transform;
			newObject.transform.localPosition = new Vector2(xOff,yOff);
			xOff += 0.4F;
			yOff -= 0.2F;

		}

		// set the first waypoint to reached
		OverlandWaypointModel first = (OverlandWaypointModel)tileModel.waypoints [0];
		first.reached = true;
		first.passed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class OverlandWaypointModel {
	public Vector2 location = new Vector2(0,0);
	public ewaypointType waypointType = ewaypointType.waypointIcon; // uses an icon
	public bool isDifficult = false; // for horses and wagons
	public ewaypointType iconRequirement = ewaypointType.waypointBoots; // the default
	public bool reached = false;
	public bool passed = false; // reached true, passed false = current waypoint
	public bool beyondNextCamp = false;

}


public class OverlandTileModel {
	public int tileNumber = 0;
	public ArrayList waypoints = new ArrayList();

	public int currentNode = 0;
	public int possibleProgess = 0;


	public OverlandTileModel(){
		for (int index = 0; index < 24; index++) {
			OverlandWaypointModel waypoint = new OverlandWaypointModel();
			waypoint.waypointType = ewaypointType.waypointIcon;
			waypoint.iconRequirement = ewaypointType.waypointBoots; // just to be clear what I'm doing

			if (index == 8 || index == 16){
				waypoint.waypointType = ewaypointType.waypointCamp;
			}

			if (index == 5 || index == 19) {
				waypoint.iconRequirement = ewaypointType.waypointMaps;
			}



			if (index == 3){
				waypoint.iconRequirement = ewaypointType.waypointBlades;
			}

			if (index == 12){
				waypoint.waypointType = ewaypointType.waypointEncounter;
			}

			if (index == 21){
				waypoint.iconRequirement = ewaypointType.waypointCups;
			}

			if (index == 23){
				waypoint.waypointType = ewaypointType.waypointFinal;
			}

			if (index > 8){
				waypoint.beyondNextCamp = true;
			}


			waypoints.Add(waypoint);
		}

		OverlandWaypointModel firstModel = (OverlandWaypointModel)waypoints[0];
		firstModel.waypointType = ewaypointType.waypointStart;


	}


	public void loadTile(int tileID){

	}

}

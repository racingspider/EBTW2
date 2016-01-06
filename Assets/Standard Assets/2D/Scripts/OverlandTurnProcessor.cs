using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverlandTurnProcessor {

	Dictionary<int,ArrayList> dieIconEffects;
	Dictionary<string, int> timeCosts;

	public int noneTimeCost = 0;
	public int defaultTimeCost = 0;
	public int progressTimeCost = 1;
	public int timePassTimeCost = 4;
	public int encounterTimeCost = 6;

	public OverlandTurnProcessor(){
		dieIconEffects = new Dictionary<int, ArrayList> ();

		dieIconEffects.Add((int)edieIcon.bandages, new ArrayList(new string[]{"increaseFitness"}));
		dieIconEffects.Add ((int)edieIcon.blades, new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.blank, new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.bonus, new ArrayList (new string[]{"bonus"}));
		dieIconEffects.Add ((int)edieIcon.boots, new ArrayList (new string[]{"progress"}));
		dieIconEffects.Add ((int)edieIcon.camp, new ArrayList (new string[]{"decreaseFatigue", "timePasses"}));
		dieIconEffects.Add ((int)edieIcon.cups, new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.disaster, new ArrayList (new string[]{"disaster"}));
		dieIconEffects.Add ((int)edieIcon.fatigue, new ArrayList (new string[]{"increaseFatigue"}));
		dieIconEffects.Add ((int)edieIcon.forced, new ArrayList (new string[]{"progress","increaseFatigue","progress"}));
		dieIconEffects.Add ((int)edieIcon.hearts, new ArrayList (new string[]{"increaseFitness"}));
		dieIconEffects.Add ((int)edieIcon.injury, new ArrayList (new string[]{"decreaseFitness"}));
		dieIconEffects.Add ((int)edieIcon.maps, new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.minds, new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.provisions, new ArrayList (new string[]{"consumeProvisions"}));
		dieIconEffects.Add ((int)edieIcon.rally, new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.reroll, new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.scrolls,new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.shields,new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.timePass, new ArrayList (new string[]{"timePasses"}));
		dieIconEffects.Add ((int)edieIcon.wands,new ArrayList (new string[]{"none"}));
		dieIconEffects.Add ((int)edieIcon.wayward, new ArrayList (new string[]{"backtrack", "timePasses"}));


		timeCosts = new Dictionary<string, int> ();

		timeCosts.Add ("none", noneTimeCost);
		timeCosts.Add ("default", defaultTimeCost);
		timeCosts.Add ("timePasses", timePassTimeCost);
		timeCosts.Add ("progress", progressTimeCost);
		timeCosts.Add ("encounter",encounterTimeCost);

	}

	// 
	public void processSeries(CrewModel theCrew, ArrayList dice, OverlandTileModel tileModel){
		foreach (DieView die in dice) {
			// get an array of effects on the current side
			ArrayList results = die.model.iconsOnCurrentSide();
			foreach(edieIcon icon in results){
				processAction(theCrew,icon, tileModel, dice);
			}
		}

		int finalIndex = 0;
		int startIndex = tileModel.currentNode;

		bool movementHalted = false;

		// process progress total
		if (theCrew.progress > 0) {
			for (int index = 0; index <= theCrew.progress; index++){

				if (!movementHalted){

					OverlandWaypointModel wap = (OverlandWaypointModel)tileModel.waypoints[index + tileModel.currentNode];

					OverlandWaypointModel startNode = (OverlandWaypointModel)tileModel.waypoints[startIndex];
					// verify we have the prerequisite for the next
		
					// if we LAND on a camp, we're done.
					if (wap.waypointType == ewaypointType.waypointCamp && index + tileModel.currentNode != startIndex){
						movementHalted = true;
					}

					// if we LAND on an encounter, we're done.
					if (wap.waypointType == ewaypointType.waypointEncounter && index + tileModel.currentNode != startIndex){
						movementHalted = true;
					}

					// if we LAND on an icon that we don't have in hand, we're done
					if (wap.waypointType == ewaypointType.waypointOptionalEncounter && index + tileModel.currentNode != startIndex){
						movementHalted = true;
					}

					// icon waypoint
					if (wap.waypointType == ewaypointType.waypointIcon && index + tileModel.currentNode != startIndex && wap.iconRequirement != ewaypointType.waypointBoots){
						// check to see if we have one
						if (! hasRequiredIcon(dice, wap.iconRequirement)){
							movementHalted = true;
						}
					}

					// if we LAND on the final, we're done


					finalIndex = index + tileModel.currentNode;

					// check the previous waypoint
					if (index + tileModel.currentNode - 1 >= 0){
						OverlandWaypointModel bak = (OverlandWaypointModel)tileModel.waypoints[index + tileModel.currentNode -1];
						bak.passed = true;
					}
					wap.reached = true;
				}
			}
		}

		tileModel.currentNode = finalIndex;
		// reset crew progress
		theCrew.progress = 0;
	}


	private bool hasRequiredIcon(ArrayList dice, ewaypointType icon){
		bool result = false;

		foreach (DieView die in dice) {
			ArrayList iconsonface = die.model.iconsOnCurrentSide();
			// the INT value of boots, cups, scrolls, etc match the waypoint and edieicon

			foreach( edieIcon dieIcon in iconsonface){
				if ((int)dieIcon == (int)icon){
					return true;
				}
			}
		}
		return result;
	}




	public void processAction(CrewModel theCrew, edieIcon theIcon, OverlandTileModel tileModel, ArrayList dice){
		ArrayList currentEffects;

		// we pass in DICE because we may need to know what is on other faces later (like blades, etc)


		if (dieIconEffects.ContainsKey ((int)theIcon)) {
			currentEffects = dieIconEffects[(int)theIcon];
			foreach (string effect in currentEffects){

				switch (effect){
				case "none":
					break;
				case "increaseFitness":
					increaseFitness(theCrew);
					break;
				case "bonus":
					break;
				case "progress":
					makeProgress(theCrew, tileModel, dice);
					break;
				case "decreaseFatigue":
					decreaseFatigue(theCrew);
					break;
				case "disaster":
					disaster(theCrew);
					break;
				case "decreaseFitness":
					decreaseFitness(theCrew);
					break;
				case "consumeProvisions":
					consumeProvisions(theCrew);
					break;
				case "halt":
					halt(theCrew);
					break;
				case "backtrack":
					backtrack(theCrew, tileModel);
					break;
				case "timePasses":
					timePasses(theCrew);
					break;
				case "increaseFatigue":
					increaseFatigue(theCrew);
					break;
				default:
					Debug.Log("fall through.");
					break;
				}
			}
		}
	}

	private void enforceTimeCosts(CrewModel theCrew, string effect){
		int tc = timeCosts ["default"];

		if (timeCosts.ContainsKey (effect)) {
			// add this time cost
			tc = timeCosts[effect];
		} 

		theCrew.currentTimeSegment += tc;

		Debug.Log(string.Format("Time Passed: {0}",theCrew.currentTimeSegment));
	}


	private void disaster(CrewModel theCrew){
		theCrew.disasters++;
		// code later to raise a disaster encounter
		Debug.Log(string.Format("Disaster! Total: {0}",theCrew.disasters));
	}

	private void bonus(CrewModel theCrew){
		// this probably shouldn't do anything in overland map
		Debug.Log ("'bonus' has no effect in overland");
	}

	private void timePasses(CrewModel theCrew){

		// nothing special happens here since the actual time pass is paid for in time...? wut?
		Debug.Log ("Time passes");
	}

	private void backtrack(CrewModel theCrew, OverlandTileModel tileModel){

		// reduce progress made and increases time (time increase is baked into the rule above)
		theCrew.progress--;
		if (theCrew.progress < 0) {
			theCrew.progress = 0;
		}

		Debug.Log ("Lose progress");
	}

	private void makeProgress(CrewModel theCrew, OverlandTileModel tileModel, ArrayList dice){
		if (theCrew.halted == false) {
			theCrew.progress++;
		}

		Debug.Log ("Make progress");
	
	}

	private void halt(CrewModel theCrew){
		theCrew.halted = true;
		Debug.Log ("Crew halted");
	}

	private void consumeProvisions(CrewModel theCrew){

		if (theCrew.provisions == 0) {
			Debug.Log("No more food left, fatigue increased");
			increaseFatigue(theCrew);

		} else {
			Debug.Log("Om, nom, nom.");
			theCrew.provisions--;
		}
	}

	private void decreaseFatigue(CrewModel theCrew){
		if (theCrew.healthTrack.Count > 0) {
			bool found = false;
			for (int index = 0; index <= theCrew.healthTrack.Count - 1; index++){
				if ((int)theCrew.healthTrack[index] == (int)ecrewHealthTrackDamage.fatigue && found == false){
					theCrew.healthTrack.RemoveAt(index);
					found = true;
				
				}
			}
			if (found){
				Debug.Log("Fatigue reduced");
			}else{
				Debug.Log("No fatigue to recover");
			}
		}
	}

	private void increaseFatigue(CrewModel theCrew){
		// check to see if we are maxxed out. If we are,
		// change the first found fatigue to INJURY
		// otherwise, if there are not fatigues, we 
		// convert to injury.

		if (theCrew.healthTrack.Count == theCrew.healthTrackSize) {
			// convert a fatigue to injury
			bool found = false;
			for (int index = 0; index <= theCrew.healthTrack.Count - 1; index++){
				if ((int)theCrew.healthTrack[index] == (int)ecrewHealthTrackDamage.fatigue && found == false){
					theCrew.healthTrack[index] = ecrewHealthTrackDamage.fitness;
					found = true;
					Debug.Log("Fatigue turned to injury!");
				}
			}

			// if not found, cause regular damage
			if (!found){
				decreaseFitness(theCrew);
			}

		} else {
			// just add a fatigue
			Debug.Log("Fatigue increased");
			theCrew.healthTrack.Add(ecrewHealthTrackDamage.fatigue);
		}

	}

	private void decreaseFitness(CrewModel theCrew){
		if (theCrew.healthTrack.Count == theCrew.healthTrackSize) {
			bool found = false;
			for (int index = 0; index <= theCrew.healthTrack.Count - 1; index++){
				if ((int)theCrew.healthTrack[index] == (int)ecrewHealthTrackDamage.fatigue && found == false){
					theCrew.healthTrack[index] = ecrewHealthTrackDamage.fitness;
					found = true;
				}
			}

			if (found == false){
				// really hurt someone!
				Debug.Log("Injure the crap out of someone");
			}

		} else {
			theCrew.healthTrack.Add(ecrewHealthTrackDamage.fitness);
		}
	}

	private void increaseFitness(CrewModel theCrew){
		if (theCrew.healthTrack.Count > 0) {
			bool found = false;
			for (int index = 0; index <= theCrew.healthTrack.Count - 1; index++){
				if ((int)theCrew.healthTrack[index] == (int)ecrewHealthTrackDamage.fitness && found == false){
					theCrew.healthTrack.RemoveAt(index);
					found = true;
					Debug.Log("Injury recovered");
				}
			}
		}
	}
}

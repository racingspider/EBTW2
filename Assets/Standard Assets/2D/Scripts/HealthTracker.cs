using UnityEngine;
using System.Collections;

public class HealthTracker : MonoBehaviour {

	ArrayList healthTrackEntries = new ArrayList();

	private CrewModel theCrew;

	private bool _needsUpdate = false;

	public bool NeedsUpdate{
		set{
			_needsUpdate = value;
		}
	}


	public CrewModel Crew{
		set {
			theCrew = value;

		}
		get{
			return theCrew;
		}
	}

	// Use this for initialization
	void Start () {
		// find each of the health track entries and put them into the arraylist
		for (int index = 1; index < 9; index++) {
			healthTrackEntries.Add (transform.Find (string.Format("HealthEntry{0}",index)).gameObject);

			if (index == 60){
				GameObject entry = (GameObject)healthTrackEntries[index - 1];
				HealthTrackEntry entryScript = entry.GetComponent<HealthTrackEntry>();
				entryScript.injury = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_needsUpdate) {
			_needsUpdate = false;

			for (int index = 0; index < healthTrackEntries.Count;index++){
				GameObject entry = (GameObject)healthTrackEntries[index];
				HealthTrackEntry entryScript = entry.GetComponent<HealthTrackEntry>();

	

				if (index < theCrew.healthTrack.Count){
					if ((int)theCrew.healthTrack[index] == (int)ecrewHealthTrackDamage.fatigue){
						entryScript.fatigue = true;
					}else{
						entryScript.injury = true;
					}
				}
				else{
					// make sure we're clear

					entryScript.injury = false;
					entryScript.fatigue = false;
				}
			}

		}
	}
}

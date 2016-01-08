using UnityEngine;
using System.Collections;

public class CrewModel {

	public int _provisions = 8;
	public ArrayList healthTrack = new ArrayList();
	public int healthTrackSize = 8; // fatigue + injury maximum

	public int currentTimeSegment = 0;
	public int progress = 0;
	public bool halted = false;

	public int disasters = 0;

	// we can calculate it from inventory
	public int provisions{
		get{
			return _provisions;
		}set{
			_provisions = value;
		}

	}

}

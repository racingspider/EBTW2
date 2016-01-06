using UnityEngine;
using System.Collections;

public class CrewModel {

	public int provisions = 12;
	public ArrayList healthTrack = new ArrayList();
	public int healthTrackSize = 8; // fatigue + injury maximum

	public int currentTimeSegment = 0;
	public int progress = 0;
	public bool halted = false;

	public int disasters = 0;




}

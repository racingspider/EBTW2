using UnityEngine;
using System.Collections;

public class TimeDisplayController : MonoBehaviour {

	public CrewModel theCrew;

	public int additional = 0;
	public int prevTimeAmount = -1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (theCrew.currentTimeSegment != prevTimeAmount) {
			
			for (int index = 1; index <= 9; index++) {
				GameObject dobject = this.transform.FindChild(string.Format("timeClock{0}",index)).gameObject;
				SpriteRenderer renderer = dobject.GetComponent<SpriteRenderer>();

				if (index > theCrew.currentTimeSegment){
					renderer.color = Color.gray;
				}else{
					renderer.color = Color.green;
				}
			}
			prevTimeAmount = theCrew.currentTimeSegment;
		}
	}
}

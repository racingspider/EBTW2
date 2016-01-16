using UnityEngine;
using System.Collections;

public class DieRollNumber : MonoBehaviour {


	public int rollNumber = 3;
	public float homeX = 0;
	public float homeY = 0;


	public void onTouch(){
		// tell parent we have been selected
		DiceInHandController doc = this.transform.parent.GetComponent<DiceInHandController> ();
		doc.selectNumberOfDice (rollNumber);

	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

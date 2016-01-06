using UnityEngine;
using System.Collections;

public class HealthTrackEntry : MonoBehaviour {

	private bool _injury = false;
	private bool _fatigue = false;

	public bool fatigue = false;
	public bool injury = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (_fatigue != fatigue) {
			_fatigue = fatigue;
			GameObject fattie = this.transform.Find("DisplayFatigue").gameObject;
			SpriteRenderer rendie = fattie.GetComponent<SpriteRenderer>();
			rendie.enabled = _fatigue;

			if (_fatigue){
				injury = false;
			}
		}

		if (_injury != injury) {
			_injury = injury;
			GameObject fattie = this.transform.Find("DisplayInjury").gameObject;
			SpriteRenderer rendie = fattie.GetComponent<SpriteRenderer>();
			rendie.enabled = _injury;

			if (_injury){
				fatigue = false;
			}
		}

	}
}

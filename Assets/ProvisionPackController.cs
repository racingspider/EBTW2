using UnityEngine;
using System.Collections;

public class ProvisionPackController : MonoBehaviour {

	public int provisions = 5;
	private int prevProvisions = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (prevProvisions != provisions) {



			prevProvisions = provisions;
		}
	}
}

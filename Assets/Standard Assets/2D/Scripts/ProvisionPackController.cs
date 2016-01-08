using UnityEngine;
using System.Collections;
using System;

public class ProvisionPackController : MonoBehaviour {

	public int provisions = 5;
	private int prevProvisions = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	// what a mess!
	void Update () {

		if (prevProvisions != provisions) {

			// full provisions is handled differently
			if (provisions == 5 || provisions == 0) {
				GameObject fiveobject = this.transform.Find ("provisions5").gameObject;
				SpriteRenderer fiverenderer = fiveobject.GetComponent<SpriteRenderer> ();
				fiverenderer.enabled = true;

				// all others are invisible
				for (int dex = 4; dex > 0; dex--) {
					GameObject dobject = this.transform.Find (string.Format ("provisions{0}", dex)).gameObject;
					SpriteRenderer renderer = dobject.GetComponent<SpriteRenderer> ();
					renderer.enabled = false;
				}

				if (provisions == 0){
					fiverenderer.color  = Color.gray;
					fiverenderer.enabled = false;

					for (int dex = 4; dex > 0; dex--) {
						GameObject dobject = this.transform.Find (string.Format ("provisions{0}", dex)).gameObject;
						SpriteRenderer renderer = dobject.GetComponent<SpriteRenderer> ();
						renderer.enabled = true;
						renderer.color = Color.gray;
					}

				}else{
					fiverenderer.color = Color.green;

				}

			} else {

				GameObject fiveobject = this.transform.Find ("provisions5").gameObject;
				SpriteRenderer fiverenderer = fiveobject.GetComponent<SpriteRenderer> ();
				fiverenderer.enabled = false;
				
				for (int dex = 4; dex > 0; dex--) {
					GameObject dobject = this.transform.Find (string.Format ("provisions{0}", dex)).gameObject;
					SpriteRenderer renderer = dobject.GetComponent<SpriteRenderer> ();
					renderer.enabled = true;
					if (provisions >= dex) {

						renderer.color = Color.yellow;
					} else {
						renderer.color = Color.gray;
					}
				}
			}

			prevProvisions = provisions;
		}
	}
}

using UnityEngine;
using System.Collections;

public class RollsRemainingController : MonoBehaviour {

	public int rollsRemaining = 0;

	ArrayList rollRenderObjects = new ArrayList();


	int previousRollsRemaining = -1;


	// Use this for initialization
	void Start () {

		for (int index = 1; index < 4; index++) {
			string childName = string.Format("rolls{0}",index);
			GameObject dobject = this.transform.FindChild(childName).gameObject;
			SpriteRenderer renderer = dobject.GetComponent<SpriteRenderer>();
			rollRenderObjects.Add(renderer);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (rollsRemaining != previousRollsRemaining) {
			previousRollsRemaining = rollsRemaining;
			for (int index = 1; index < 4; index++){
				SpriteRenderer renderer = (SpriteRenderer)rollRenderObjects[index - 1];
				if (index > this.rollsRemaining){
					renderer.color = Color.gray;
				}else{
					renderer.color = Color.green;
				}
			}

	
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
	public LayerMask touchInputMask;
	private List<GameObject> touchList = new List<GameObject> ();
	private GameObject[] touchesOld;
	private RaycastHit hit;
	// Update is called once per frame
	void Update () {


#if UNITY_EDITOR
	if (Input.GetMouseButtonUp(0)){


			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos),Vector2.zero);

			if (hitInfo){
				hitInfo.transform.gameObject.SendMessage("onTouch",SendMessageOptions.DontRequireReceiver);
			}
		}

#endif



		foreach (Touch touch in Input.touches) {

			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
					
			if (Physics.Raycast(ray,out hit,0)){
				
				GameObject recipient = hit.transform.gameObject;
				touchList.Add(recipient);
				
				Debug.Log("hit man, hit");
			}



			Debug.Log("yeah, that's a touch");

		}
	}
}

using UnityEngine;
using System.Collections;

public class ProvisionsViewController : MonoBehaviour {

	public CrewModel theCrew;
	private int previousProvisions = 0;

	private ProvisionPackController pack1;
	private ProvisionPackController pack2;
	private ProvisionPackController pack3;

	// Use this for initialization
	void Start () {
		GameObject dobject = this.transform.FindChild ("ProvisionPack1").gameObject;
		pack1 = dobject.GetComponent<ProvisionPackController> ();

		pack2 = this.transform.FindChild ("ProvisionPack2").gameObject.GetComponent<ProvisionPackController> ();
		pack3 = this.transform.FindChild ("ProvisionPack3").gameObject.GetComponent<ProvisionPackController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (previousProvisions != theCrew.provisions) {
			int remaining = theCrew.provisions;

			Debug.Log(remaining);

			if (remaining >= 5){
				pack1.provisions = 5;
				remaining -=5;
			}else{
				pack1.provisions = remaining;
				remaining = 0;
			}

			Debug.Log(remaining);

			if (remaining >= 5){
				pack2.provisions = 5;
				remaining -=5;
			}else{
				pack2.provisions = remaining;
				remaining = 0;
			}

			Debug.Log(remaining);

			if (remaining >= 5){
				pack3.provisions = 5;
				remaining -=5;
			}else{
				pack3.provisions = remaining;

				Debug.Log(string.Format("pack 3: {0}",pack3.provisions));

				remaining = 0;
			}

			Debug.Log(remaining);

			previousProvisions = theCrew.provisions;
		}
	}
}

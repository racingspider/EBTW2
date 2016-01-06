using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;




public class DieView : MonoBehaviour {
	
	private GameObject background;
	private GameObject selection;
	private GameObject ico1;
	private GameObject ico2;
	private GameObject ico3;

	public Vector2 homeLocation;
	
	private Dictionary<int, Color> backgroundColors = new Dictionary<int,Color>();



	public DieModel model;


	public DieView(){
		// initialize the colors dictionary
		backgroundColors.Add ((int)edieBaseType.series1, new Color(0.54F,0.56F,0.43F));
		backgroundColors.Add ((int)edieBaseType.series2, new Color(0.56F,0.43F,0.43F));
		backgroundColors.Add ((int)edieBaseType.captain, new Color(1.0F,0.5F,0.5F));
		backgroundColors.Add ((int)edieBaseType.scout, new Color(0.44F,0.78F,0.21F));
		backgroundColors.Add ((int)edieBaseType.adventurer, Color.magenta);
	}
		

	public void displayDie(){
		SpriteRenderer renderer = background.GetComponent<SpriteRenderer> ();
		renderer.sprite = Resources.Load ("Sprites/DiceBackgrounds/whiteGlossDie", typeof(Sprite)) as Sprite;
		renderer.color = backgroundColors [(int)model.dieType];
		
		ArrayList icons = model.iconsOnCurrentSide();
		
		ico1 = transform.Find ("Icon1").gameObject;
		renderer = ico1.GetComponent<SpriteRenderer> ();
		
		string iconName = String.Format( "Sprites/Icons/{0}", icons[0]);	

		renderer.sprite = Resources.Load (iconName, typeof(Sprite)) as Sprite;
		if ((int)model.iconSource[model.currentResult - 1] > 1) {
			renderer.color = new Color (1.0F, 0.5F, 0.0F);
		}

		// if we're selected, show that, if we're frozen, locked, etc.
		if (model.dieState == edieState.selected || model.dieState == edieState.locked) {
			SpriteRenderer selectRenderer = selection.GetComponent<SpriteRenderer> ();

			selectRenderer.enabled = true;
			if (model.dieState == edieState.locked) {
				selectRenderer.color = Color.gray;
			} else {
				selectRenderer.color = Color.white;
			}
		} else {
			SpriteRenderer selectRenderer = selection.GetComponent<SpriteRenderer>();
			selectRenderer.enabled = false;
		}
	}

	void onTouch(){
		// switch states if possible

		if (model.dieState != edieState.locked) {
//			SpriteRenderer renderer = selection.GetComponent<SpriteRenderer>();

			model.switchSelect();

			DiceInHandController parento = this.GetComponentInParent<DiceInHandController> ();
			parento.selectUnselectDie(this);

			displayDie ();
		}
	}


	// Use this for initialization
	void Start () {
		background = transform.Find("DieBackground").gameObject;
		selection = transform.Find("Selection").gameObject;

		//SpriteRenderer renderer = background.GetComponent<SpriteRenderer>();
		//renderer.sprite = Resources.Load("Sprites/DiceBackgrounds/whiteGlossDie", typeof(Sprite)) as Sprite;
		//renderer.color = Color.blue;
		//ico1 = transform.Find ("Icon1").gameObject;
		//renderer = ico1.GetComponent<SpriteRenderer> ();
		//renderer.sprite = Resources.Load ("Sprites/Icons/boots", typeof(Sprite)) as Sprite;
		//renderer.color = Color.black;
	}



	// Update is called once per frame
	void Update () {


	}
}

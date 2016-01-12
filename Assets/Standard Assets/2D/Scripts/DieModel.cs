using UnityEngine;
using System.Collections;

public class DieModel {

	public edieBaseType dieType = edieBaseType.series2;

	private int previousResult = 1;
	public int currentResult = 1;

	public edieState previousState = edieState.inHand;
	public edieState dieState = edieState.inHand;

	public ArrayList iconsOnfaces = new ArrayList(); // this is ALL icons
	public ArrayList iconSource = new ArrayList(); // base, equipment, ability (black, blue, orange?)

	public int slot = 0;
	public int homeSlot = 0;

	public DieModel(){
		rollDie();
	}

	public void rollDie(){

		if (dieState == edieState.selected) {

			dieState = edieState.locked;

		}

		if ((dieState != edieState.locked) && (dieState != edieState.frozen)){
			previousResult = currentResult;
			currentResult = (int)Random.Range(1, 7);
		}

	}

	public ArrayList iconsOnCurrentSide(){
		ArrayList results = new ArrayList();

		results.Add(iconsOnfaces [currentResult - 1]);

		return results;
	}

	public void switchSelect(){

		if (dieState == edieState.selected) {
			previousState = edieState.selected;
			dieState = edieState.inHand;
		}
		else if (dieState == edieState.inHand){
			previousState = edieState.inHand;
			dieState = edieState.selected;
		}
		 
	}



}

public static class DieFactory{
public	static DieModel createScout(){
		DieModel model = new DieModel();
		model.iconsOnfaces.Add(edieIcon.boots);
		model.iconsOnfaces.Add(edieIcon.maps);
		model.iconsOnfaces.Add(edieIcon.boots);
		model.iconsOnfaces.Add(edieIcon.blades);
		model.iconsOnfaces.Add(edieIcon.cups);
		model.iconsOnfaces.Add(edieIcon.maps);
		model.dieType = edieBaseType.scout;
		model.iconSource = ArrayList.Repeat (1, 6);
		return model;
	}

public	static DieModel createSeries1(){
		DieModel model = new DieModel();
		model.iconsOnfaces.Add(edieIcon.boots);
		model.iconsOnfaces.Add(edieIcon.boots);
		model.iconsOnfaces.Add(edieIcon.provisions);
		model.iconsOnfaces.Add(edieIcon.fatigue);
		model.iconsOnfaces.Add(edieIcon.injury);
		model.iconsOnfaces.Add(edieIcon.fatigue);
		model.dieType = edieBaseType.series1;
		model.iconSource = ArrayList.Repeat (1, 6);
		return model;
	}

public 	static DieModel createSeries2(){
		DieModel model = new DieModel();
		model.iconsOnfaces.Add(edieIcon.boots);
		model.iconsOnfaces.Add(edieIcon.boots);
		model.iconsOnfaces.Add(edieIcon.boots);
		model.iconsOnfaces.Add(edieIcon.camp);
		model.iconsOnfaces.Add(edieIcon.wayward);
		model.iconsOnfaces.Add(edieIcon.disaster);
		//model.iconsOnfaces.Add (edieIcon.boots); // second boot icon on first die facing
		model.dieType = edieBaseType.series2;
		model.iconSource = ArrayList.Repeat (1, 6);
		return model;
	}

public	static DieModel createCaptain(){
		DieModel model = new DieModel();
		model.iconsOnfaces.Add(edieIcon.blades);
		model.iconsOnfaces.Add(edieIcon.boots);
		model.iconsOnfaces.Add(edieIcon.maps);
		model.iconsOnfaces.Add(edieIcon.blades);
		model.iconsOnfaces.Add(edieIcon.minds);
		model.iconsOnfaces.Add(edieIcon.blades);
		model.dieType = edieBaseType.captain;
		model.iconSource = ArrayList.Repeat (1, 6);
		return model;
	}

}
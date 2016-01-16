using UnityEngine;
using System.Collections;

public class DiceInHandController : MonoBehaviour {

	private DieView die1;
	private DieView die2;
	private DieView die3;
	private DieView die4;
	private DieView die5;
	private DieView die6;

	private ArrayList lockSlots = new ArrayList();
	private ArrayList selectedDice = new ArrayList();

	private CrewModel theCrew;

	// here are the actual dice
	private ArrayList dice= new ArrayList();

	private int numberDiceRolled = 6; // for now, always roll 6 dice
	private int previousDiceRolled = -1;

	private int seriesRollNumber = 1; // 1 = initial roll, each reroll adds one
	private int numberDiceSelected = 0; 

	// not sure this belongs here...
	public OverlandTurnProcessor turnProcessor = new OverlandTurnProcessor ();

	public bool enforceRollingRules = false;

	// passed in from OverlandGameMain
	public CrewModel Crew{
		set {
			theCrew = value;
		}
		get{
			return theCrew;
		}
	}


	public DiceInHandController(){


	}

	public void selectNumberOfDice(int number){

		numberDiceRolled = number;

		// send dice home, up to number of selected dice
		Debug.Log (string.Format ("number dice rolled {0}", numberDiceRolled));
		resetDieLocations (numberDiceRolled);

		// hide the selection buttons
		for (int index = 3; index < 7; index++) {
			string childName = string.Format("rollButton{0}", index);
			GameObject dobject = this.transform.FindChild (childName).gameObject;
			DieRollNumber droll = dobject.GetComponent<DieRollNumber> ();
			droll.transform.position = new Vector2 (300, 300);
		}
	}

	public void selectUnselectDie(DieView theDie){

		if (!selectedDice.Contains (theDie)) {

			// set current position to the correct lock slot?
			selectedDice.Add(theDie);
			numberDiceSelected++;

		} else {
			theDie.transform.localPosition = new Vector2(theDie.homeLocation.x, 0.0f);
			selectedDice.Remove(theDie);
			numberDiceSelected--;
		}

		// CHANGE ROLL BUTTON TO PROCESS ...
		// if selected + locked = number of dice in series, change icon to process, otherwise, roll button 

		displayLockedAndSelectedDice ();
	}


	private void displayLockedAndSelectedDice(){
		// update positions
		int index = 0;
		foreach (DieView theDie in selectedDice) {
			GameObject lockslot = (GameObject)lockSlots[index];
			theDie.transform.position = lockslot.transform.position;
			index++;
		}
	
	}

	public bool checkToResetAllDice(){
		bool result = false;
		int lockedOrSelected = 0;
		foreach (DieModel die in dice) {
			if (die.dieState == edieState.locked || die.dieState == edieState.selected){
				lockedOrSelected++;
			}
		}

		if (lockedOrSelected == numberDiceRolled) {
			result = true;
		}

		return result;
	}

	public void onTouch(){

		if (checkToResetAllDice ()) {
			// process!!

			OverlandTileView tileView = FindObjectOfType<OverlandTileView>();

			OverlandTurnProcessor turnProc = new OverlandTurnProcessor();
			turnProc.processSeries(theCrew, selectedDice, tileView.tileModel);

			// then reset and banish until number of dice are selected
			foreach (DieModel die in dice){
				die.dieState = edieState.inHand;
			}

			banishDiceInHand();
			resetDieNumberLocations();
			//resetDieLocations();
			selectedDice.Clear();
			displayDice();
		
			OverlandGameController GC = this.transform.parent.GetComponent<OverlandGameController>();
			GC.updateHealthTracker();

			seriesRollNumber = 0; // just processed
		} 			

		// we autoroll after resetting, we can change that later
		rollDice ();
	}

	private void resetDieNumberLocations(){
		for (int index = 3; index < 7; index++) {
			string childName = string.Format("rollButton{0}", index);
			GameObject dobject = this.transform.FindChild (childName).gameObject;
			DieRollNumber droll = dobject.GetComponent<DieRollNumber> ();
			droll.transform.localPosition = new Vector2 (droll.homeX, droll.homeY);
		}
	}


	private void banishDiceInHand(){
		die1.transform.localPosition = new Vector2 (300, 300);
		die2.transform.localPosition = new Vector2 (300, 300);
		die3.transform.localPosition = new Vector2 (300, 300);
		die4.transform.localPosition = new Vector2 (300, 300);
		die5.transform.localPosition = new Vector2 (300, 300);
		die6.transform.localPosition = new Vector2 (300, 300);
	}



	private void resetDieLocations(int numberRolled){
		die1.transform.localPosition = new Vector2 (die1.homeLocation.x, die1.homeLocation.y);
		die2.transform.localPosition = new Vector2 (die2.homeLocation.x, die2.homeLocation.y);
		die3.transform.localPosition = new Vector2 (die3.homeLocation.x, die3.homeLocation.y);

		switch (numberRolled) {
		case 3:
			die4.transform.localPosition = new Vector2 (300,300);
			die5.transform.localPosition = new Vector2 (300,300);
			die6.transform.localPosition = new Vector2 (300,300);
			break;

		case 4:
			die4.transform.localPosition = new Vector2 (die4.homeLocation.x, die4.homeLocation.y);
			die5.transform.localPosition = new Vector2 (300,300);
			die6.transform.localPosition = new Vector2 (300,300);
			break;
		case 5:
			die4.transform.localPosition = new Vector2 (die4.homeLocation.x, die4.homeLocation.y);
			die5.transform.localPosition = new Vector2 (die5.homeLocation.x, die5.homeLocation.y);
			die6.transform.localPosition = new Vector2 (300,300);
			break;
		case 6:
			die4.transform.localPosition = new Vector2 (die4.homeLocation.x, die4.homeLocation.y);
			die5.transform.localPosition = new Vector2 (die5.homeLocation.x, die5.homeLocation.y);
			die6.transform.localPosition = new Vector2 (die6.homeLocation.x, die6.homeLocation.y);
			break;
		default:
			break;
		}



	}

	public void rollDice(){

		if (!canRollDice ()) {
			return;
		}

		// if everything is selected, process and release
		seriesRollNumber++;
		numberDiceSelected = 0;

		foreach (DieModel die in dice){

			die.rollDie();

			if (die.dieState == edieState.locked) {
				// verify that it isn't already in a slot
				if (!lockSlots.Contains(die)){
					lockSlots.Add(die);
				}
			}
		}

		displayDice ();
	}

	private bool canRollDice(){
		bool result = false;

		if (!enforceRollingRules) {
			result = true;
		}

		if (seriesRollNumber == 0) {
			result = true;
		} else if (seriesRollNumber == 1) {
			//must select 3 dice.
			if (numberDiceSelected >= 3){
				result = true;
			}

		} else {
			//must select at least once.
			if (numberDiceSelected >= 1){
				result = true;
			}
		}

		Debug.Log (string.Format ("seriesRollNumber: {0}, numberDiceSelected: {1}, result: {2}", seriesRollNumber, numberDiceSelected, result.ToString()));


		return result;
	}

	private void displayDice(){
		die1.displayDie ();
		die2.displayDie ();
		die3.displayDie ();
		die4.displayDie ();
		die5.displayDie ();
		die6.displayDie ();
	}

	// Use this for initialization
	void Start () {
		// create a simple test set of dice
		dice.Add(DieFactory.createSeries1());
		dice.Add(DieFactory.createSeries1());
		dice.Add(DieFactory.createSeries1());
		dice.Add(DieFactory.createSeries2());
		dice.Add(DieFactory.createScout());
		dice.Add(DieFactory.createCaptain());

		GameObject dobject = transform.Find("Die1").gameObject;
		die1 = dobject.GetComponent<DieView>();
		die1.model = (DieModel)dice[0];
		die1.displayDie();

		die2 = transform.Find ("Die2").gameObject.GetComponent<DieView>();
		die2.model = (DieModel)dice [1];
		die2.displayDie ();

		die3 = transform.Find ("Die3").gameObject.GetComponent<DieView>();
		die3.model = (DieModel)dice [2];
		die3.displayDie ();

		die4 = transform.Find ("Die4").gameObject.GetComponent<DieView>();
		die4.model = (DieModel)dice [3];
		die4.displayDie ();
			
		die5 = transform.Find ("Die5").gameObject.GetComponent<DieView>();
		die5.model = (DieModel)dice [4];
		die5.displayDie ();

		die6 = transform.Find ("Die6").gameObject.GetComponent<DieView>();
		die6.model = (DieModel)dice [5];
		die6.displayDie ();

		// lock slots
		lockSlots.Add (transform.Find ("LockSlot1").gameObject);
		lockSlots.Add (transform.Find ("LockSlot2").gameObject);
		lockSlots.Add (transform.Find ("LockSlot3").gameObject);
		lockSlots.Add (transform.Find ("LockSlot4").gameObject);
		lockSlots.Add (transform.Find ("LockSlot5").gameObject);
		lockSlots.Add (transform.Find ("LockSlot6").gameObject);


		GameObject dfed = transform.Find ("Die1").gameObject;
		SpriteRenderer rend = dfed.GetComponent<SpriteRenderer> ();
		rend.enabled = false;



	}
	
	// Update is called once per frame
	void Update () {
		if (previousDiceRolled != numberDiceRolled) {

		}
	}
}

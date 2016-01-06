using UnityEngine;
using System.Collections;



public class ItemBase : MonoBehaviour {

	public int lookupID;
	public eitemClass itemClass = eitemClass.tool;
	public string itemName;
	public string itemImage;
	public string description;
	public ArrayList icons = new ArrayList();
	public ArrayList skillModifiers = new ArrayList();
	public ArrayList tags = new ArrayList();
	public int maxNumber = 1;
	public eitemType itemType = eitemType.held;
	public int typeWeight = 1;
	public eitemValue value = eitemValue.cheap;
	public int prerequisite = 0;
	public int consumes = 0;
	public int maxStack = 4;

	/* typeweight is the value of this item applied to the maximum of a specific itemType (weapon, item, armor).
	 max typeWeight for each itemType is typically 4. Armor, could be itemType 2 with a typeWeight of 3. Meaning only one armor
	 can be equipped, while a cape with itemType 2 could have typeWeight of 1 - thus allowing a cape AND armor.		
	 While a sword would have typeweight 2, meaning TWO swords could be equipped.
	 A 2 handed axe would have typeweight 4 (forcing no shield or other held weapon)	
	*/

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


public static class ItemFactory{

	public static ItemBase createItem(){
		return new ItemBase();
	}

	public static ItemBase createItemWithValues(){
		return new ItemBase ();
	}


}
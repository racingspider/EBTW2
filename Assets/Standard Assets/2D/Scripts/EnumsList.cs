using UnityEngine;
using System.Collections;


public enum ecrewHealthTrackDamage{
	fatigue, fitness
};

public enum eitemClass{
	none, tool, axe,sword,bow,armor,book,javelin,spear,shield,supply,animal
};

public enum eitemType{
	inventoryOnly,held,trinket,head,body,hands,feet,animal,reagent,crewMember,equipment
};

public enum ebodyLocation{
	legs,arms,head,body
};

public enum eitemValue{
	none,cheap,inexpensive,moderate,reasonable,expensive,exorbitant
};

public enum edieBaseType{
	none,series1,series2,series3,scout,captain,adventurer,scribe
};


public enum edieIcon{
	blank,blades,boots,cups,scrolls,maps,wands,minds,shields,bonus,bandages,reroll,hearts,provisions,
	timePass,injury,fatigue,wayward,camp,disaster,forced,rally,
	reagents
};

// frozen means it can't be used at all.
public enum edieState{
	outOfPlay, inHand, locked, selected, frozen
};

public enum egameEffects{
	none, consumeProvisions, increaseFatigue, decreaseFatigue, increaseFitness, decreaseFitness, progress, halt, backtrack
};

// these boots, cups, etc should be in the same order as the edieicon enum so they have the same value...
public enum ewaypointType{
	waypointStart,waypointBlades,
	waypointBoots,waypointCups, waypointScrolls, waypointMaps, waypointWands, waypointMinds, waypointShields,
	waypointIcon, waypointCamp, waypointEncounter, waypointOptionalEncounter, waypointFinal
};


static class DieIconStringNames{
	public static string getFilename(edieIcon icon){
		switch (icon) {
		case edieIcon.bandages:
			return "Sprites/Icons/bandages";
		case edieIcon.blades:
			return "Sprites/Icons/blades";
		case edieIcon.blank:
			return "Sprites/Icons/boots";
		case edieIcon.bonus:
			return "Sprites/Icons/bonus1";
		case edieIcon.boots:
			return "Sprites/Icons/boots";
		case edieIcon.camp:
			return "Sprites/Icons/camp";
		case edieIcon.cups:
			return "Sprites/Icons/cups";
		case edieIcon.disaster:
			return "Sprites/Icons/disaster";
		case edieIcon.fatigue:
			return "Sprites/Icons/fatigue";
		case edieIcon.forced:
			return "Sprites/Icons/forced";
		case edieIcon.hearts:
			return "Sprites/Icons/hearts";
		case edieIcon.injury:
			return "Sprites/Icons/injury";
		case edieIcon.maps:
			return "Sprites/Icons/maps";
		case edieIcon.minds:
			return "Sprites/Icons/minds";
		case edieIcon.provisions:
			return "Sprites/Icons/provisions";
		case edieIcon.rally:
			return "Sprites/Icons/rally";
		case edieIcon.reroll:
			return "Sprites/Icons/reroll";
		case edieIcon.scrolls:
			return "Sprites/Icons/scrolls";
		case edieIcon.shields:
			return "Sprites/Icons/shields";
		case edieIcon.timePass:
			return "Sprites/Icons/timePass";
		case edieIcon.wands:
			return "Sprites/Icons/wands";
		case edieIcon.wayward:
			return "Sprites/Icons/wayward";
		default:
			return "Sprites/Icons/boots";


		}
	}
}
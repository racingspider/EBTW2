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
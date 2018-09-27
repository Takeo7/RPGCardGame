using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "Card",menuName = "Custom/Card")]
public class Card : ScriptableObject {
	[Header("MainProperties")]
	public int index;
	public DeckType deckType;
	public string Name;
	public Sprite icon;
	[TextArea(5,5)]
	public string description;
	[HideInInspector]
	public string aggro;
	[Header("CardProperties")]
	public int price;
	[HideInInspector]
	public CardType cardType;
	[HideInInspector]
	public int toolbar;


	#region IF_CONSUMABLE
	[HideInInspector]
	public bool isConsumable;
	[HideInInspector]
	public int consumableBonus;
	[HideInInspector]
	public bool enemyOnly;
	#endregion
	#region IF_MONSTER
	[HideInInspector]
	public bool isMonster;
	[HideInInspector]
	public int monsterIndex;
	[HideInInspector]
	public byte monsterLevel;
    [HideInInspector]
    public byte treasures;
    [HideInInspector]
	public MonsterType monsterType;

	#endregion
	#region IF_RACE
	[HideInInspector]
	public bool isRace;
	[HideInInspector]
	public RaceType raceType;
	#endregion
	#region IF_CLASS
	[HideInInspector]
	public bool isClass;
	[HideInInspector]
	public ClassType classType;
	#endregion
	#region IF_EQUIPMENT
	[HideInInspector]
	public bool isEquipment;
	[HideInInspector]
	public EquipmentType equipmentType;
	[HideInInspector]
	public RaceType raceRestriction;
	[HideInInspector]
	public RaceType racePermission;
	[HideInInspector]
	public ClassType classRestriction;
	[HideInInspector]
	public ClassType classPermission;
	[HideInInspector]
	public SexType sexRestriction;
	[HideInInspector]
	public bool isBig;
	[HideInInspector]
	public bool isTwoHanded;
	[HideInInspector]
	public int equipmentBonus;
	[HideInInspector]
	public int escapeBonus;
	#endregion

	/*public void ChangeName()
	{
		string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
		AssetDatabase.RenameAsset(assetPath, Name);
		AssetDatabase.SaveAssets();
	}*/


}
public enum DeckType { Door, Treasure}
public enum CardType { Curse,Equipment,Race,Class,Monster,Consumable}
public enum ClassType { NaN,Default,Wizard,Warrior,Rogue,Cleric, SuperMunchkin }
public enum RaceType { Default, Human,Elf,Dwarf,Hobbit,Bastard}
public enum EquipmentType { Helmet,Armor,Weapon,Kneepads,Boots,Others }
public enum SexType { NaN, Male, Female}
public enum MonsterType { Default, Undead}

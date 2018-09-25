using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(Card))]
public class CardSccriptableEditor : Editor {

	int toolbar;
	bool isFirst = true;
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		Card card = (Card)target;
		if(GUILayout.Button("THE BUTTON TO RULE EM ALL"))
		{
			//card.ChangeName();
		}
		if (isFirst)
		{
			isFirst = false;
			toolbar = GUILayout.Toolbar(card.toolbar,
			new string[] { CardType.Curse.ToString(), CardType.Equipment.ToString(), CardType.Race.ToString(), CardType.Class.ToString(), CardType.Monster.ToString(), CardType.Consumable.ToString() });
			switch (card.toolbar)
			{
				case 0:
					card.cardType = CardType.Curse;
					break;
				case 1:
					card.cardType = CardType.Equipment;
					CheckEquipmentCard(card);
					break;
				case 2:
					card.cardType = CardType.Race;
					CheckRaceCard(card);
					break;
				case 3:
					card.cardType = CardType.Class;
					CheckClassCard(card);
					break;
				case 4:
					card.cardType = CardType.Monster;
					CheckMonsterCard(card);
					break;
				case 5:
					card.cardType = CardType.Consumable;
					CheckConsumibleCard(card);
					break;
			}
		}
		else
		{
			toolbar = GUILayout.Toolbar(toolbar,
						new string[] { CardType.Curse.ToString(), CardType.Equipment.ToString(), CardType.Race.ToString(), CardType.Class.ToString(), CardType.Monster.ToString(), CardType.Consumable.ToString() });
			card.toolbar = toolbar;
			switch (toolbar)
			{
				case 0:
					card.cardType = CardType.Curse;
					break;
				case 1:
					card.cardType = CardType.Equipment;
					CheckEquipmentCard(card);
					break;
				case 2:
					card.cardType = CardType.Race;
					CheckRaceCard(card);
					break;
				case 3:
					card.cardType = CardType.Class;
					CheckClassCard(card);
					break;
				case 4:
					card.cardType = CardType.Monster;
					CheckMonsterCard(card);
					break;
				case 5:
					card.cardType = CardType.Consumable;
					CheckConsumibleCard(card);
					break;
			}
		}	
	}
	void CheckConsumibleCard(Card card)
	{
		AllFalse(card);
		if (card.cardType == CardType.Consumable)
		{
			card.isConsumable = true;
			card.consumableBonus = EditorGUILayout.IntField("ConsumableBonus", card.consumableBonus);
			card.enemyOnly = EditorGUILayout.Toggle("Enemy Only", card.enemyOnly);
		}
		else
		{
			card.isConsumable = false;
			card.consumableBonus = 0;
			card.enemyOnly = false;
		}
	}
	void CheckMonsterCard(Card card)
	{
		AllFalse(card);
		if (card.cardType == CardType.Monster)
		{
			card.isMonster = true;
			card.monsterIndex = EditorGUILayout.IntField("MonsterIndex", card.monsterIndex);
			card.monsterLevel = (byte)EditorGUILayout.IntField("MonsterLevel", card.monsterLevel);
			card.aggro = EditorGUILayout.TextField("Aggro", card.aggro, GUILayout.Height(100));
		}
		else
		{
			card.isMonster = false;
			card.monsterIndex = 0;
			card.monsterLevel = 0;

		}
	}
	void CheckRaceCard(Card card)
	{
		AllFalse(card);
		if (card.cardType == CardType.Race)
		{
			card.isRace = true;
			card.raceType = (RaceType)EditorGUILayout.EnumPopup("Race", card.raceType);
		}
		else
		{
			card.isRace = false;
			card.raceType = RaceType.Human;
		}
	}
	void CheckClassCard(Card card)
	{
		AllFalse(card);
		if (card.cardType == CardType.Class)
		{
			card.isClass = true;
			card.classType = (ClassType)EditorGUILayout.EnumPopup("Class", card.classType);
		}
		else
		{
			card.isClass = false;
			card.classType = ClassType.Default;
		}
	}
	void CheckEquipmentCard(Card card)
	{
		AllFalse(card);
		if (card.cardType == CardType.Equipment)
		{
			card.isEquipment = true;
			card.equipmentBonus = EditorGUILayout.IntField("Bonus", card.equipmentBonus);
			card.equipmentType = (EquipmentType)EditorGUILayout.EnumPopup("EquipmentType", card.equipmentType);
			card.isBig = EditorGUILayout.Toggle("Is Big", card.isBig);
			card.isTwoHanded = EditorGUILayout.Toggle("Is Two Handed", card.isTwoHanded);
			card.raceRestriction = (RaceType)EditorGUILayout.EnumPopup("RaceRestriction", card.raceRestriction);
			card.racePermission = (RaceType)EditorGUILayout.EnumPopup("RacePermission", card.racePermission);
			card.classRestriction = (ClassType)EditorGUILayout.EnumPopup("ClassRestriction", card.classRestriction);
			card.classPermission = (ClassType)EditorGUILayout.EnumPopup("ClassPermission", card.classPermission);
			card.sexRestriction = (SexType)EditorGUILayout.EnumPopup("SexRestriction", card.sexRestriction);
		}
		else
		{
			card.isEquipment = false;
			card.equipmentType = EquipmentType.Others;
			card.isBig = false;
			card.isTwoHanded = false;
			card.sexRestriction = SexType.NaN;
		}
	}
	void AllFalse(Card card)
	{
		card.isConsumable = false;
		card.isMonster = false;
		card.isRace = false;
		card.isClass = false;
		card.isEquipment = false;
	}
}

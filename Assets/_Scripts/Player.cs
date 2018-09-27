using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region Singleton
    public static Player instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    #region Variables
    public int MyID;
	public bool canEquip;
	public bool canDiscard;
    public GameObject cardPrefab;
    public Transform handTransform;

    public GameManager GM;

    private void Start()
    {
        MyID = PhotonNetwork.player.ID;
    }

    public List<Card> Hand;


    #region Turns

    public bool MyTurn;

    
    #endregion

    #region Equipement
    public Card[] race;
	public Card[] clas;

	public Card helmet;
	public Card armour;
	public Card hand1; //Bool
	public Card hand2;
	public Card kneepads;
	public Card boots;
	public List<Card> others;
	#endregion

	#region Stats
	//Base Stats
	public byte level = 1;
	public byte escape = 4;
	public SexType sex;

	//Power stats
	public int power = 1;
	public bool isBig = false;
	#endregion

	#region Temp
	public Card changeableCard;
	public Card cardToEquip;
	public Card unequippedCard;
	public bool isSelectedWichHand;
	public int isSelectedWichNUMHand;
	public bool isSelectedWichRace;
	public int isSelectdWichNUMRace;
	public bool wantToChangeRace;
	public int isSelectdWichNUMClass;
	public bool wantToChangeClass;
	public bool isSelectedWichClass;

	#endregion

	#endregion

	public void TryEquipCard(Card card)
	{
		if (CheckIfEquipable(card))
		{
			if (CheckPlaceHolder(card))
			{
				//Pregunta si cambia de carta
			}
			else 
			{
				cardToEquip = card;
				//Show Dialog with the changeable card
			}
		}
	}
    [PunRPC]
	public void GetCards(int[] id)
	{
        //FOR TESTING
        if (MyTurn)
        {
            int length = id.Length;
            for (int i = 0; i < length; i++)
            {
                Hand.Add(GM.IndexCards[id[i]]);
                InstantiateHandCard(GM.IndexCards[id[i]]);
                //hand new card visual
            }
        }
	}
    [PunRPC]
    public void GetCards(int id)
    {
        if (MyTurn)
        {
            Hand.Add(GM.IndexCards[id]);
            InstantiateHandCard(GM.IndexCards[id]);
        }
    }
    [PunRPC]
    public void GetFirstHand(int[] ids, int playerid)
    {
        if (playerid == PhotonNetwork.player.ID)
        {
            int length = ids.Length;
            for (int i = 0; i < length; i++)
            {
                Hand.Add(GM.IndexCards[ids[i]]);
                InstantiateHandCard(Hand[i]);
                //hand new card visual
            }
        }
    }
    public void InstantiateHandCard(Card card)
    {
        GameObject g = Instantiate(cardPrefab, handTransform);
        g.GetComponent<CardInfo>().RefillCard(card);
    }
    //For Treasure Button
    public void HitTreasure()
    {
        GM.PV.RPC("DrawTreasureCards", PhotonTargets.All);
    }

	#region Equip Methods
	bool CheckIfEquipable(Card card)
	{
		bool Check = false;
		bool Check2 = false;
		int length = race.Length;
		for (int i = 0; i < length; i++)
		{
			if (card.raceRestriction != race[i].raceType && card.racePermission == race[i].raceType && card.sexRestriction != sex)
			{
				Check = true;
			}
		}
		int length1 = clas.Length;
		for (int i = 0; i < length1; i++)
		{
			if(card.classRestriction != clas[i].classType && card.classPermission == clas[i].classType && card.sexRestriction != sex)
			{
				Check2 = true;
			}
		}
		if(Check == false || Check2 == false)
		{
			Check = false;
		}


		return Check;
	}
	bool CheckPlaceHolder(Card card)
	{
		bool Check = false;

		if (card.isRace)
		{
			if (race == null)
			{
				//Esta Vacio
				Check = true;
			}

		}
		else if (card.isClass)
		{
			if (clas == null)
			{
				Check = true;
			}
		}
		else if (card.isEquipment)
		{
			switch (card.equipmentType)
			{
				case EquipmentType.Helmet:
					if (helmet == null)
					{
						Check = true;
						Check = CheckBig(card, helmet);
					}
					break;
				case EquipmentType.Armor:
					if (armour == null)
					{
						Check = true;
						Check = CheckBig(card, armour);
					}
					break;
				case EquipmentType.Weapon:
					if (card.isTwoHanded)
					{
						if (hand1 == null)
						{
							Check = true;
							Check = CheckBig(card, hand1);
						}
					}
					else
					{
						if(hand1 == null)
						{
							Check = true;
							Check = CheckBig(card, hand1);
						}
						else if(hand2 == null)
						{
							Check = true;
							Check = CheckBig(card, hand2);
						}
					}
					break;
				case EquipmentType.Kneepads:
					if ( kneepads == null)
					{
						Check = true;
						Check = CheckBig(card, kneepads);
					}
					break;
				case EquipmentType.Boots:
					if (boots == null)
					{
						Check = true;
						Check = CheckBig(card, boots);
					}
					break;
				case EquipmentType.Others:
					Check = true;
					Check = CheckBig(card,others);
					break;
			}
		}

		return Check;
	}
	bool CheckBig(Card card,Card equipped)
	{
		bool Check = false;
		int length = race.Length;
		for (int i = 1; i < length-1; i++)
		{
			if (race[i].raceType == RaceType.Dwarf)
			{
				Check = true;
			}
			else
			{
				if (isBig)
				{
					if (card.isBig && equipped.isBig)
					{
						Check = true;
						changeableCard = equipped;
					}
					else
					{
						changeableCard = null;
					}
				}
			}
		}

		return Check;
	}
	bool CheckBig(Card card, List<Card> equipped)
	{
		bool Check = false;
		int length = race.Length;
		for (int i = 1; i < length-1; i++)
		{
			if (race[i].raceType == RaceType.Dwarf)
			{
				Check = true;
			}
			else
			{
				if (isBig)
				{
					int length1 = equipped.Count;
					for (int j = 0; j < length1; j++)
					{
						if (card.isBig && equipped[j].isBig)
						{
							Check = true;
							changeableCard = equipped[j];
							break;
						}
						else
						{
							changeableCard = null;
						}
					}
				}
			}
		}
		return Check;
	}
	public void Equip(Card card)
	{
		if (card.isEquipment)
		{
			switch (card.equipmentType)
			{
				case EquipmentType.Helmet:
					if (helmet != null)
					{
						UnEquip(helmet);
					}
					helmet = card;
					break;
				case EquipmentType.Armor:
					if (armour != null)
					{
						UnEquip(armour);
					}
					armour = card;
					break;
				case EquipmentType.Weapon:
					if (card.isTwoHanded)
					{
						if (hand1 != null)
						{
							UnEquip(hand1);
							hand1 = card;
						}
					}
					else
					{
						if (hand1 == null)
						{
							hand1 = card;
						}
						else if (hand1 != null && hand2 == null && hand1.isTwoHanded == false)
						{
							hand2 = card;
						}
						else if (hand1 != null && hand2 == null && hand1.isTwoHanded)
						{
							UnEquip(hand1);
							hand1 = card;
						}
						else if (hand1 != null && hand2 != null)
						{
							if (isSelectedWichHand)
							{
								if (isSelectedWichNUMHand == 0)
								{
									UnEquip(hand1);
									hand1 = card;
								}
								else if (isSelectedWichNUMHand == 1)
								{
									UnEquip(hand2);
									hand2 = card;
								}
								isSelectedWichHand = false;
							}
							else
							{
								//ShowDialog with the two hands to select wich one the user dont want
								WichOneToUnEquipDialog();
							}

							break;
						}
					}
					break;
				case EquipmentType.Kneepads:
					if (kneepads != null)
					{
						UnEquip(kneepads);
					}
					kneepads = card;
					break;
				case EquipmentType.Boots:
					if (boots != null)
					{
						UnEquip(boots);
					}
					boots = card;
					break;
				case EquipmentType.Others:
					others.Add(card);
					break;
			}
			power += card.equipmentBonus;
		}
		else if(card.isRace)
		{
			if (isSelectedWichRace)
			{
				if (isSelectdWichNUMRace == 0)
				{
					UnEquip(race[1]);
					race[1] = card;
				}
				else if (isSelectdWichNUMRace == 1)
				{
					if(race[2] != null)
					{
						UnEquip(race[2]);
					}
					race[2] = card;
				}
				isSelectedWichRace = false;
			}
			else
			{
				if (race[0] != null && card.raceType == RaceType.Bastard)
				{
					//Dialog U CANT BE DOUBLE BASTARD
					//cantBeDoubleBastard.isActive(true);
				}
				else if (card.raceType == RaceType.Bastard)
				{
					race[0] = card;
				}
				else if (race[0] != null && race[1].raceType == RaceType.Human)
				{
					UnEquip(race[1]);
					race[1] = card;
				}
				else if (race[0] != null && race[1] != null)
				{
					//Dialog IF U WANT TO CHANGE THE RACE NUMBER 1
					WichRaceToChangeDialog();
				}
				else if (race[0] == null && race[1].raceType == RaceType.Human)
				{
					UnEquip(race[1]);
					race[1] = card;
				}
				else if (race[0] == null && race[1] != null)
				{
					//Dialog WANT TO CHANGE RACE
					if (wantToChangeRace)
					{
						UnEquip(race[1]);
						race[1] = card;
						wantToChangeRace = false;
					}
					else
					{
						WantToChangeRace();
					}
				}
			}
		}
		else if (card.isClass)
		{
			if (isSelectedWichClass)
			{
				if (isSelectdWichNUMClass == 0)
				{
					UnEquip(clas[1]);
					clas[1] = card;
				}
				else if (isSelectdWichNUMClass == 1)
				{
					if (clas[2] != null)
					{
						UnEquip(clas[2]);
					}
					clas[2] = card;
				}
				isSelectedWichClass = false;
			}
			else
			{
				if (clas[0] != null && card.classType == ClassType.SuperMunchkin)
				{
					//Dialog U CANT BE DOUBLE BASTARD
					//cantBeDoubleSuperMunchkin.isActive(true);
				}
				else if (card.classType == ClassType.SuperMunchkin)
				{
					clas[0] = card;
				}
				else if (clas[0] != null && clas[1].classType == ClassType.Default)
				{
					UnEquip(clas[1]);
					clas[1] = card;
				}
				else if (clas[0] != null && clas[1] != null)
				{
					//Dialog IF U WANT TO CHANGE THE RACE NUMBER 1
					WichClassToChangeDialog();
				}
				else if (clas[0] == null && clas[1].classType == ClassType.Default)
				{
					UnEquip(clas[1]);
					clas[1] = card;
				}
				else if (clas[0] == null && clas[1] != null)
				{
					//Dialog WANT TO CHANGE CLASS
					if (wantToChangeClass)
					{
						UnEquip(clas[1]);
						clas[1] = card;
						wantToChangeClass = false;
					}
					else
					{
						WantToChangeClass();
					}
				}
			}

		}
		escape -= (byte)card.escapeBonus;

	}
	public void DontEquip()
	{
		changeableCard = null;
		cardToEquip = null;
	}
	public void UnEquip(Card variable)
	{
		power -= variable.equipmentBonus;
		escape += (byte)variable.escapeBonus;
		//Descartar discards.Add(variable);
		variable = null;
	}
	public void WichOneToUnEquipDialog()
	{
		
	}
	public void WichOneToUnEquip(int i)
	{
		isSelectedWichNUMHand = i;
		isSelectedWichHand = true;
		Equip(cardToEquip);
	}
	public void WichRaceToChangeDialog()
	{

	}
	public void WichRaceToChange(int i)
	{
		isSelectdWichNUMRace = i;
		isSelectedWichRace = true;
		Equip(cardToEquip);
	}
	public void WantToChangeRace()
	{
		
	}
	public void WantToChangeRaceBool(bool WantToChangeRace)
	{
		wantToChangeRace = WantToChangeRace;
	}
	public void WichClassToChangeDialog()
	{

	}
	public void WantToChangeClass()
	{

	}
	public void WantToChangeClassBool(bool WantToChangeRace)
	{
		wantToChangeRace = WantToChangeRace;
	}
    #endregion
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.localPosition;
            stream.Serialize(ref pos);
        }
        else
        {
            Vector3 pos = Vector3.zero;
            stream.Serialize(ref pos);  // pos gets filled-in. must be used somewhere
        }
    }
}

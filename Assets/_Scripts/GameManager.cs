using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	#region Singleton

	public static GameManager instance;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	#endregion

	#region Variables

	Card monster;

	byte doorsToOpen;
	bool firstDraw;

	Player currentPlayer;
	List<Player> players;

	private List<Card> treasureDeck;
	private List<Card> doorDeck;
	private List<Card> discardDeck;

	#endregion

	#region Unity Methods

	private void Start()
	{
        if (PhotonNetwork.player.ID == 1)
        {
            ResetDecks();
            RefillCardsResources();
        }
	}

	#endregion

	#region CardMethods
	//Refill
	private void ResetDecks()
	{
		treasureDeck = new List<Card>();
		doorDeck = new List<Card>();
		discardDeck = new List<Card>();
	}
	void RefillCardsResources()
	{
		Card[] cardsTemp = Resources.LoadAll<Card>("Cards");
		int count = cardsTemp.Length;
		for (int i = 0; i < count; i++)
		{
			if (cardsTemp[i].deckType == DeckType.Door)
			{
				doorDeck.Add(cardsTemp[i]);
			}
			else if (cardsTemp[i].deckType == DeckType.Treasure)
			{
				doorDeck.Add(cardsTemp[i]);
			}
		}
		treasureDeck = ShuffleCards(DeckType.Treasure);
		doorDeck = ShuffleCards(DeckType.Door);

	}
	List<Card> ShuffleCards(DeckType d)
	{
		List<Card> tempList = new List<Card>();
		int countDeck;
		int totalcards;
		int rand;
		switch (d)
		{
			case DeckType.Door:
				 countDeck = doorDeck.Count;
				for (int i = 0; i < countDeck; i++)
				{
					totalcards = doorDeck.Count;
					rand = Random.Range(0, totalcards);
					tempList.Add(doorDeck[i]);
					doorDeck.Remove(doorDeck[i]);
				}
				break;
			case DeckType.Treasure:
				 countDeck = treasureDeck.Count;
				for (int i = 0; i < countDeck; i++)
				{
					totalcards = treasureDeck.Count;
					rand = Random.Range(0, totalcards);
					tempList.Add(treasureDeck[i]);
					treasureDeck.Remove(treasureDeck[i]);
				}
				break;
			default:
				Debug.LogWarning("Missing DeckType");
				break;
		}
		return tempList;
	}	

	//DiscardDeck
	public void DiscardCard(Card card)
	{
		discardDeck.Add(card);
	}

	//Player Methods
	public void DrawTreasureCards()
	{
		currentPlayer.GetCards(GetTreasures(monster.treasures));
		discardDeck.Add(monster);
		monster = null;
	}
	public void DrawDoorCard()
	{
		Card temp = GetCard(DeckType.Door);
		switch (temp.cardType)
		{
			case CardType.Curse:
				if (firstDraw)
				{
					//Apply
					//Show all card
					firstDraw = false;
				}
				else
				{
					currentPlayer.GetCards(temp);
					//Show player card
					//End turn
				}
				break;
			case CardType.Race:
			case CardType.Class:
				if (firstDraw)
				{
					//Show all card
					currentPlayer.GetCards(temp);
					firstDraw = false;
				}
				else
				{
					currentPlayer.GetCards(temp);
					//Show player card
					//End turn
				}
				break;
			case CardType.Monster:
				if (firstDraw)
				{
					//Show all card
					//Apply monster
					//StartCombat
				}
				else
				{
					currentPlayer.GetCards(temp);
					//ShowPlayerCard
					//End turn
				}
				break;
			default:
				break;
		}
	}
	Card GetCard(DeckType d)
	{
		Card temp = new Card();
		switch (d)
		{
			case DeckType.Door:
				temp = doorDeck[0];
				doorDeck.Remove(doorDeck[0]);
				break;
			case DeckType.Treasure:
				temp = treasureDeck[0];
				treasureDeck.Remove(treasureDeck[0]);
				break;
			default:
				Debug.LogWarning("Missing DeckType");
				break;
		}
		return temp;
	}
	List<Card> GetTreasures(int treasures)
	{
		List<Card> temp = new List<Card>();
		for (int i = 0; i < treasures; i++)
		{
			temp.Add(GetCard(DeckType.Treasure));
		}
		return temp;
	}

	#endregion
}

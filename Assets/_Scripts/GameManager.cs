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

    public Card[] IndexCards;

    public PhotonView PV;
    public PhotonView Player_PV;


    #endregion

    #region Unity Methods

    private void Start()
	{
        RefillCardsResources();
    }

    [PunRPC]
    public void StartMatch()
    {
        if (PhotonNetwork.player.ID == 1)
        {
            ResetDecks();

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
		IndexCards = Resources.LoadAll<Card>("Cards");
		int count = IndexCards.Length;
		for (int i = 0; i < count; i++)
		{
			if (IndexCards[i].deckType == DeckType.Door)
			{
				doorDeck.Add(IndexCards[i]);
			}
			else if (IndexCards[i].deckType == DeckType.Treasure)
			{
				doorDeck.Add(IndexCards[i]);
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
    [PunRPC]
	public void DiscardCard(int id)
	{
		discardDeck.Add(IndexCards[id]);
	}

	//Player Methods
    [PunRPC]
	public void DrawTreasureCards()
	{
        Player_PV.RPC("GetCards", PhotonTargets.All, GetTreasures(monster.treasures));
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
	int[] GetTreasures(int treasures)
	{
		int[] temp = new int[treasures];
		for (int i = 0; i < treasures; i++)
		{            
			temp[i] = GetCard(DeckType.Treasure).index;
		}
		return temp;
	}

	#endregion
}

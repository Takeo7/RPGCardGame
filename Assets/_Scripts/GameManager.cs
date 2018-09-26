using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public CardInfo AnimCardPrew;

    public Sprite[] Monsters;

	Card monster;

	byte doorsToOpen;
	bool firstDraw = true;

	Player currentPlayer;
	List<Player> players;

	public List<Card> treasureDeck = new List<Card>();
	public List<Card> doorDeck = new List<Card>();
	public List<Card> discardDeck = new List<Card>();

    public Card[] IndexCards;

    public PhotonView PV;
    public PhotonView Player_PV;
    public PhotonView Turn_PV;
    public PhotonView DS_PV;

    public DoorScript DS;
    public TurnManager TM;

    #endregion

    #region Unity Methods

    private void Start()
	{
        RefillCardsResources();
        TM.startTurnDelegate += SetFirstDrawTrue;
        DS.openFirstDoor += DrawDoorCard;
        DS.openSecondDoor += DrawSecondDoorCard;
        
    }

    [PunRPC]
    public void StartMatch()
    {
        if (PhotonNetwork.player.ID == 1)
        {            
            Debug.Log("This is first turn");
            TM.FirstTurn();
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
                treasureDeck.Add(IndexCards[i]);
            }
        }
        treasureDeck = ShuffleCards(DeckType.Treasure);
		doorDeck = ShuffleCards(DeckType.Door);

	}
	List<Card> ShuffleCards(DeckType d)
	{
		List<Card> tempList = new List<Card>();
		int countDeck = 0;
		int totalcards = 0;
		int rand = 0;
		switch (d)
		{
			case DeckType.Door:
				 countDeck = doorDeck.Count;
				for (int i = 0; i < countDeck; i++)
				{
					totalcards = doorDeck.Count;
					rand = Random.Range(0, totalcards);
                    if (rand == totalcards)
                    {
                        rand = 0;
                    }
                    tempList.Add(doorDeck[rand]);
					doorDeck.Remove(doorDeck[rand]);
				}
				break;
			case DeckType.Treasure:
				 countDeck = treasureDeck.Count;
				for (int i = 0; i < countDeck; i++)
				{
					totalcards = treasureDeck.Count;
					rand = Random.Range(0, totalcards);
                    if (rand == totalcards)
                    {
                        rand = 0;
                    }
					tempList.Add(treasureDeck[rand]);
					treasureDeck.Remove(treasureDeck[rand]);
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
    [PunRPC]
	public void DrawDoorCard()
	{
        if (PhotonNetwork.player.ID == 1)
        {
            Card temp = GetCard(DeckType.Door);
            PV.RPC("ActivateCardRPC", PhotonTargets.All, temp.index);
        }        
	}
    [PunRPC]
    public void ActivateCardRPC(int i)
    {
        Card temp = IndexCards[i];
        AnimCardPrew.ActivateAnimCard(temp);
    }
    [PunRPC]
    public void DrawSecondDoorCard()
    {
        Card temp = GetCard(DeckType.Door);
        if (PhotonNetwork.player.ID == TM.actualTurn)
        {            
            AnimCardPrew.Front.SetActive(true);
            AnimCardPrew.Back.SetActive(false);
            AnimCardPrew.anim.SetBool("SecondDraw", true);
            AnimCardPrew.secondDraw = true;
            AnimCardPrew.ActivateAnimCard(temp);
        }
        else
        {
            AnimCardPrew.Front.SetActive(false);
            AnimCardPrew.Back.SetActive(true);
            AnimCardPrew.ActivateAnimCard(temp);
        }
        
    }
    public void SetFirstDrawTrue()
    {
        firstDraw = true;
    }
    public void ApplyDoorCard(Card temp)
    {
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
                    Player_PV.RPC("GetCards", PhotonTargets.All, temp.index);
                    //Show player card
                    //End turn
                }
                break;
            case CardType.Race:
            case CardType.Class:
                if (firstDraw)
                {
                    //Show all card
                    Player_PV.RPC("GetCards", PhotonTargets.All, temp.index);
                    firstDraw = false;
                }
                else
                {
                    Player_PV.RPC("GetCards", PhotonTargets.All, temp.index);
                    //Show player card
                    //End turn
                }
                break;
            case CardType.Monster:
                if (firstDraw)
                {
                    DS_PV.RPC("ActivateMonster", PhotonTargets.All, temp.monsterIndex);
                    //Apply monster
                    //StartCombat
                }
                else
                {
                    Player_PV.RPC("GetCards", PhotonTargets.All, temp.index);
                    //ShowPlayerCard
                    //End turn
                }
                break;
            default:
                Debug.Log("There is no CardType");
                break;
        }
    }


	Card GetCard(DeckType d)
	{
		Card temp = new Card();
        Debug.Log(d);
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

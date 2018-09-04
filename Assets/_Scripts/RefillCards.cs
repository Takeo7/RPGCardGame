using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillCards : MonoBehaviour {

	public Card[] cards;
	int cardsCount;
	public GameObject cardPrefab;
	public RectTransform deck;

	private void Awake()
	{
		RefillCardsResources();
		RefillCardsWithPrefab();
	}
	void RefillCardsResources()
	{
		Card[] cardsTemp = Resources.LoadAll<Card>("Cards");
		cardsCount = cardsTemp.Length;
		cards = new Card[cardsCount];
		for (int i = 0; i < cardsCount; i++)
		{
			for (int j = 0; j < cardsCount; j++)
			{
				if (cardsTemp[j].index == i)
				{
					cards[i] = cardsTemp[j];
					j++;
				}
			}			
		}
	}
	void RefillCardsWithPrefab()
	{
		for (int i = 0; i < cardsCount; i++)
		{
			GameObject temp = Instantiate(cardPrefab, deck);
			CardInfo tempCardInfo = temp.GetComponent<CardInfo>();
			temp.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			tempCardInfo.RefillCard(cards[i]);
		}
	}
}

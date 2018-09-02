using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour {

	#region Variable
	public Card cardInfo;
	[Space]

	[Header("UnityUI")]
	public Text cardName;
	public Text cardbonus;
	public Image cardIcon;
	public Text cardDescription;
	public Text cardBigOrSmall;
	public Text cardValue;
	#endregion

	public void RefillCard(Card card)
	{
		cardInfo = card;
		cardName.text = card.name;
		cardbonus.text = ""+card.consumableBonus;
		cardIcon.sprite = card.icon;
		cardDescription.text = card.description;
		if (card.isBig) { cardBigOrSmall.text = "Big"; } else { cardBigOrSmall.text = ""; }
		cardValue.text = card.price+" Gold";
	}
}

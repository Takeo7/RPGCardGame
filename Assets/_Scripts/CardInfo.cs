using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour {

	#region Variable
	public Card cardInfo;
    public Animator anim;
    public GameManager GM;
    public Player player;

    public GameObject Front;
    public GameObject Back;

    public bool secondDraw = false;

	[Space]

	[Header("UnityUI")]
	public Text cardName;
	public Text cardbonus;
	public Image cardIcon;
	public Text cardDescription;
	public Text cardBigOrSmall;
	public Text cardValue;
    public CardType cardType;
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
        cardType = card.cardType;
	}

    public void EndFirstAnim()
    {
        if (secondDraw == false)
        {
            StartCoroutine("SecondAnim");
        }
        else
        {
            StartCoroutine("SecondDraw");
        }
    }

    public void DesactivateAnimCard()
    {
        gameObject.SetActive(false);
    }
    public void ActivateAnimCard(Card card)
    {
        RefillCard(card);
        gameObject.SetActive(true);
    }

    IEnumerator SecondDraw()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("ToHand");
    }

    IEnumerator SecondAnim()
    {
        yield return new WaitForSeconds(1f);
        switch (cardType)
        {
            case CardType.Curse:
                //Curse ANIMATION
                //anim.SetTrigger("Curse");
                break;
            case CardType.Equipment:
                if (player.MyTurn)
                {
                    anim.SetTrigger("ToHand");
                }
                else
                {
                    anim.SetTrigger("ToCharacter");
                }
                break;
            case CardType.Race:
                if (player.MyTurn)
                {
                    anim.SetTrigger("ToHand");
                }
                else
                {
                    anim.SetTrigger("ToCharacter");
                }
                break;
            case CardType.Class:
                if (player.MyTurn)
                {
                    anim.SetTrigger("ToHand");
                }
                else
                {
                    anim.SetTrigger("ToCharacter");
                }
                break;
            case CardType.Monster:
                anim.SetTrigger("Monster");
                GM.DS.DesactivateDoor();
                break;
            case CardType.Consumable:
                if (player.MyTurn)
                {
                    anim.SetTrigger("ToHand");
                }
                else
                {
                    anim.SetTrigger("ToCharacter");
                }
                break;
            default:
                break;
        }
    }

    public void ApplyCard()
    {
        if (PhotonNetwork.player.ID == 1)
        {
            GM.ApplyDoorCard(cardInfo);
        }        
    }


}

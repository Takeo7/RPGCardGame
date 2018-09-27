using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	Player player;
	GameManager GM;

	PlayerCharacter pch;

	public bool isMonster = false;
	public bool isCharacter = false;
	public bool isWaterWell = false;

	void Start()
	{
		player = Player.instance;
		GM = GameManager.instance;
		if (isCharacter)
		{
			pch = transform.parent.GetComponent<PlayerCharacter>();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("OnPointerEnter");
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log("OnPointerExit");
	}
	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log(eventData.pointerDrag.name+" was dropped on" + gameObject.name);
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		CardInfo c = d.GetComponent<CardInfo>();

		/*if(d != null)
		{
			d.parentToReturnTo = transform;
		}*/
		if (isMonster && !isCharacter && !isWaterWell && player.MyTurn && (c.cardInfo.cardType == CardType.Consumable || c.cardInfo.cardType == CardType.Curse) && GM.monster != null)
		{
			//Apply card to monster
			Debug.Log("DropTOMonster");
		}
		else if(!isMonster && isCharacter && !isWaterWell && player.MyTurn && player.canEquip && /*CanEquipFunction Instead of that -->*/c.cardInfo.cardType == CardType.Equipment && pch.playerID == PhotonNetwork.player.ID)
		{
			//EQUIP
			Debug.Log("DropToEquip");
		}
		else if(!isMonster && !isCharacter && isWaterWell && player.MyTurn && player.canDiscard)
		{
			//DISCARD
			Debug.Log("DropToDiscard");
		}
	}
}

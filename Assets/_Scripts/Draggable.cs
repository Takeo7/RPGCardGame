using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler {

	public Transform canvas;
	public Transform hand;
	public float yOffset;
	public float xOffset;
	public Transform parentToReturnTo = null;
	CanvasGroup CG;
	GameManager GM;
	CardInfo cardInfo;
	void Start()
	{
		cardInfo = GetComponent<CardInfo>();
		GM = GameManager.instance;
		canvas = GM.canvasTransform;
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		StartCoroutine("OnPointerDownCO", eventData);
	}
	IEnumerator OnPointerDownCO(PointerEventData eventData)
	{
		yield return new WaitForSeconds(0.25f);
		GM.previewBiggerCard.RefillCard(cardInfo.cardInfo);
		GM.previewBigCardGO.SetActive(true);
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		GM.previewBigCardGO.SetActive(false);
	}
	void StopBigPreview()
	{
		StopCoroutine("OnPointerDownCO");
		GM.previewBigCardGO.SetActive(false);
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		StopBigPreview();
		Debug.Log("OnBeginDrag");
		parentToReturnTo = transform.parent;
		transform.SetParent(canvas);
		CG = GetComponent<CanvasGroup>();
		CG.blocksRaycasts = false;
		CG.alpha = 0.5f;
	}
	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("OnDrag");
		transform.localPosition = new Vector2(eventData.position.x- xOffset, eventData.position.y-yOffset);
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDrag");
		transform.SetParent(parentToReturnTo);
		CG.blocksRaycasts = true;
		CG.alpha = 1f;
	}
	

}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardHandler : MonoBehaviour,IDragHandler,IEndDragHandler, IBeginDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    CardStatus card;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;

    private void Start()
    {
        card = GetComponent<CardStatus>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DeckSlot deckSlot = transform.parent.GetComponent<DeckSlot>();
        if (deckSlot != null)
        {
            deckSlot.OnCardRemoved();
        }

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var hit in results)
        {
            if (hit.gameObject.CompareTag("CardList"))
            {
                SetParentAfterDrag(hit.gameObject.transform);
            }
        }
        transform.position = parentAfterDrag.position;
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true;
    }

    public CardStatus GetCardStatus()
    {
        return card;
    }

    public void SetParentAfterDrag(Transform parent)
    {
        parentAfterDrag = parent;
    }
}
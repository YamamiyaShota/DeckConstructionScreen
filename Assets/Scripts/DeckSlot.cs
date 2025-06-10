using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DeckSlot : MonoBehaviour,IDropHandler
{
    public bool IsCardSet = false;
    [SerializeField] int deckIndex;
    [SerializeField] DeckManager deckManager;

    public void OnDrop(PointerEventData eventData)
    {
        CardHandler droppedCard = eventData.pointerDrag.GetComponent<CardHandler>();
        if (droppedCard != null)
        {
            if (!IsCardSet)
            {
                droppedCard.SetParentAfterDrag(transform);
                droppedCard.transform.position = transform.position;
                IsCardSet = true;

                OnCardSet(droppedCard.GetCardStatus());
            }
            else
            {
                droppedCard.SetParentAfterDrag(droppedCard.parentAfterDrag);
            }
        }
    }

    public void OnCardSet(CardStatus card)
    {
        deckManager.SetDeck(deckIndex, card);
    }

    public void OnCardRemoved()
    {
        deckManager.RemoveDeck(deckIndex);
        IsCardSet = false;
    }
}
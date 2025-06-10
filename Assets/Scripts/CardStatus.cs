using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardStatus : MonoBehaviour
{
    [SerializeField] CardData cardData;
    [SerializeField] int cardIndex;
    [SerializeField] int cost;
    [SerializeField] int helthPoint;
    [SerializeField] Sprite icon;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI helthText;

    public int GetNumber;
    public int Cost { get { return cost; } }
    public int HelthPoint { get { return helthPoint; } }

    private void Start()
    {
        SetStatus();
    }

    public void SetStatus()
    {
        var card = cardData.CharacterList[cardIndex];
        cost = card.cost;
        helthPoint = card.helthPoint;
        icon = card.icon;

        image.sprite = icon;
        costText.text = $"cost:{cost}";
        helthText.text = $"HP: {helthPoint}";
    }
}

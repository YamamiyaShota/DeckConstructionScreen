using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerate : MonoBehaviour
{
    [SerializeField] DeckManager deckManager;
    [SerializeField] int generateCount; //生成回数
    [SerializeField] GameObject[] cards; //生成用のカードオブジェクト
    [SerializeField] GameObject content;

    void Start()
    {
        generateCount = Mathf.Clamp(generateCount, 4, 255);
        Generate();
    }

    public void Generate()
    {
        for (int i = 0; i < generateCount; i++)
        {
            var cardIndex = Random.Range(0, cards.Length - 1);
            var card = Instantiate(cards[cardIndex], content.transform.position, Quaternion.identity);
            card.transform.SetParent(content.transform);
            card.transform.localScale = Vector3.one;
            var cardStatus = card.GetComponent<CardStatus>();
            deckManager.AddCardList(cardStatus);
            cardStatus.GetNumber = i;
        }
    }
}

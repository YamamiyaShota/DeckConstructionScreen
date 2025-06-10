using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

public class DeckManager : MonoBehaviour
{
    [SerializeField] CardData cardData;
    [SerializeField] int totalCost;

    [SerializeField] GameObject content;
    [SerializeField] GameObject resultScreen;

    [SerializeField] TextMeshProUGUI totalCostText;
    [SerializeField] TextMeshProUGUI sortButtonText;
    [SerializeField] TextMeshProUGUI resultScreenText;

    [SerializeField] List<CardStatus> cardList;
    [SerializeField] List<CardStatus> deck = new List<CardStatus>(4);

    [SerializeField] int SortIndex = 0;
    public UnityAction OnClickSortButton;

    void Start()
    {
        totalCostText.text = $"Total cost:{totalCost}";
        if(SortIndex == 0)
        {
            sortButtonText.text = "ソート\n獲得順";
        }
        resultScreen.SetActive(false);
    }

    /// <summary>
    /// カードをリストに追加
    /// </summary>
    /// <param name="card"></param>
    public void AddCardList(CardStatus card)
    {
        cardList.Add(card);
    }

    /// <summary>
    /// カードをリストから削除
    /// </summary>
    /// <param name="card"></param>
    public void RemeveCardList(CardStatus card)
    {
        cardList.Remove(card);
    }

    /// <summary>
    /// デッキにカードをセットする
    /// </summary>
    /// <param name="index"></param>
    /// <param name="card"></param>
    public void SetDeck(int index , CardStatus card)
    {
        deck[index] = card;
        AddCost(card.Cost);
    }

    /// <summary>
    /// デッキからカードを除去する
    /// </summary>
    /// <param name="index"></param>
    public void RemoveDeck(int index)
    {
        RemoveCost(deck[index].Cost);
        deck[index] = null;
    }

    /// <summary>
    /// デッキにセットされているカードのコストを追加する
    /// </summary>
    /// <param name="cost"></param>
    public void AddCost(int cost)
    {
        totalCost += cost;
        totalCostText.text = $"Total cost:{totalCost}";
        TotalCost();
    }

    /// <summary>
    /// デッキから消されたカードのコスト分引く
    /// </summary>
    /// <param name="cost"></param>
    public void RemoveCost(int cost) 
    {
        totalCost -= cost;
        totalCostText.text = $"Total cost:{totalCost}";
        TotalCost();
    }

    /// <summary>
    /// トータルコストが10以上だったらトータルコストの色を赤にする、違うときは白にする
    /// </summary>
    public void TotalCost()
    {
        if(totalCost >= 10)
        {
            totalCostText.color = Color.red;
        }
        else
        {
            totalCostText.color = Color.white;
        }
    }

    public void OnClickSort()
    {
        OnClickSortButton = SortIndex switch
        {
            1 => SortByCost,
            2 => SortByHP,
            _ => SortByAquisitionOrder,
        };

        OnClickSortButton?.Invoke();

        SortIndex++;

        if (SortIndex > 2)
        {
            SortIndex = 0;
        }

        sortButtonText.text = SortIndex switch
        {
            1 => "ソート\nコスト順",
            2 => "ソート\nHP順",
            _ => "ソート\n獲得順"
        };
    }

    /// <summary>
    /// 獲得順にソート
    /// </summary>
    public void SortByAquisitionOrder()
    {
        cardList = cardList.OrderBy(x => x.GetNumber).ToList();
        SortCardList();
    }

    /// <summary>
    /// コストの大きい順にソート
    /// </summary>
    public void SortByCost()
    {
        cardList = cardList.OrderBy(x => x.Cost).ToList();
        SortCardList();
    }

    /// <summary>
    /// HPの小さい順にソート
    /// </summary>
    public void SortByHP()
    {
        cardList = cardList.OrderBy(x => x.HelthPoint).ToList();
        SortCardList();
    }

    /// <summary>
    /// カードリストを順番に並べる
    /// </summary>
    public void SortCardList()
    {
        // ソート順にTransformを並べ替え
        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].transform.SetSiblingIndex(i);
        }

        // GridLayoutGroupの再レイアウト
        var grid = content.GetComponent<GridLayoutGroup>();
        if (grid != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content.transform);
        }
    }

    public void OnClickDecision()
    {
        resultScreenText.text = totalCost switch
        {
            int cost when cost >= 10 => "コストが上限値を超えています。",
            int cost when cost <= 0 => "デッキに何もありません。",
            _ => "デッキ構築完了"
        };

        resultScreen.SetActive(true);
        StartCoroutine(HideObjectAfterDelay(resultScreen, 2f));
    }

    IEnumerator HideObjectAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
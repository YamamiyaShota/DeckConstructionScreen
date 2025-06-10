using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    [SerializeField] List<CardProfile> cardList;
    public List<CardProfile> CharacterList => cardList;

    [System.Serializable]
    public class CardProfile 
    {
        public int getNumber;
        public int cost;
        public int helthPoint;
        public Sprite icon;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Deck_SO : ScriptableObject
{
    const int MAX_CARDS = 40;
    public const int MAX_CARD_COUNT = 4;

    [System.Serializable]
    public class CardType
    {
        [SerializeField] public Card_SO cardType;
        [SerializeField] public int count;
    }

    public List<CardType> cards = new List<CardType>();

    public List<Card_SO> CardList
    {
        get
        {
            List<Card_SO> temp = new List<Card_SO>();
            foreach(CardType cardType in cards)
            {
                for(int i = 0; i < cardType.count; i++) temp.Add(cardType.cardType);
            }
            return temp;
        }
    }

    public bool ContainsCard(Card_SO cardType)
    {
        foreach(CardType c in cards)
        {
            if (c.cardType == cardType) return true;
        }
        return false;
    }

    public int TotalCards
    {
        get
        {
            int sum = 0;
            for(int i = 0; i < cards.Count; i++) sum += cards[i].count;
            return sum;
        }
    }

    public int CardsRemaining
    {
        get => MAX_CARDS - TotalCards;
    }
    
}

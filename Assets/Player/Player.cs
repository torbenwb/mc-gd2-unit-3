using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameEvent Defeated;

    Board hand;
    public GameObject cardPrefab;
    Stack<Card_SO> deck = new Stack<Card_SO>();
    List<Card_SO> discard = new List<Card_SO>();
    public Deck_SO myDeck;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI manaDisplay;
    private int health = 10;
    private int mana = 3;
    public float startDelay = 0.1f;
    public float effectDelay = 0.25f;
    public float endDelay = 0.1f;

    public bool MyTurn => TurnStateMachine.TurnState == TurnStateMachine.State.PlayerTurn;

    public void StartTurn()
    {
        if (TurnStateMachine.TurnState == TurnStateMachine.State.GameOver) return;
        TurnStateMachine.TurnState = TurnStateMachine.State.PlayerTurn;
        Mana = 3;
        DrawCard();
    }

    public void EndTurn(){
        if (TurnStateMachine.TurnState == TurnStateMachine.State.GameOver) return;
        FindObjectOfType<Enemy>().StartTurn();
    }


    public int Health
    {
        get => health;
        set
        {
            health = value;
            healthDisplay.text = health.ToString();
            if (Health <= 0) Defeated.RaiseEvent();
        }
    }

    public int Mana
    {
        get => mana;
        set
        {
            mana = value;
            manaDisplay.text = mana.ToString();
        }
    }

    void Awake()
    {
        hand = GetComponent<Board>();
        ShuffleDeck();
    }

    void Start()
    {
        Health = 10;
        Mana = 3;
        DrawCard();
        DrawCard();
        DrawCard();
        DrawCard();
        DrawCard();
    }

    public void ShuffleDeck()
    {
        
        List<Card_SO> temp = myDeck.CardList;
        while(temp.Count > 0)
        {
            int i = Random.Range(0, temp.Count);
            Card_SO card = temp[i];
            deck.Push(card);
            temp.RemoveAt(i);
        }
    }

    public void DrawCard()
    {
        if (deck.Count <= 0)
        {
            Debug.Log("No Cards!");
        }
        else
        {
            Card_SO card = deck.Pop();
            hand.NewBoardItem(cardPrefab).GetComponent<Card>().CardType = card;
        }
    }

    public void DiscardCard(Card card)
    {
        discard.Add(card.CardType);
        hand.DestroyBoardItem(card);
    }

    public void TryPlayCard(Card card, Creature targetCreature)
    {
        if (!MyTurn) return;
        if (!card) return;
        Card_SO cardType = card.CardType;
        if (Mana < cardType.manaCost) return;

        Mana -= cardType.manaCost;
        DiscardCard(card);

        StartCoroutine(ResolveCardEffects(cardType, targetCreature));
    }  

    IEnumerator ResolveCardEffects(Card_SO cardType, Creature targetCreature)
    {
        yield return new WaitForSeconds(startDelay);

        foreach(CardEffect effect in cardType.cardEffects)
        {
            switch(effect.targetType)
            {
                case CardEffect.TargetType.Player: effect.ResolveEffect(this); break;
                case CardEffect.TargetType.Creature: effect.ResolveEffect(targetCreature); break;
                case CardEffect.TargetType.AllCreatures: effect.ResolveEffect(FindObjectsOfType<Creature>()); break;
            }
            yield return new WaitForSeconds(effectDelay);
        }

        FindObjectOfType<Enemy>().PostCardPlayed();
        yield return new WaitForSeconds(endDelay);

    }
}


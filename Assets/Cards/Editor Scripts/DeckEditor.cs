using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
[CustomEditor(typeof(Deck_SO))]
public class DeckEditor : Editor
{
    public Texture plus;
    public Texture minus;

    public override void OnInspectorGUI()
    {
        Deck_SO deck = (Deck_SO)target;

        GUILayout.Label($"Cards in deck: {deck.TotalCards}");
        GUILayout.Label($"Cards remaining: {deck.CardsRemaining}");

        if (GUILayout.Button("Add Card"))
        {
            deck.cards.Add(new Deck_SO.CardType());
        }

        for(int i = 0; i < deck.cards.Count; i++)
        {
            Deck_SO.CardType cardType = deck.cards[i];

            EditorGUILayout.BeginHorizontal();
            Card_SO newCardType = (Card_SO)EditorGUILayout.ObjectField(cardType.cardType, typeof(Card_SO), true);
            if (!deck.ContainsCard(newCardType)) cardType.cardType = newCardType;

            if (GUILayout.Button(minus) && cardType.count > 0) cardType.count--;
            GUILayout.Label(cardType.count.ToString());
            if (GUILayout.Button(plus) && deck.CardsRemaining >= 1)
            {
                cardType.count = Mathf.Clamp(cardType.count + 1, 0, Deck_SO.MAX_CARD_COUNT);
            }

            if (GUILayout.Button("Remove Card")) deck.cards.RemoveAt(i--);
            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(deck);
    }
}
*/
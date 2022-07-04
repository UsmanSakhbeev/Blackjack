using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Deck : MonoBehaviour
{
    private List<Card> cardsList = new();
    public Sprite[] cards;

    void Start()
    {
        PrepareDeck();
    }

    public void PrepareDeck()
    {
        CreateDeck();
        ShuffleDeck();
    }
    
    private void CreateDeck()
    {

        for (int i = 0; i < cards.Length; i++)
        {
            var currentNumber = i % 13 + 1;

            if (currentNumber > 10)
            {
                currentNumber = 10;
            }
            if(currentNumber == 1)
            {
                currentNumber = 11;
            }
            cardsList.Add(new Card(cards[i], currentNumber));
        }
    }

    public Card GetCard()
    {
        if(cardsList.Count == 0)
        {
            CreateDeck();
            ShuffleDeck();
        }
        var card = cardsList[cardsList.Count - 1];
        cardsList.Remove(card);
        return card;
    }

    private void ShuffleDeck()
    {
        int deckCount = cardsList.Count;
        for (int i = 0; i < 100; i++)
        {
            var rnd = new System.Random();
            int id = rnd.Next(0, deckCount - 1);

            var tmp = cardsList[id];
            if(cardsList.Remove(tmp))
            {
                cardsList.Add(tmp);
            }
        }
    }
    public class Card
    {
        public readonly Sprite sprite;
        public readonly int value;

        public Card(Sprite sprite, int value)
        {
            this.sprite = sprite;
            this.value = value;
        }
    }
}





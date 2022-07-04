using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public GameObject cardPrefab;
    public DeckScript deckScript;

    public Text playerScore;
    public Text dealerScore;
    public Text betText;
    public Text kreditText;

    private int _playerCardCount;
    private int _dealerCardCount;
    private int _playerScore;
    private int _dealerScore;
    private int _bet;
    private int _kredit;

    private List<GameObject> _cardsOnDeck = new();

    private void Start()
    {
        _playerScore = 0;
        _bet = 0;
        _kredit = 500;
    }

    private void Update()
    {
        UpdateUI();
    }

    public void StartNewRound()
    {
        if (_bet == 0)
        {
            _bet = 10;
            _kredit -= 10;
        }

        PlayerGetsCard();
        PlayerGetsCard();
        DealerGetsCard();
    }
    public void PlayerGetsCard()
    {
        var card = deckScript.GetCard();

        _playerScore += card.value;
        _playerCardCount++;

        ShowPlayerCard(card);
    }

    public void ShowPlayerCard(DeckScript.Card card)
    {
        cardPrefab.GetComponent<SpriteRenderer>().sprite = card.sprite;
        var newCard = Instantiate(cardPrefab, new Vector3(-3.75f + _playerCardCount * 1.6f, -2, 0), Quaternion.identity);
        _cardsOnDeck.Add(newCard);
    }

    public void DealerGetsCard()
    {
        var card = deckScript.GetCard();

        _dealerScore += card.value;
        _dealerCardCount++;

        ShowDealerCard(card);
    }
    public void ShowDealerCard(DeckScript.Card card)
    {
        cardPrefab.GetComponent<SpriteRenderer>().sprite = card.sprite;
        var newCard = Instantiate(cardPrefab, new Vector3(-3.75f + _dealerCardCount * 1.6f, 1.5f, 0), Quaternion.identity);
        _cardsOnDeck.Add(newCard);
    }

    public void FillDealerHand()
    {
        if (_dealerScore < 17)
        {
            DealerGetsCard();
            FillDealerHand();
        }
    }

    public void PlaceBet(int value)
    {
        if (value <= _kredit)
        {
            _bet += value;
            _kredit -= value;
        }
    }

    public void DoubleBet()
    {
        if (_bet <= _kredit)
        {
            _kredit -= _bet;
            _bet += _bet;
        }
    }

    public void UpdateUI()
    {
        dealerScore.text = _dealerScore.ToString();
        playerScore.text = _playerScore.ToString();
        betText.text = "Bet: " + _bet.ToString();
        kreditText.text = "Kredit: " + _kredit.ToString();
    }

    public void DetermineTheWinner()
    {
        if (_playerScore == 21)
            _kredit += Convert.ToInt32(_bet * 1.5);
        else if (_playerScore == _dealerScore)
            _kredit += _bet;        
        if (_playerScore > _dealerScore && _playerScore < 21)
            _kredit += _bet * 2;

        _bet = 0;
    }

    public void PrepareTheTable()
    {
        foreach (var item in _cardsOnDeck)
        {
            Destroy(item);
        }

        _cardsOnDeck.Clear();
        _playerScore = 0;
        _dealerScore = 0;

        _playerCardCount = 0;
        _dealerCardCount = 0;
    }
}

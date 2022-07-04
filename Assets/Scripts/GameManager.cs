using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button dealButton;
    public Button standButton;
    public Button hitButton;
    public Button doubleButton;
    public Button redChipButton;
    public Button blueChipButton;
    public Button greenChipButton;
    public Button yellowChipButton;

    public DeckScript deckScript;
    public Game game;

    private bool _isRoundEnded;

    void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        standButton.onClick.AddListener(() => StandClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        doubleButton.onClick.AddListener(() => DoubleClicked());

        redChipButton.onClick.AddListener(() => RedChipClicked());
        blueChipButton.onClick.AddListener(() => BlueChipClicked());
        greenChipButton.onClick.AddListener(() => GreenChipClicked());
        yellowChipButton.onClick.AddListener(() => YellowChipClicked());

        standButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        doubleButton.gameObject.SetActive(false);

        _isRoundEnded = false;
    }

    private void Update()
    {
        if (_isRoundEnded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                game.PrepareTheTable();
                _isRoundEnded = false;
            }
        }
    }

    private void NewRound()
    {
        standButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        doubleButton.gameObject.SetActive(false);
        dealButton.gameObject.SetActive(true);

        redChipButton.gameObject.SetActive(true);
        blueChipButton.gameObject.SetActive(true);
        greenChipButton.gameObject.SetActive(true);
        yellowChipButton.gameObject.SetActive(true);        
    }

    private void Round()
    {
        standButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(true);
        doubleButton.gameObject.SetActive(true);
        dealButton.gameObject.SetActive(false);

        redChipButton.gameObject.SetActive(false);
        blueChipButton.gameObject.SetActive(false);
        greenChipButton.gameObject.SetActive(false);
        yellowChipButton.gameObject.SetActive(false);
    }

    private void DealClicked()
    {
        Round();

        game.StartNewRound();
    }
    private void StandClicked()
    {
        game.FillDealerHand();
        game.DetermineTheWinner();

        _isRoundEnded = true;
        NewRound();
    }
    private void HitClicked()
    {
        game.PlayerGetsCard();
    }
    private void DoubleClicked()
    {
        game.PlayerGetsCard();
        game.DoubleBet();
        game.FillDealerHand();
        game.DetermineTheWinner();

        _isRoundEnded = true;
        NewRound();
    }

    private void RedChipClicked()
    {
        game.PlaceBet(10);
    }
    private void BlueChipClicked()
    {
        game.PlaceBet(50);
    }
    private void GreenChipClicked()
    {
        game.PlaceBet(100);
    }
    private void YellowChipClicked()
    {
        game.PlaceBet(500);
    }
}

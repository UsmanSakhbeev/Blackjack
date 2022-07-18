using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button dealButton;
    [SerializeField] private Button standButton;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button doubleButton;
    [SerializeField] private Button redChipButton;
    [SerializeField] private Button blueChipButton;
    [SerializeField] private Button greenChipButton;
    [SerializeField] private Button yellowChipButton;
    [SerializeField] private Button newGameButton;

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

        newGameButton.onClick.AddListener(() => NewGameClicked());

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

    private void StartNewRound()
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
        StartNewRound();
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
        StartNewRound(); 
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

    public void NewGameClicked()
    {
        game.NewGame();        
    }
}

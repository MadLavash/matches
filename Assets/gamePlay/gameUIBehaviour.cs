using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class gameUIBehaviour : MonoBehaviour {

    public GameObject playerPanel;
    public GameObject comPanel;

    private Text txtMovesCount;
    private Text txtWhoseMove;
    private Button btnAccept;
    private Text txtWin;
    private Text txtProbability;

    private const string MOVES_COUNT_STRING = "выберите от {0} до {1} спичек";
    private const string WHOSE_MOVE_PLAYER = "ход игрока";
    private const string WHOSE_MOVE_COMPUTER = "ход компьютера";
    private const string COMPUTER_PROBABILITY = "вероятность победы компьютера: {0}";
    private const string PLAYER_WIN = "вы победили";
    private const string PLAYER_LOOSE = "вы проиграли";

    void Awake()
    {
        txtWhoseMove = playerPanel.GetComponent<RectTransform>().Find("txtWhoseMove").GetComponent<Text>();
        txtMovesCount = playerPanel.GetComponent<RectTransform>().Find("txtMovesCount").GetComponent<Text>();
        btnAccept = playerPanel.GetComponent<RectTransform>().Find("btnAccept").GetComponent<Button>();
        txtProbability = comPanel.GetComponent<RectTransform>().Find("txtProbability").GetComponent<Text>();
        txtWin = GameObject.Find("txtWin").GetComponent<Text>();
    }

    public void SetMovesCount(int matchesTakenLastMove)
    {
        txtMovesCount.text = String.Format(MOVES_COUNT_STRING, matchesTakenLastMove - 1 >= 1 ? matchesTakenLastMove - 1 : 1, matchesTakenLastMove + 1);
    }

    public void SetWhoseMove(bool isPlayerMoveNow)
    {
        if (isPlayerMoveNow)
        {
            txtWhoseMove.text = WHOSE_MOVE_PLAYER;
        }
        else
        {
            txtWhoseMove.text = WHOSE_MOVE_COMPUTER;
        }
    }

    public void SetButtonInteractible (bool isInteractible)
    {
        btnAccept.interactable = isInteractible;
    }

    public void SetComputerProbability (float computerWincChance)
    {
        txtProbability.text = String.Format(COMPUTER_PROBABILITY, (int)(computerWincChance /* 100*/));
    }

    public void ShowWinText (bool isPlayerMove)
    {
        if (isPlayerMove)
        {
            txtWin.text = PLAYER_WIN;
        }
        else
        {
            txtWin.text = PLAYER_LOOSE;
        }

        txtWin.enabled = true;
        btnAccept.enabled = false;
    }

}

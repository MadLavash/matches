using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class controlBehaviour : MonoBehaviour
{
    private const int MAX_MATCHES_SPAWN = 45;
    private const int MIN_MATCHES_SPAWN = 20;

    private levelBehaviour levelScript;
    private int matchesCount;
    private int matchesSelected;
    private int matchesTakenLastMove;
    private bool isPlayerMove;
    private gameUIBehaviour gameUIScript;
    private aiBehaviour aiScript;
    private int variationsCounter;

    void Awake()
    {
        levelScript = Camera.main.GetComponent<levelBehaviour>();
        gameUIScript = Camera.main.GetComponent<gameUIBehaviour>();
        aiScript = Camera.main.GetComponent<aiBehaviour>();
        matchesCount = UnityEngine.Random.Range(MIN_MATCHES_SPAWN, MAX_MATCHES_SPAWN + 1);
        matchesSelected = 0;
        matchesTakenLastMove = 2;
        isPlayerMove = true;
    }

    void Start()
    {
        levelScript.CreateGamefield(matchesCount);
        gameUIScript.SetMovesCount(matchesTakenLastMove);
    }

    public bool CanPlayerSelectMatch()
    {
        if ((matchesSelected + 1 <= matchesTakenLastMove + 1) && isPlayerMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CanPlayerFinishRound()
    {
        if (matchesSelected >= matchesTakenLastMove - 1 && matchesSelected >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayerHasSelectedMatch(object sender, EventArgs e)
    {
        matchesSelected++;

        if (matchesSelected >= matchesTakenLastMove - 1 && matchesSelected >= 1 && isPlayerMove)
        {
            gameUIScript.SetButtonInteractible(true);
        }
    }

    public void PlayerHasDeselectedMatch(object sender, EventArgs e)
    {
        matchesSelected--;

        if (matchesSelected < matchesTakenLastMove - 1 || matchesSelected < 1)
        {
            gameUIScript.SetButtonInteractible(false);
        }
    }

    public void MoveIsMade()
    {
        if (CanPlayerFinishRound())
        {
            levelScript.DeactivateSelectedMatches();

            matchesTakenLastMove = matchesSelected;
            matchesCount -= matchesSelected;
            matchesSelected = 0;

            if (CheckWinCondition())
            {
                gameUIScript.ShowWinText(isPlayerMove);
                return;
            }

            if (isPlayerMove)
            {
                isPlayerMove = false;
                aiScript.aiMakeDecision(matchesCount, matchesTakenLastMove);
            }
            else
            {
                isPlayerMove = true;
            }

            UpdateUI();
        }
        else
        {
            print("Warning: unexpected move");
        }

    }

    public void UpdateUI()
    {
        gameUIScript.SetMovesCount(matchesTakenLastMove);
        gameUIScript.SetWhoseMove(isPlayerMove);
        gameUIScript.SetButtonInteractible(false);
    }

    bool CheckWinCondition()
    {
        if (matchesCount == 0)
        {
            return true;
        }
        else if (matchesCount - matchesTakenLastMove - 1 < 0)
        {
            isPlayerMove = !isPlayerMove;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RestertLevel()
    {
        SceneManager.LoadScene("GamePlay");
    }
}



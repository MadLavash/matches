using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System;

public class aiBehaviour : MonoBehaviour
{

    private const float MAX_BRANCHES_COUNT = 5e6f;
    private const float MAX_CHILDRENS_COUNT = 3;

    private bool isUsingTreeMethod;
    private branchBehaviour movesTree;
    private levelBehaviour levelScript;
    private gameUIBehaviour gameUIScript;

    Stopwatch millisecondsTimer = new Stopwatch();

    void Awake()
    {
        isUsingTreeMethod = false;
        levelScript = Camera.main.GetComponent<levelBehaviour>();
        gameUIScript = Camera.main.GetComponent<gameUIBehaviour>();
    }

    public void aiMakeDecision(int matchesCount, int matchesTakenLastMove)
    {
        if (isUsingTreeMethod)
        {
            PlayerMadeMove(matchesTakenLastMove);
            ChooseBestMove();
        }
        else
        {
            if (CalculateBranchCount(matchesCount, matchesTakenLastMove) <= MAX_BRANCHES_COUNT)
            {
                isUsingTreeMethod = true;
                CreateMovesTree(matchesCount, matchesTakenLastMove);
                ChooseBestMove();
            }
            else
            {
                UsualMoveLogic(matchesCount, matchesTakenLastMove);
            }
        }
    }

    float CalculateBranchCount(int matchesCount, int matchesTakenLastMove)
    {
        return Mathf.Pow(MAX_CHILDRENS_COUNT, (float)(matchesCount / 2)) * (-0.246f * matchesTakenLastMove > 6 ? 6 : matchesTakenLastMove + 1.25f);
    }

    void CreateMovesTree(int matchesCount, int matchesTakenLastMove)
    {
        millisecondsTimer.Start();
        movesTree = new branchBehaviour(matchesCount, matchesTakenLastMove, true);
        millisecondsTimer.Stop();
        print("Create tree time: " + millisecondsTimer.ElapsedMilliseconds);

        millisecondsTimer.Reset();
        millisecondsTimer.Start();
        movesTree.CalculateProbability();
        millisecondsTimer.Stop();
        print("Calculating probability time: " + millisecondsTimer.ElapsedMilliseconds);
    }

    void ChooseBestMove()
    {
        int indexOfHighestValue = Array.IndexOf(movesTree.chancesOfChildren, movesTree.chancesOfChildren.Max());
        gameUIScript.SetComputerProbability(movesTree.chancesOfChildren[indexOfHighestValue]);

        switch (indexOfHighestValue)
        {
            case 0:
                movesTree = movesTree.leftBranch;
                levelScript.ChoosingForAI(movesTree.takenMatchesCount);
                break;
            case 1:
                movesTree = movesTree.centerBranch;
                levelScript.ChoosingForAI(movesTree.takenMatchesCount);
                break;
            case 2:
                movesTree = movesTree.rightBranch;
                levelScript.ChoosingForAI(movesTree.takenMatchesCount);
                break;
            default:
                print("Error in choosing best move");
                break;
        }
        
    }

    void UsualMoveLogic(int matchesCount, int matchesTakenLastMove)
    {
        if (matchesCount / matchesTakenLastMove % 2 == 0)
        {
            levelScript.ChoosingForAI(matchesTakenLastMove);
        }
        else
        {
            levelScript.ChoosingForAI(matchesTakenLastMove + 1);
        }
    }

    void PlayerMadeMove(int matchesTakenLastMove)
    {
        if (movesTree.leftBranch != null && matchesTakenLastMove == movesTree.leftBranch.takenMatchesCount)
        {
            movesTree = movesTree.leftBranch;
        }
        else if (matchesTakenLastMove == movesTree.centerBranch.takenMatchesCount)
        {
            movesTree = movesTree.centerBranch;
        }
        else if (matchesTakenLastMove == movesTree.rightBranch.takenMatchesCount)
        {
            movesTree = movesTree.rightBranch;
        }
        else
        {
            print("Error in player move (AI message)");
        }

        gameUIScript.SetComputerProbability(movesTree.chancesOfChildren.Max());
    }


}

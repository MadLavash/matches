using UnityEngine;
using System.Collections;

public class branchBehaviour 
{
    public int matchesCount { get; set; }
    public int takenMatchesCount { get; set; }
    public float computerWinChance { get; set; }
    public float[] chancesOfChildren { get; set; }

    public branchBehaviour leftBranch { get; set; }
    public branchBehaviour centerBranch { get; set; }
    public branchBehaviour rightBranch { get; set; }

    private bool isComputerMove;

    public branchBehaviour(int matchesCount, int takenMatchesCount, bool wasHumanMove)
    {
        this.matchesCount = matchesCount;
        this.takenMatchesCount = takenMatchesCount;
        this.isComputerMove = !wasHumanMove;

        if (matchesCount > 0)
        {
            if (takenMatchesCount - 1 > 0)
            {
                leftBranch = new branchBehaviour((matchesCount - takenMatchesCount + 1), (takenMatchesCount - 1), isComputerMove);
            }
            else
            {
                leftBranch = null;
            }
            centerBranch = new branchBehaviour(matchesCount - takenMatchesCount, takenMatchesCount, isComputerMove);
            rightBranch = new branchBehaviour((matchesCount - takenMatchesCount - 1), (takenMatchesCount + 1), isComputerMove);
        }
    }

    public float CalculateProbability()
    {
        if (matchesCount > 0)
        {
            chancesOfChildren = new float[3];

            if (leftBranch == null)
            {
                chancesOfChildren[0] = 0;
                chancesOfChildren[1] = centerBranch.CalculateProbability();
                chancesOfChildren[2] = rightBranch.CalculateProbability();

                computerWinChance = (chancesOfChildren[1] + chancesOfChildren[2]) / 2f;
            }
            else
            {
                chancesOfChildren[0] = leftBranch.CalculateProbability();
                chancesOfChildren[1] = centerBranch.CalculateProbability();
                chancesOfChildren[2] = rightBranch.CalculateProbability();

                computerWinChance = (chancesOfChildren[1] + chancesOfChildren[2] + chancesOfChildren[0]) / 3f;
            }

        }
        else
        {
            computerWinChance = isComputerMove ? 100f : 0f;
        }

        return computerWinChance;
    }

}

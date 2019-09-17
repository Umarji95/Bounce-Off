using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternDetector : MonoBehaviour
{
    public List<GridIndex> activeElements = new List<GridIndex>();

    private List<GridIndex> resultGridIndices = new List<GridIndex>();

    public void CheckForPattern(PATTERN_TYPE currentPattern)
    {
        switch(currentPattern)
        {
            case PATTERN_TYPE.SQUARE_FOUR_DOTS:

                //Clearing any presvious result data
                if (resultGridIndices.Count > 0)
                    resultGridIndices.Clear();

                SquarePattern squarePattern = new SquarePattern();
                resultGridIndices = squarePattern.SquarePatternAlgorithm(activeElements);  //Getting the final indices where the pattern has formed
                DebugGridResultMessage(resultGridIndices, "Sqaure is formed at : ");

                break;

            case PATTERN_TYPE.T_FIVE_DOTS:

                //Clearing any presvious result data
                if (resultGridIndices.Count > 0)
                    resultGridIndices.Clear();

                T_FIVE_Pattern t_FIVE = new T_FIVE_Pattern();
                resultGridIndices = t_FIVE.T_FivePatternAlgorithm(activeElements);
                DebugGridResultMessage(resultGridIndices, "T with 5 points is formed at :");

                break;
        }
    }

    public void ResetActiveElementsList()
    {
        //activeElements.Clear();
    }

    public void DebugGridResultMessage(List<GridIndex> formationList , string message = "")
    {
        for(int i = 0; i < formationList.Count; i++)
        {
            message += "(" + formationList[i].X + "," + formationList[i].Y + ") ";  
        }

        Debug.Log(message);
    }
}

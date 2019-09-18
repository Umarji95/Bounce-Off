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
        //Clearing any previous result data
        if (resultGridIndices.Count > 0)
            resultGridIndices.Clear();

        switch (currentPattern)
        {
            case PATTERN_TYPE.SQUARE_FOUR_DOTS:

                SquarePattern squarePattern = new SquarePattern();
                resultGridIndices = squarePattern.SquarePatternAlgorithm(activeElements);  //Getting the final indices where the pattern has formed
                DebugGridResultMessage(resultGridIndices, "Sqaure is formed at : ");

                break;

            case PATTERN_TYPE.T_FOUR_DOTS:

                T_FOUR_Pattern t_FOUR = new T_FOUR_Pattern();
                resultGridIndices = t_FOUR.T_FourPatternAlgorithm(activeElements);
                DebugGridResultMessage(resultGridIndices, "T with 4 points is formed at :");

                break;

            case PATTERN_TYPE.T_FIVE_DOTS:

                T_FIVE_Pattern t_FIVE = new T_FIVE_Pattern();
                resultGridIndices = t_FIVE.T_FivePatternAlgorithm(activeElements);
                DebugGridResultMessage(resultGridIndices, "T with 5 points is formed at :");

                break;

            case PATTERN_TYPE.PLUS_FIVE_DOTS:

                PlusPattern plusPattern = new PlusPattern();
                resultGridIndices = plusPattern.PlusPatternAlgorithm(activeElements);
                DebugGridResultMessage(resultGridIndices, "Plus is formed at : ");

                break;

            case PATTERN_TYPE.THREE_DOTS:

                ThreeDotsPattern threeDotsPattern = new ThreeDotsPattern();
                resultGridIndices = threeDotsPattern.ThreeDotsAlgorithm(activeElements);
                DebugGridResultMessage(resultGridIndices, "Three Dots is formed at : ");

                break;

            case PATTERN_TYPE.FOUR_DOTS:

                FourDotsPattern fourDotsPattern = new FourDotsPattern();
                resultGridIndices = fourDotsPattern.FourDotsAlgorithm(activeElements);
                DebugGridResultMessage(resultGridIndices, "Four Dots is formed at : ");

                break;
        }
    }

    public void ResetActiveElementsList()
    {
        activeElements.Clear();
    }

    public void DebugGridResultMessage(List<GridIndex> formationList , string message = "")
    {
        if (formationList.Count <= 0)
            return;

        for(int i = 0; i < formationList.Count; i++)
        {
            message += "(" + formationList[i].X + "," + formationList[i].Y + ") ";  
        }

        Debug.Log(message);
    }
}

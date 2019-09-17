using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePatternController 
{
    protected GridIndex topElement = null, bottomElement = null, leftElement = null, rightElement = null;

    //Stores the final list of grid indices repective pattern formed at
    protected List<GridIndex> finalPatternIndices = new List<GridIndex>();

    /// <summary>
    /// Gets all the neighbouring elements to currently passesed Element (Top/Bottom/Left/Right)
    /// </summary>
    /// <param name="currentActiveRow"></param>
    /// <param name="currentActiveCol"></param>
    protected void GetNeighbourElements(int currentActiveRow , int currentActiveCol)
    {
        topElement = bottomElement = leftElement = rightElement= null; 

        if ((currentActiveRow - 1) >= GridManager.instance.minElements)
            topElement = GridManager.instance.elementsList.Find(obj => obj.X == currentActiveRow - 1 && obj.Y == currentActiveCol);

        if ((currentActiveRow + 1) < GridManager.instance.maxElements)
            bottomElement = GridManager.instance.elementsList.Find(obj => obj.X == currentActiveRow + 1 && obj.Y == currentActiveCol);

        if ((currentActiveCol - 1) >= GridManager.instance.minElements)
            leftElement = GridManager.instance.elementsList.Find(obj => obj.X == currentActiveRow && obj.Y == currentActiveCol - 1);

        if ((currentActiveCol + 1) < GridManager.instance.maxElements)
            rightElement = GridManager.instance.elementsList.Find(obj => obj.X == currentActiveRow && obj.Y == currentActiveCol + 1);

        //Below steps are to check if the neighburing elements are active elements
        #region Checking if elements are active
        if (topElement != null && !topElement.isActive)
            topElement = null;

        if (bottomElement != null && !bottomElement.isActive)
            bottomElement = null;

        if (leftElement != null && !leftElement.isActive)
            leftElement = null;

        if (rightElement != null && !rightElement.isActive)
            rightElement = null;
        #endregion
    }
}

public class SquarePattern : BasePatternController
{

    /// <summary>
    /// Returns all the Indices where the square has formed
    /// </summary>
    /// <param name="activeElements">currently active elements in the grid </param>
    /// <returns></returns>
    public List<GridIndex> SquarePatternAlgorithm(List<GridIndex> activeElements)
    {
        //Clearing the previous stored indices before adding any new data
        finalPatternIndices.Clear();

        for(int i = 0; i < activeElements.Count;i++)
        {
            int currentRow = activeElements[i].X;
            int currentCol = activeElements[i].Y;

            //Getting all the neighbors of the current element
            GetNeighbourElements(currentRow , currentCol);

            if (leftElement != null && bottomElement != null)
            {
                GridIndex bottomLeft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol - 1);

                if (bottomLeft.isActive)
                {
                    finalPatternIndices.Add(activeElements[i]);
                    finalPatternIndices.Add(leftElement);
                    finalPatternIndices.Add(bottomElement);
                    finalPatternIndices.Add(bottomLeft);

                    break;
                }
            }

            else if (leftElement != null && topElement != null)
            {
                GridIndex topleft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol - 1);

                if (topleft.isActive)
                {
                    finalPatternIndices.Add(activeElements[i]);
                    finalPatternIndices.Add(leftElement);
                    finalPatternIndices.Add(topElement);
                    finalPatternIndices.Add(topleft);

                    break;
                }
            }

            else if (rightElement != null && bottomElement != null)
            {
                GridIndex bottomRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol + 1);

                if (bottomRight.isActive)
                {
                    finalPatternIndices.Add(activeElements[i]);
                    finalPatternIndices.Add(rightElement);
                    finalPatternIndices.Add(bottomElement);
                    finalPatternIndices.Add(bottomRight);

                    break;
                }
            }

            else if (rightElement != null && topElement != null)
            {
                GridIndex topRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol + 1);

                if (topRight.isActive)
                {
                    finalPatternIndices.Add(activeElements[i]);
                    finalPatternIndices.Add(rightElement);
                    finalPatternIndices.Add(topElement);
                    finalPatternIndices.Add(topRight);

                    break;
                }
            }
        }

        return finalPatternIndices;
    }
}

public class T_FIVE_Pattern : BasePatternController
{

    /// <summary>
    /// Returns all the Indices where the "T SHAPE " with 5 points has formed
    /// </summary>
    /// <param name="activeElements">currently active elements in the grid </param>
    /// <returns></returns>
    public List<GridIndex> T_FivePatternAlgorithm(List<GridIndex> activeElements)
    {
        //Clearing the previous stored indices before adding any new data
        finalPatternIndices.Clear();

        for (int i = 0; i < activeElements.Count; i++)
        {
            int currentRow = activeElements[i].X;
            int currentCol = activeElements[i].Y;

            //Getting all the neighbors of the current element
            GetNeighbourElements(currentRow, currentCol);

            if (leftElement != null && rightElement != null && bottomElement != null)
            {
                GridIndex lowerBottom = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 2 && obj.Y == currentCol); //i.e bottom of the bottom element

                if (lowerBottom.isActive)
                {
                    finalPatternIndices.Add(leftElement);
                    finalPatternIndices.Add(activeElements[i]);
                    finalPatternIndices.Add(rightElement);
                    finalPatternIndices.Add(bottomElement);
                    finalPatternIndices.Add(lowerBottom);

                    break;
                }
            }

            else if (leftElement != null && rightElement != null && topElement != null)
            {
                GridIndex upperTop = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 2 && obj.Y == currentCol); //i.e top of the top element

                if (upperTop.isActive)
                {
                    finalPatternIndices.Add(leftElement);
                    finalPatternIndices.Add(activeElements[i]);
                    finalPatternIndices.Add(rightElement);
                    finalPatternIndices.Add(topElement);
                    finalPatternIndices.Add(upperTop);

                    break;
                }
            }

            else if (topElement != null && bottomElement != null && leftElement != null)
            {
                GridIndex besideLeft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow && obj.Y == currentCol - 2); //i.e left side of the left element

                if (besideLeft.isActive)
                {
                    finalPatternIndices.Add(topElement);
                    finalPatternIndices.Add(activeElements[i]);
                    finalPatternIndices.Add(bottomElement);
                    finalPatternIndices.Add(leftElement);
                    finalPatternIndices.Add(besideLeft);

                    break;
                }
            }

            else if (topElement != null && bottomElement != null && rightElement != null)
            {
                GridIndex besideRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow && obj.Y == currentCol + 2); //i.e left side of the left element

                if (besideRight.isActive)
                {
                    finalPatternIndices.Add(topElement);
                    finalPatternIndices.Add(activeElements[i]);
                    finalPatternIndices.Add(bottomElement);
                    finalPatternIndices.Add(rightElement);
                    finalPatternIndices.Add(besideRight);

                    break;
                }
            }
        }
        return finalPatternIndices;
    }
}

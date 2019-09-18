using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePatternController : PatternBase
{
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
        resultPatternIndices.Clear();

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
                    resultPatternIndices.Add(activeElements[i]);
                    resultPatternIndices.Add(leftElement);
                    resultPatternIndices.Add(bottomElement);
                    resultPatternIndices.Add(bottomLeft);

                    break;
                }
            }

            else if (leftElement != null && topElement != null)
            {
                GridIndex topleft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol - 1);

                if (topleft.isActive)
                {
                    resultPatternIndices.Add(activeElements[i]);
                    resultPatternIndices.Add(leftElement);
                    resultPatternIndices.Add(topElement);
                    resultPatternIndices.Add(topleft);

                    break;
                }
            }

            else if (rightElement != null && bottomElement != null)
            {
                GridIndex bottomRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol + 1);

                if (bottomRight.isActive)
                {
                    resultPatternIndices.Add(activeElements[i]);
                    resultPatternIndices.Add(rightElement);
                    resultPatternIndices.Add(bottomElement);
                    resultPatternIndices.Add(bottomRight);

                    break;
                }
            }

            else if (rightElement != null && topElement != null)
            {
                GridIndex topRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol + 1);

                if (topRight.isActive)
                {
                    resultPatternIndices.Add(activeElements[i]);
                    resultPatternIndices.Add(rightElement);
                    resultPatternIndices.Add(topElement);
                    resultPatternIndices.Add(topRight);

                    break;
                }
            }
        }
        return resultPatternIndices;
    }
}

public class T_FOUR_Pattern : BasePatternController
{
    /// <summary>
    /// Returns all the Indices where the "T SHAPE " with 5 points has formed
    /// </summary>
    /// <param name="activeElements">currently active elements in the grid </param>
    /// <returns></returns>
    public List<GridIndex> T_FourPatternAlgorithm(List<GridIndex> activeElements)
    {
        //Clearing the previous stored indices before adding any new data
        resultPatternIndices.Clear();

        //Resetting the value
        resultPatternDirection = PatternDirection.NONE;

        for (int i = 0; i < activeElements.Count; i++)
        {
            int currentRow = activeElements[i].X;
            int currentCol = activeElements[i].Y;

            //Getting all the neighbors of the current element
            GetNeighbourElements(currentRow, currentCol);

            if (leftElement != null && rightElement != null && bottomElement != null)
            {

                resultPatternIndices.Add(leftElement);
                resultPatternIndices.Add(activeElements[i]);
                resultPatternIndices.Add(rightElement);
                resultPatternIndices.Add(bottomElement);

                resultPatternDirection = PatternDirection.BOTTOM;
                break;
            }

            else if (leftElement != null && rightElement != null && topElement != null)
            {
                resultPatternIndices.Add(leftElement);
                resultPatternIndices.Add(activeElements[i]);
                resultPatternIndices.Add(rightElement);
                resultPatternIndices.Add(topElement);

                resultPatternDirection = PatternDirection.TOP;
                break;
            }

            else if (topElement != null && bottomElement != null && leftElement != null)
            {
                resultPatternIndices.Add(topElement);
                resultPatternIndices.Add(activeElements[i]);
                resultPatternIndices.Add(bottomElement);
                resultPatternIndices.Add(leftElement);

                resultPatternDirection = PatternDirection.LEFT;
                break;
            }

            else if (topElement != null && bottomElement != null && rightElement != null)
            {
                resultPatternIndices.Add(topElement);
                resultPatternIndices.Add(activeElements[i]);
                resultPatternIndices.Add(bottomElement);
                resultPatternIndices.Add(rightElement);

                resultPatternDirection = PatternDirection.RIGHT;
                break;
            }
        }
        return resultPatternIndices;
    }
}

public class T_FIVE_Pattern : T_FOUR_Pattern
{
    /// <summary>
    /// Returns all the Indices where the "T SHAPE " with 5 points has formed
    /// </summary>
    /// <param name="activeElements">currently active elements in the grid </param>
    /// <returns></returns>
    public List<GridIndex> T_FivePatternAlgorithm(List<GridIndex> activeElements)
    {
        //Finding the Four Dots T shape
        resultPatternIndices =  T_FourPatternAlgorithm(activeElements);

        int enteredListCount = resultPatternIndices.Count;

        switch(resultPatternDirection)
        {
            case PatternDirection.TOP:

                GridIndex upperTop = GridManager.instance.elementsList.Find(obj => obj.X == topElement.X - 1 && obj.Y == topElement.Y); //i.e top of the top element

                if(upperTop != null && upperTop.isActive)
                    resultPatternIndices.Add(upperTop);

                break;

            case PatternDirection.BOTTOM:

                GridIndex lowerBottom = GridManager.instance.elementsList.Find(obj => obj.X == bottomElement.X + 1 && obj.Y == bottomElement.Y); //i.e bottom of the bottom element

                if(lowerBottom != null && lowerBottom.isActive)
                    resultPatternIndices.Add(lowerBottom);

                break;

            case PatternDirection.LEFT:

                GridIndex besideLeft = GridManager.instance.elementsList.Find(obj => obj.X == leftElement.X && obj.Y == leftElement.Y - 1); //i.e left side of the left element

                if(besideLeft!= null && besideLeft.isActive)
                    resultPatternIndices.Add(besideLeft);

                break;

            case PatternDirection.RIGHT:

                GridIndex besideRight = GridManager.instance.elementsList.Find(obj => obj.X == rightElement.X && obj.Y == rightElement.Y + 1); //i.e left side of the left element

                if(besideRight !=null && besideRight.isActive)
                    resultPatternIndices.Add(besideRight);

                break;

        }

        //Clearing the list if no new element was added in to the list in above calculation , indicating match was not found
        if (resultPatternIndices.Count <= enteredListCount)
             resultPatternIndices.Clear();

        return resultPatternIndices;
    }
}

public class PlusPattern : BasePatternController
{
    /// <summary>
    /// Returns all the Indices where the "Plus SHAPE " has formed
    /// </summary>
    /// <param name="activeElements">currently active elements in the grid </param>
    /// <returns></returns>
    public List<GridIndex> PlusPatternAlgorithm(List<GridIndex> activeElements)
    {
        resultPatternIndices.Clear();

        for (int i = 0; i < activeElements.Count; i++)
        {
            int currentRow = activeElements[i].X;
            int currentCol = activeElements[i].Y;

            //Getting all the neighbors of the current element
            GetNeighbourElements(currentRow, currentCol);

            if(topElement != null && bottomElement != null && leftElement != null && rightElement != null)
            {
                resultPatternIndices.Add(activeElements[i]);
                resultPatternIndices.Add(leftElement);
                resultPatternIndices.Add(rightElement);
                resultPatternIndices.Add(topElement);
                resultPatternIndices.Add(bottomElement);

                break;
            }
        }
        return resultPatternIndices;
    }
}

public class ThreeDotsPattern : BasePatternController
{
    /// <summary>
    /// Returns all the Indices where the "Three dots SHAPE " has formed
    /// </summary>
    /// <param name="activeElements">currently active elements in the grid </param>
    /// <returns></returns>
    public List<GridIndex> ThreeDotsAlgorithm(List<GridIndex> activeElements)
    {
        resultPatternIndices.Clear();

        for (int i = 0; i < activeElements.Count; i++)
        {
            int currentRow = activeElements[i].X;
            int currentCol = activeElements[i].Y;

            //Getting all the neighbors of the current element
            GetNeighbourElements(currentRow, currentCol);

            if(leftElement != null && rightElement != null)
            {
                resultPatternIndices.Add(activeElements[i]);
                resultPatternIndices.Add(leftElement);
                resultPatternIndices.Add(rightElement);

                resultPatternDirection = PatternDirection.HORIZONTAL;
                break;
            }

            else if(topElement != null && bottomElement != null)
            {
                resultPatternIndices.Add(activeElements[i]);
                resultPatternIndices.Add(topElement);
                resultPatternIndices.Add(bottomElement);

                resultPatternDirection = PatternDirection.VERTICAL;
                break;
            }
        }
        return resultPatternIndices; 
    }
}


public class FourDotsPattern : BasePatternController
{
    /// <summary>
    /// Returns all the Indices where the "Four dots SHAPE " has formed
    /// </summary>
    /// <param name="activeElements">currently active elements in the grid </param>
    /// <returns></returns>
    public List<GridIndex> FourDotsAlgorithm(List<GridIndex> activeElements)
    {

        resultPatternIndices.Clear();

        for (int i = 0; i < activeElements.Count; i++)
        {
            int currentRow = activeElements[i].X;
            int currentCol = activeElements[i].Y;

            //Getting all the neighbors of the current element
            GetNeighbourElements(currentRow, currentCol);

            if (leftElement != null && rightElement != null)
            {
                GridIndex besideLeft = GridManager.instance.elementsList.Find(obj => obj.X == leftElement.X && obj.Y == leftElement.Y - 1); //i.e left side of the left element
                GridIndex besideRight = GridManager.instance.elementsList.Find(obj => obj.X == rightElement.X && obj.Y == rightElement.Y + 1); //i.e left side of the left element

                if(besideLeft != null && besideLeft.isActive)
                {
                    resultPatternIndices.Add(besideLeft);
                    resultPatternIndices.Add(leftElement);
                    resultPatternIndices.Add(activeElements[i]);
                    resultPatternIndices.Add(rightElement);

                    break;
                }

                else if(besideRight != null && besideRight.isActive)
                {
                    resultPatternIndices.Add(leftElement);
                    resultPatternIndices.Add(activeElements[i]);
                    resultPatternIndices.Add(rightElement);
                    resultPatternIndices.Add(besideRight);

                    break;
                }
            }

            else if (topElement != null && bottomElement != null)
            {
                GridIndex upperTop = GridManager.instance.elementsList.Find(obj => obj.X == topElement.X - 1 && obj.Y == topElement.Y); //i.e top of the top element
                GridIndex lowerBottom = GridManager.instance.elementsList.Find(obj => obj.X == bottomElement.X + 1 && obj.Y == bottomElement.Y); //i.e bottom of the bottom element

                if (upperTop != null && upperTop.isActive)
                {
                    resultPatternIndices.Add(upperTop);
                    resultPatternIndices.Add(topElement);
                    resultPatternIndices.Add(activeElements[i]);
                    resultPatternIndices.Add(bottomElement);

                    break;
                }

                else if (lowerBottom != null && lowerBottom.isActive)
                {
                    resultPatternIndices.Add(topElement);
                    resultPatternIndices.Add(activeElements[i]);
                    resultPatternIndices.Add(bottomElement);
                    resultPatternIndices.Add(lowerBottom);

                    break;
                }
            }
        }
        return resultPatternIndices;
    }
}

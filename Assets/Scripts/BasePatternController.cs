using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePatternController 
{

    protected GridIndex topElement = null, bottomElement = null, leftElement = null, rightElement = null;

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

        #region Checking if elements are active
        if (topElement != null && !topElement.isClicked)
        {
            topElement = null;
        }

        if (bottomElement != null && !bottomElement.isClicked)
        {
            bottomElement = null;
        }

        if (leftElement != null && !leftElement.isClicked)
        {
            leftElement = null;
        }

        if (rightElement != null && !rightElement.isClicked)
        {
            rightElement = null;
        }
            #endregion
    }
}

public class SquarePattern : BasePatternController
{
    //Always the GridIndes is stored in following way 0 = Top , 1 = Bottom , 2 = Left , 3 = Bottom
    private GridIndex[] gridIndices = null;

    /// <summary>
    /// Retuns all the Indices where the square is formed
    /// </summary>
    public void SquarePatternAlgorithm(List<GridIndex> activeElements)
    {
        for(int i = 0; i < activeElements.Count;i++)
        {
            int currentRow = activeElements[i].X;
            int currentCol = activeElements[i].Y;

            //Getting all the neighbors of the current element
            GetNeighbourElements(currentRow , currentCol);

            if (leftElement != null && bottomElement != null)
            {
                GridIndex bottomLeft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol - 1);

                if (bottomLeft.isClicked)
                {
                    Debug.Log("Squared Formed at : " + currentRow + ":" + currentCol + "--" + leftElement.X + ":" + leftElement.Y + "--" +
                                    bottomElement.X + ":" + bottomElement.Y + "--" + bottomLeft.X + ":" + bottomLeft.Y);
                    GridManager.instance.text.SetActive(true);
                    break;
                }
            }

            else if (leftElement != null && topElement != null)
            {
                GridIndex topleft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol - 1);

                if (topleft.isClicked)
                {
                    Debug.Log("Squared Formed at : " + currentRow + ":" + currentCol + "--" + leftElement.X + ":" + leftElement.Y + "--" +
                                topElement.X + ":" + topElement.Y + "--" + topleft.X + ":" + topleft.Y);
                    GridManager.instance.text.SetActive(true);
                    break;
                }
            }

            else if (rightElement != null && bottomElement != null)
            {
                GridIndex bottomRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol + 1);

                if (bottomRight.isClicked)
                {
                    Debug.Log("Squared Formed at : " + currentRow + ":" + currentCol + "--" + rightElement.X + ":" + rightElement.Y + "--" +
                                 bottomElement.X + ":" + bottomElement.Y + "--" + bottomRight.X + ":" + bottomRight.Y);
                    GridManager.instance.text.SetActive(true);
                    break;
                }
            }

            else if (rightElement != null && topElement != null)
            {
                GridIndex topRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol + 1);

                if (topRight.isClicked)
                {
                    Debug.Log("Squared Formed at : " + currentRow + ":" + currentCol + "--" + rightElement.X + ":" + rightElement.Y + "--" +
                                 topElement.X + ":" + topElement.Y + "--" + topRight.X + ":" + topRight.Y);
                    GridManager.instance.text.SetActive(true);
                    break;
                }
            }
        }
    }
}

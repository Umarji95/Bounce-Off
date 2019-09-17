using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternDetector : MonoBehaviour
{
    AllEnums.PATTERN_TYPE currentPattern = AllEnums.PATTERN_TYPE.Square;

    [HideInInspector]
    public List<GridIndex> activeElements = new List<GridIndex>();

    public void CheckForPattern()
    {
        switch(currentPattern)
        {
            case AllEnums.PATTERN_TYPE.Square:

                for(int i = 0; i < activeElements.Count; i ++)
                {
                    int currentRow = activeElements[i].X;
                    int currentCol = activeElements[i].Y;

                    GridIndex topElement = null , bottomElement = null, leftElement = null, rightElement = null;

                    #region Getting Neighbours
                    if ((currentRow - 1) > GridManager.instance.minElements)
                        topElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol);

                    if((currentRow + 1) < GridManager.instance.maxElements)
                        bottomElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol);

                    if ((currentCol - 1) > GridManager.instance.minElements)
                        leftElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow && obj.Y == currentCol - 1);

                    if ((currentCol + 1) < GridManager.instance.maxElements)
                        rightElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow  && obj.Y == currentCol + 1);
                    #endregion

                    #region Checking if neighbors are active
                    if (topElement == null || !topElement.isClicked)
                        topElement = null;

                    if (bottomElement == null || !bottomElement.isClicked)
                        bottomElement = null;

                    if (leftElement == null || !leftElement.isClicked)
                        leftElement = null;

                    if (rightElement == null || !rightElement.isClicked)
                        rightElement = null;
                    #endregion

                    if (leftElement != null && bottomElement != null)
                    {
                        GridIndex bottomLeft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol - 1);

                        if (bottomLeft.isClicked)
                            Debug.Log("Squared Formed at : " + currentRow + ":" + currentCol +"--"+ leftElement.X +":"+ leftElement.Y + "--" + 
                                            bottomElement.X + ":" + bottomElement.Y + "--" + bottomLeft.X + ":" + bottomLeft.Y);
                        break;
                    }

                    else if (leftElement != null && topElement != null)
                    {
                        GridIndex topleft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol - 1);

                        if (topleft.isClicked)
                            Debug.Log("Squared Formed at : " + currentRow + ":" + currentCol + "--" + leftElement.X + ":" + leftElement.Y + "--" +
                                        topElement.X + ":" + topElement.Y + "--" + topleft.X + ":" + topleft.Y);
                        break;
                    }

                    else if (rightElement != null && bottomElement != null)
                    {
                        GridIndex bottomRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol + 1);

                        if (bottomRight.isClicked)
                            Debug.Log("Squared Formed at : " + currentRow + ":" + currentCol + "--" + rightElement.X + ":" + rightElement.Y + "--" +
                                         bottomElement.X + ":" + bottomElement.Y + "--" + bottomRight.X + ":" + bottomRight.Y);
                        break;
                    }

                    else if (rightElement != null && topElement != null)
                    {
                        GridIndex topRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol + 1);

                        if (topRight.isClicked)
                            Debug.Log("Squared Formed at : " + currentRow + ":" + currentCol + "--" + rightElement.X + ":" + rightElement.Y + "--" +
                                         topElement.X + ":" + topElement.Y + "--" + topRight.X + ":" + topRight.Y);
                        break;
                    }

                    //else
                    //    Debug.Log("Square not found");
                }

                break;
        }
    }
}

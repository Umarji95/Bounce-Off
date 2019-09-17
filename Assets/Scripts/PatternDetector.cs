using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternDetector : MonoBehaviour
{
    [HideInInspector]
    public List<GridIndex> activeElements = new List<GridIndex>();

    public void CheckForPattern(PATTERN_TYPE currentPattern)
    {
        switch(currentPattern)
        {
            case PATTERN_TYPE.SQUARE_FOUR_DOTS:
            #region Square

                for(int i = 0; i < activeElements.Count; i ++)
                {
                    int currentRow = activeElements[i].X;
                    int currentCol = activeElements[i].Y;

                    GridIndex topElement = null , bottomElement = null, leftElement = null, rightElement = null;

                    #region Getting Top , Bottom , Left and Right Elements
                    if ((currentRow - 1) >= GridManager.instance.minElements)
                        topElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol);

                    if((currentRow + 1) < GridManager.instance.maxElements)
                        bottomElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol);

                    if ((currentCol - 1) >= GridManager.instance.minElements)
                        leftElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow && obj.Y == currentCol - 1);

                    if ((currentCol + 1) < GridManager.instance.maxElements)
                        rightElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow  && obj.Y == currentCol + 1);
                    #endregion

                    #region Checking if elements are active
                    if (topElement != null && !topElement.isClicked)
                        topElement = null;

                    if (bottomElement != null && !bottomElement.isClicked)
                        bottomElement = null;

                    if (leftElement != null && !leftElement.isClicked)
                        leftElement = null;

                    if (rightElement != null && !rightElement.isClicked)
                        rightElement = null;
                    #endregion

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
            #endregion
                break;

            case PATTERN_TYPE.T_FIVE_DOTS:

                for (int i = 0; i < activeElements.Count; i++)
                {
                    int currentRow = activeElements[i].X;
                    int currentCol = activeElements[i].Y;

                    GridIndex topElement = null, bottomElement = null, leftElement = null, rightElement = null;

                    #region Getting Top , Bottom , Left and Right Elements
                    if ((currentRow - 1) >= GridManager.instance.minElements)
                        topElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 1 && obj.Y == currentCol);

                    if ((currentRow + 1) < GridManager.instance.maxElements)
                        bottomElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 1 && obj.Y == currentCol);

                    if ((currentCol - 1) >= GridManager.instance.minElements)
                        leftElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow && obj.Y == currentCol - 1);

                    if ((currentCol + 1) < GridManager.instance.maxElements)
                        rightElement = GridManager.instance.elementsList.Find(obj => obj.X == currentRow && obj.Y == currentCol + 1);
                    #endregion

                    #region Checking if elements are active
                    if (topElement != null && !topElement.isClicked)
                        topElement = null;

                    if (bottomElement != null && !bottomElement.isClicked)
                        bottomElement = null;

                    if (leftElement != null && !leftElement.isClicked)
                        leftElement = null;

                    if (rightElement != null && !rightElement.isClicked)
                        rightElement = null;
                    #endregion

                    if(leftElement != null && rightElement != null && bottomElement != null)
                    {
                        GridIndex lowerBottom = GridManager.instance.elementsList.Find(obj => obj.X == currentRow + 2 && obj.Y == currentCol); //i.e bottom of the bottom element

                        if (lowerBottom.isClicked)
                        {
                            Debug.Log("T Five dots Formed at : " + currentRow + ":" + currentCol + "--" + leftElement.X + ":" + leftElement.Y + "--" +
                                            "--" + rightElement.X + ":" + rightElement.Y +"--"+ bottomElement.X + ":" + bottomElement.Y + "--" + lowerBottom.X + ":" + lowerBottom.Y);
                            GridManager.instance.text.SetActive(true);
                            break;
                        }
                    }

                    else if(leftElement != null && rightElement != null && topElement != null)
                    {
                        GridIndex upperTop = GridManager.instance.elementsList.Find(obj => obj.X == currentRow - 2 && obj.Y == currentCol); //i.e top of the top element

                        if (upperTop.isClicked)
                        {
                            Debug.Log("T Five dots Formed at : " + currentRow + ":" + currentCol + "--" + leftElement.X + ":" + leftElement.Y + "--" +
                                            "--" + rightElement.X + ":" + rightElement.Y + "--" + topElement.X + ":" + topElement.Y + "--" + upperTop.X + ":" + upperTop.Y);
                            GridManager.instance.text.SetActive(true);
                            break;
                        }
                    }

                    else if (topElement != null && bottomElement != null && leftElement != null)
                    {
                        GridIndex besideLeft = GridManager.instance.elementsList.Find(obj => obj.X == currentRow && obj.Y == currentCol - 2); //i.e left side of the left element

                        if (besideLeft.isClicked)
                        {
                            Debug.Log("T Five dots Formed at : " + currentRow + ":" + currentCol + "--" + topElement.X + ":" + topElement.Y + "--" +
                                            "--" + bottomElement.X + ":" + bottomElement.Y + "--" + leftElement.X + ":" + leftElement.Y + "--" + besideLeft.X + ":" + besideLeft.Y);
                            GridManager.instance.text.SetActive(true);
                            break;
                        }
                    }

                    else if (topElement != null && bottomElement != null && rightElement != null)
                    {
                        GridIndex besideRight = GridManager.instance.elementsList.Find(obj => obj.X == currentRow && obj.Y == currentCol + 2); //i.e left side of the left element

                        if (besideRight.isClicked)
                        {
                            Debug.Log("T Five dots Formed at : " + currentRow + ":" + currentCol + "--" + topElement.X + ":" + topElement.Y + "--" +
                                            "--" + bottomElement.X + ":" + bottomElement.Y + "--" + rightElement.X + ":" + rightElement.Y + "--" + besideRight.X + ":" + besideRight.Y);
                            GridManager.instance.text.SetActive(true);
                            break;
                        }
                    }

                }
                break;
        }
    }

    public void ResetActiveElementsList()
    {
        activeElements.Clear();
    }
}

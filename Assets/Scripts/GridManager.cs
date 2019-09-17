using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    private GridManager gridManager;

    public GameObject holder;

    [HideInInspector]
    public List<GridIndex> elementsList = new List<GridIndex>();
    public PatternDetector patternDetector;

    public int minElements = 0;
    public int maxElements = 5;

    private Color clickedColor = Color.red;

    private int minPatternCheck = 3;
    private int totalActiveElements = 0;

    private void Awake()
    {
        gridManager = this;

        if(instance == null)
            instance = gridManager;

        else
            Destroy(instance);
    }

    /// <summary>
    /// Assigning the Listerners to buttons for only grid elements
    /// </summary>
    public void AssignTheListenersToButtons()
    {
        Button[] buttons = holder.GetComponentsInChildren<Button>(true);

        foreach (Button button in buttons)
        {
            Button b = button;
            button.onClick.AddListener(() => { OnElementClicked(b); });
        }
    }

    /// <summary>
    /// When any element is clicked in the Grid
    /// </summary>
    /// <param name="button"></param>
    public void OnElementClicked(Button button)
    {
        //Checking if the button is already clicked
        bool isButtonClicked = button.transform.GetComponent<GridIndex>().isClicked;

        if (!isButtonClicked)
        {
            button.transform.GetComponent<Image>().color = clickedColor;
            button.interactable = false;
            button.transform.GetComponent<GridIndex>().isClicked = true;

            if (patternDetector != null)
                patternDetector.activeElements.Add(button.transform.GetComponent<GridIndex>());

            totalActiveElements++;
        }

        if (totalActiveElements > minPatternCheck)
        {
            if (patternDetector != null)
                patternDetector.CheckForPattern();
        }

    }
}

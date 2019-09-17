using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    private GridManager gridManager;

    public GameObject holder;
    public GameObject text;

    [HideInInspector]
    public List<GridIndex> elementsList = new List<GridIndex>();
    public PatternDetector patternDetector;
    public GridCreator gridCreator;

    public int minElements = 0;
    public int maxElements = 5;

    public Dropdown patternTypesDropDown;

    private Color clickedColor = Color.red;

    private int minPatternCheck;
    private int totalActiveElements = 0;

    private bool checkForPattern = false;

    private PATTERN_TYPE currentPattern;

    private void Awake()
    {
        gridManager = this;

        if(instance == null)
            instance = gridManager;

        else
            Destroy(instance);
    }

    private void Start()
    {
        PopulatePatternDropDown();
        patternTypesDropDown.onValueChanged.AddListener(delegate { OnPatternTypeSelected(patternTypesDropDown); });
    }

    private void Update()
    {
        //We start pattern serach algorithm only if minimum required no of dots have been activated or pressed
        if (totalActiveElements > minPatternCheck && checkForPattern)
        {
            checkForPattern = false;

            if (patternDetector != null)
                patternDetector.CheckForPattern(currentPattern);
        }
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

            checkForPattern = true;
            totalActiveElements++;
        }
    }

    /// <summary>
    /// Resetting the entire grid be deleting existing grind and creating a new one (Testing purpouse only)
    /// </summary>
    public void Reset_ActiveGrid()
    {
        for (int i = 0; i < holder.transform.childCount; i++)
        {
            Destroy(holder.transform.GetChild(i).gameObject);
        }

        elementsList.Clear();
        totalActiveElements = 0;
        checkForPattern = false;
        text.SetActive(false);
        gridCreator.ResetGridCurrentData();
        patternDetector.ResetActiveElementsList();
        Utils.ClearLogConsole();

        gridCreator.CreateButtoGrid();
    }

    /// <summary>
    /// Populating the all the available Patterns in the dropdown
    /// </summary>
    public void PopulatePatternDropDown()
    {
        string[] enumNames = System.Enum.GetNames(typeof(PATTERN_TYPE));
        List<string> names = new List<string>(enumNames);

        patternTypesDropDown.AddOptions(names);
    }

    /// <summary>
    /// Callback to handle when any one of the pattern is selected
    /// </summary>
    /// <param name="dropdown"></param>
    public void OnPatternTypeSelected(Dropdown dropdown)
    {
        currentPattern = (PATTERN_TYPE)dropdown.value;


        switch(currentPattern)
        {
            case PATTERN_TYPE.A:
                minPatternCheck = 5;
                break;
            case PATTERN_TYPE.C:
                minPatternCheck = 6;
                break;
            case PATTERN_TYPE.E:
                minPatternCheck = 10;
                break;
            case PATTERN_TYPE.FOUR_DOTS:
                minPatternCheck = 3;
                break;
            case PATTERN_TYPE.H:
                minPatternCheck = 6;
                break;
            case PATTERN_TYPE.I_SEVEN_DOTS:
                minPatternCheck = 6;
                break;
            case PATTERN_TYPE.L_FIVE_DOTS:
                minPatternCheck = 4;
                break;
            case PATTERN_TYPE.L_FOUR_DOTS:
                minPatternCheck = 3;
                break;
            case PATTERN_TYPE.PLUS_FIVE_DOTS:
                minPatternCheck = 4;
                break;
            case PATTERN_TYPE.SQUARE_FOUR_DOTS:
                minPatternCheck = 3;
                break;
            case PATTERN_TYPE.THREE_DOTS:
                minPatternCheck = 2;
                break;
            case PATTERN_TYPE.T_FIVE_DOTS:
                minPatternCheck = 4;
                break;
            case PATTERN_TYPE.T_FOUR_DOTS:
                minPatternCheck = 3;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCreator : MonoBehaviour
{
    public GameObject buttonPrefab;

    private GameObject _holder;

    private int maxGridElements = 25; // 5 X 5 grid
    private int maxRows = 5;
    private int maxCol = 5;

    private int currentRow;
    private int currentCol;

    private void Start()
    {
        _holder = GridManager.instance.holder;
    }
    public void CreateButtoGrid()
    {
        InvokeRepeating("InitButton", 0f, 0.05f);
    }

    private void InitButton()
    {
        //Once we reach max amount of elements needed we stop instantitating
        if (_holder.transform.childCount == maxGridElements)
        {
            CancelInvoke("InitButton");
            GridManager.instance.AssignTheListenersToButtons();
        }

        else
        {
            GameObject obj = Instantiate(buttonPrefab, _holder.transform);

            obj.AddComponent<GridIndex>();  //Getting componenet to assign the Grid Values 

            if (currentCol >= maxCol)            //Once we reach max column values , increasing the row count by 1 and resetting the col value to 0 
            {
                currentRow++;
                currentCol = 0;
            }

            //Assigning the row and column values to the current instantiated obj
            obj.GetComponent<GridIndex>().X = currentRow;
            obj.GetComponent<GridIndex>().Y = currentCol;
            GridManager.instance.elementsList.Add(obj.GetComponent<GridIndex>());
            currentCol++;
        }
    }
}

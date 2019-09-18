using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternBase
{
    protected GridIndex topElement = null, bottomElement = null, leftElement = null, rightElement = null;

    //Stores the final list of grid indices repective pattern formed at
    protected List<GridIndex> resultPatternIndices = new List<GridIndex>();

    // Used for getting to know in which direction the pattern is formed (Helps in resuing the code with ease)
    protected enum PatternDirection
    {
        NONE,
        TOP,
        BOTTOM,
        LEFT,
        RIGHT,
        HORIZONTAL,
        VERTICAL,
    }

    //This variable Stores in which direction the current pattern is formed
    protected PatternDirection resultPatternDirection = PatternDirection.NONE;
}

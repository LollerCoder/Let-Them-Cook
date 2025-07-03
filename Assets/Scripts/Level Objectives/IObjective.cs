using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjective
{
    
    void clearCondition();

    void onConditionClear();

    bool getIfCleared();
}